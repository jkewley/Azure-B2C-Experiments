using System.ComponentModel.DataAnnotations;

namespace WebApp_OpenIDConnect_DotNet_B2C.Model
{
    public class User
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
    }
}