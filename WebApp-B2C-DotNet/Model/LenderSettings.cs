using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WebApp_OpenIDConnect_DotNet_B2C.Model.CreditAgency;

namespace WebApp_OpenIDConnect_DotNet_B2C.Model
{
    public class LenderSettings
    {
        public CreditAgencyEnum PreferredCreditAgency { get; set; }

        public ApplicationValidationStrategy ValidationStrategy { get; set; }
    }
}