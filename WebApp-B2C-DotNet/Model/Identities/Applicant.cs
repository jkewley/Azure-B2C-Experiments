using System;

namespace WebApp_OpenIDConnect_DotNet_B2C.Model
{
    /// <summary>
    ///     The Applicant is the person who is applying for a loan
    /// </summary>
    public class Applicant : User
    {
        public DateTime DateOfBirth { get; set; }

        public byte[] EncryptionKey { get; set; }

        public string Ethnicity { get; set; }

        public string Gender { get; set; }

        public string SocialSecurityNumber { get; set; }
    }
}