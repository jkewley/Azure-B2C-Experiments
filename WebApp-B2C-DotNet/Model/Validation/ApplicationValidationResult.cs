using System.Collections.Generic;

namespace WebApp_OpenIDConnect_DotNet_B2C.Model.Validation
{
    public class ApplicationValidationResult
    {
        public IEnumerable<ValidationError> Errors { get; set; }

        public bool IsValid { get; set; }
    }

    public class ValidationError
    {
        public string Description { get; set; }

        public bool IsShowStopper { get; set; }

        public int LineNumber { get; set; }
    }
}