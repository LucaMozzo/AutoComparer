﻿namespace UnitTests.Models
{
    internal record EmployeeNonNullableDoB
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}