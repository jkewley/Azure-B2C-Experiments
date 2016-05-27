using System;

namespace WebApp_OpenIDConnect_DotNet_B2C.Model.CreditAgency
{
    public class EquifaxStrategy : CreditAgencyStrategy
    {
        /// <summary>
        ///     Takes an <see cref="Application" /> from an <see cref="Applicant" /> and sends it to the credit agency for reivew
        /// </summary>
        /// <param name="anApplication">The application</param>
        /// <returns>A <see cref="CreditReviewResult" /> that contains the score that the Credit Agency came up with</returns>
        public override CreditReviewResult ProcessApplication(Application anApplication) {
            throw new NotImplementedException();
        }
    }
}