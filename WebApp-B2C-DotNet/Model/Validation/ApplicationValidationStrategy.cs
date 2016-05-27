namespace WebApp_OpenIDConnect_DotNet_B2C.Model.Validation
{
    public abstract class ApplicationValidationStrategy
    {
        /// <summary>
        ///     Validates a completed <see cref="Application" /> submitted by an <see cref="Applicant" /> before sending it off to an agency
        /// </summary>
        public abstract ApplicationValidationResult Validate(Application anApplication);
    }
}