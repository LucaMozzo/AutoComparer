namespace UnitTests.Models
{
    internal class EmployeeCar
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Car Car { get; set; }
    }
}
