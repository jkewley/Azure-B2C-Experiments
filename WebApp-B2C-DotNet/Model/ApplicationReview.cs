using System;

namespace WebApp_OpenIDConnect_DotNet_B2C.Model
{
    /// <summary>
    ///     Ties an application to the lifecycle of the review process
    /// </summary>
    public class ApplicationReview
    {
        public Application Application { get; set; }

        public DateTime Completed { get; set; }

        public Employee Processor { get; set; }

        public DateTime Submitted { get; set; }
    }
}