using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Notifications;
using Microsoft.Owin.Security.OpenIdConnect;

using Owin;

using WebApp_OpenIDConnect_DotNet_B2C.Data;
using WebApp_OpenIDConnect_DotNet_B2C.Model;
using WebApp_OpenIDConnect_DotNet_B2C.Policies;

// The following using statements were added for this sample

namespace WebApp_OpenIDConnect_DotNet_B2C
{
    public partial class Startup
    {
        // The ACR claim is used to indicate which policy was executed
        public const string AcrClaimType = "http://schemas.microsoft.com/claims/authnclassreference";
        public const string OIDCMetadataSuffix = "/.well-known/openid-configuration";
        public const string PolicyKey = "b2cpolicy";

        // App config settings
        private static readonly string aadInstance = ConfigurationManager.AppSettings["ida:AadInstance"];
        private static readonly string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        public static string PasswordResetPolicyId = ConfigurationManager.AppSettings["ida:PasswordResetPolicyId"];
        public static readonly string ProfileEditPolicyId = ConfigurationManager.AppSettings["ida:ProfileEdit"];
        private static readonly string redirectUri = ConfigurationManager.AppSettings["ida:RedirectUri"];

        // B2C policy identifiers
        public static string SusiPolicyId = ConfigurationManager.AppSettings["ida:SusiPolicyId"];
        private static readonly string tenant = ConfigurationManager.AppSettings["ida:Tenant"];

        public void ConfigureAuth(IAppBuilder app) {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            OpenIdConnectAuthenticationOptions options = new OpenIdConnectAuthenticationOptions {
                // These are standard OpenID Connect parameters, with values pulled from web.config
                ClientId = clientId,
                RedirectUri = redirectUri,
                PostLogoutRedirectUri = redirectUri,
                Notifications = new OpenIdConnectAuthenticationNotifications {
                    AuthenticationFailed = AuthenticationFailed,
                    RedirectToIdentityProvider = OnRedirectToIdentityProvider,
                    SecurityTokenValidated = OnSecurityTokenValidated, // the entrypoint for token return from B2C infrastructure
                    AuthorizationCodeReceived = OnAuthorizationCodeReceived, //long-lived tokens that need refreshin'. Comes from offline_access claim
                    MessageReceived = OnMessageReceived,
                    SecurityTokenReceived = OnSecurityTokenReceived
                },
                Scope = "openid offline_access",
                ResponseType = "id_token",

                // The PolicyConfigurationManager takes care of getting the correct Azure AD authentication
                // endpoints from the OpenID Connect metadata endpoint.  It is included in the PolicyAuthHelpers folder.
                ConfigurationManager = new PolicyConfigurationManager(
                    String.Format(CultureInfo.InvariantCulture, aadInstance, tenant, "/v2.0", OIDCMetadataSuffix),
                    new[] {
                        SusiPolicyId,
                        PasswordResetPolicyId,
                        ProfileEditPolicyId
                    }),

                // This piece is optional - it is used for displaying the user's name in the navigation bar.
                TokenValidationParameters = new TokenValidationParameters {
                    NameClaimType = "name"
                }
            };

            app.UseOpenIdConnectAuthentication(options);
        }

        // Used for avoiding yellow-screen-of-death TODO
        private Task AuthenticationFailed(AuthenticationFailedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> notification) {
            notification.HandleResponse();

            if(notification.ProtocolMessage.ErrorDescription != null && notification.ProtocolMessage.ErrorDescription.Contains("AADB2C90118")) {
                notification.Response.Redirect("/Account/ResetPassword");
            } else {
                notification.Response.Redirect("/Home/Error?message=" + notification.Exception.Message);
            }

            return Task.FromResult(0);
        }

        private Task OnAuthorizationCodeReceived(AuthorizationCodeReceivedNotification someArgs) {
            return Task.FromResult(0);
        }

        private Task OnMessageReceived(MessageReceivedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> aArg) {
            return Task.FromResult(0);
        }

        // This notification can be used to manipulate the OIDC request before it is sent.  Here we use it to send the correct policy.
        private async Task OnRedirectToIdentityProvider(RedirectToIdentityProviderNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> notification) {
            PolicyConfigurationManager mgr = notification.Options.ConfigurationManager as PolicyConfigurationManager;
            if(notification.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest) {
                OpenIdConnectConfiguration config = await mgr.GetConfigurationByPolicyAsync(CancellationToken.None, notification.OwinContext.Authentication.AuthenticationResponseRevoke.Properties.Dictionary[PolicyKey]);
                notification.ProtocolMessage.IssuerAddress = config.EndSessionEndpoint;
            } else {
                OpenIdConnectConfiguration config = await mgr.GetConfigurationByPolicyAsync(CancellationToken.None, notification.OwinContext.Authentication.AuthenticationResponseChallenge.Properties.Dictionary[PolicyKey]);
                notification.ProtocolMessage.IssuerAddress = config.AuthorizationEndpoint;
            }
        }

        private Task OnSecurityTokenReceived(SecurityTokenReceivedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> aArg) {
            return Task.FromResult(0);
        }

        private Task OnSecurityTokenValidated(SecurityTokenValidatedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> notification) {
            // If you wanted to keep some local state in the app (like a db of signed up users),
            // you could use this notification to create the user record if it does not already
            // exist.
            List<Claim> myClaims = notification.AuthenticationTicket.Identity.Claims.ToList();
            string anOIDClaimValue = myClaims.FirstOrDefault(aClaim => aClaim.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;
            if(string.IsNullOrEmpty(anOIDClaimValue)) throw new ApplicationException("Missing OID claim!");
            using(B2CContext aContext = new B2CContext()) {
                Applicant anInternalApplicant = aContext.Applicants.FirstOrDefault(aUser => aUser.ExternalID == anOIDClaimValue);
                if(anInternalApplicant == null) {
                    Applicant aNewApplicant = new Applicant {
                        ExternalID = anOIDClaimValue,
                        FirstName = myClaims.FirstOrDefault(aClaim => aClaim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")?.Value,
                        LastName = myClaims.FirstOrDefault(aClaim => aClaim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")?.Value,
                        Gender = myClaims.FirstOrDefault(aClaim => aClaim.Type == "extension_Gender")?.Value,
                        Ethnicity = myClaims.FirstOrDefault(aClaim => aClaim.Type == "extension_Ethnicity")?.Value,
                        DateOfBirth = DateTime.Now.AddYears(-20)
                    };
                    aContext.Applicants.Add(aNewApplicant);
                    aContext.SaveChanges();
                }
            }
            return Task.FromResult(0);
        }
    }
}