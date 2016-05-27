using WebApp_OpenIDConnect_DotNet_B2C.Model.CreditAgency;
using WebApp_OpenIDConnect_DotNet_B2C.Model.Validation;

namespace WebApp_OpenIDConnect_DotNet_B2C.Model
{
    /// <summary>
    ///     The running instance of this application is used by an individual lender (branch) for a banking institution (bank).
    ///     In our world each individual lender can choose how they want to process applications basedon the typical local customer profile
    /// </summary>
    public class LenderSettings
    {
        public CreditAgencyEnum PreferredCreditAgency { get; set; }

        public ApplicationValidationStrategy ValidationStrategy { get; set; }
    }
}