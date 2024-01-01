namespace UnitTests.Models
{
    internal record EmployeeMissingId
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
