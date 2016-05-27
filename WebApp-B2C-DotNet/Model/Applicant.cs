using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp_OpenIDConnect_DotNet_B2C.Model
{
    public class Applicant
    {
        [MaxLength(250)]
        public string EmailAddress { get; set; }

        public string ExternalID { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [Key]
        public int UserID { get; set; }

        public DateTime DateOfBirth { get; set; }

        public byte[] EncryptionKey { get; set; }

        public string Gender { get; set; }

        public string Ethnicity { get; set; }
    }
}