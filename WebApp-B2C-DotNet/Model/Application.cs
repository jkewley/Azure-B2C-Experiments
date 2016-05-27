using System.Collections.Generic;

namespace WebApp_OpenIDConnect_DotNet_B2C.Model
{
    /// <summary>
    ///     The application is filled out by the <see cref="Applicant" /> when trying to secure a loan from the lending entity that is using this install of the product
    /// </summary>
    public class Application
    {
        public List<PhoneNumber> ContactNumbers { get; set; }

        public string DOB { get; set; }

        public string DriverLicenseNumber { get; set; }

        public string FirstName { get; set; }

        public Address HomeAddress { get; set; }

        public string LastName { get; set; }

        public MaritalStatus MaritalStatus { get; set; }

        public int MonthlyExpenses { get; set; }

        public int NetWorth { get; set; }

        public int RevolvinghDebt { get; set; }

        public int Salary { get; set; }

        public string SSN { get; set; }

        public int YearsAtCurrentResidence { get; set; }
    }

    public enum MaritalStatus
    {
        Unmarried,
        Married,
        Separated
    }

    public class Address
    {
        public string City { get; set; }

        public string State { get; set; }

        public string StreetAddress { get; set; }

        public string ZipCode { get; set; }
    }

    public class PhoneNumber
    {
        public string Number { get; set; }

        public PhoneType Use { get; set; }
    }

    public enum PhoneType
    {
        Home,
        Office,
        Cell
    }
}