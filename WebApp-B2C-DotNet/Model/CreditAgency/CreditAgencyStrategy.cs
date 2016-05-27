namespace WebApp_OpenIDConnect_DotNet_B2C.Model.CreditAgency
{
    /// <summary>
    ///     This is an abstraction of the process involved in retrieving a credit score from one of the three credit score agencies
    /// </summary>
    public abstract class CreditAgencyStrategy
    {
        /// <summary>
        ///     Takes an <see cref="Application" /> from an <see cref="Applicant" /> and sends it to the credit agency for reivew
        /// </summary>
        /// <param name="anApplication">The application</param>
        /// <returns>A <see cref="CreditReviewResult" /> that contains the score that the Credit Agency came up with</returns>
        public abstract CreditReviewResult ProcessApplication(Application anApplication);
    }

    public class CreditReviewResult
    {
        public int Score { get; set; }
    }
}