using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp_OpenIDConnect_DotNet_B2C.Model.Validation
{
    public abstract class ApplicationValidationStrategy
    {
        public abstract ApplicationValidationResult Validate(Application anApplication);
    }
}