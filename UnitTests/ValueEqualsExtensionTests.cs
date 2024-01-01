using AutoComparer;
using UnitTests.Models;

namespace UnitTests
{
    [TestClass]
    public class ValueEqualsExtensionTests
    {
        private Employee _employee = new Employee
        {
            Id = Guid.NewGuid(),
            FirstName = "James",
            LastName = "Doe",
            DateOfBirth = DateTime.Now
        };

        [TestMethod]
        public void HappyPathEqualValueEmployee()
        {
            var result = _employee.ValueEquals(new EmployeeMatchingFields
            {
                Id = _employee.Id,
                FirstName = _employee.FirstName,
                LastName = _employee.LastName,
                DateOfBirth = _employee.DateOfBirth
            });

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HappyPathNonEqualValueEmployee()
        {
            var result = _employee.ValueEquals(new EmployeeMatchingFields
            {
                Id = _employee.Id,
                FirstName = _employee.FirstName,
                LastName = _employee.LastName,
                DateOfBirth = _employee.DateOfBirth.Value.AddDays(-1)
            });

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SameTypeEqualValue()
        {
            var result = _employee.ValueEquals(new Employee
            {
                Id = _employee.Id,
                FirstName = _employee.FirstName,
                LastName = _employee.LastName,
                DateOfBirth = _employee.DateOfBirth
            });

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SameTypeNonEqualValue()
        {
            var result = _employee.ValueEquals(new Employee
            {
                Id = _employee.Id,
                FirstName = _employee.FirstName,
                LastName = _employee.LastName,
                DateOfBirth = _employee.DateOfBirth.Value.AddDays(-1)
            });

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CarEmployeeNoOverlapNonEqual()
        {
            var result = _employee.ValueEquals(new Car
            {
                Make = "Toyota",
                Model = "Yaris"
            });

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void InheritanceEqualValueWorks()
        {
            var result = _employee.ValueEquals(new InheritingFromEmployee()
            {
                Id = _employee.Id,
                FirstName = _employee.FirstName,
                LastName = _employee.LastName,
                DateOfBirth = _employee.DateOfBirth
            });

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IdAsStringNotHandled()
        {
            var result = _employee.ValueEquals(new EmployeeIdAsString
            {
                Id = "1234567",
                FirstName = _employee.FirstName,
                LastName = _employee.LastName,
                DateOfBirth = _employee.DateOfBirth
            });

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void MissingIdEqualValue()
        {
            var result = _employee.ValueEquals(new EmployeeMissingId()
            {
                FirstName = _employee.FirstName,
                LastName = _employee.LastName,
                DateOfBirth = _employee.DateOfBirth
            });

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void NonNullableDoBEqualValue()
        {
            var result = _employee.ValueEquals(new EmployeeNonNullableDoB()
            {
                Id = _employee.Id,
                FirstName = _employee.FirstName,
                LastName = _employee.LastName,
                DateOfBirth = _employee.DateOfBirth.Value
            });

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void NonNullableDoBNonEqualValue()
        {
            var result = _employee.ValueEquals(new EmployeeNonNullableDoB()
            {
                Id = _employee.Id,
                FirstName = _employee.FirstName,
                LastName = _employee.LastName,
                DateOfBirth = _employee.DateOfBirth.Value.AddDays(-1)
            });

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SameValueNullDoBCheck()
        {
            var result = _employee.ValueEquals(new Employee()
            {
                Id = _employee.Id,
                FirstName = _employee.FirstName,
                LastName = _employee.LastName,
                DateOfBirth = null
            });

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SameValueBothNullDoB()
        {
            var result = new Employee()
            {
                Id = _employee.Id,
                FirstName = _employee.FirstName,
                LastName = _employee.LastName,
                DateOfBirth = null
            }.ValueEquals(new Employee()
            {
                Id = _employee.Id,
                FirstName = _employee.FirstName,
                LastName = _employee.LastName,
                DateOfBirth = null
            });

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void InnerObjectEqualValue()
        {
            var employee1 = new EmployeeCar
            {
                Id = _employee.Id,
                FirstName = _employee.FirstName,
                LastName = _employee.LastName,
                DateOfBirth = _employee.DateOfBirth,
                Car = new Car
                {
                    Make = "Toyota",
                    Model = "Yaris"
                }
            };

            var employee2 = new EmployeeCar
            {
                Id = _employee.Id,
                FirstName = _employee.FirstName,
                LastName = _employee.LastName,
                DateOfBirth = _employee.DateOfBirth,
                Car = new Car
                {
                    Make = "Toyota",
                    Model = "Yaris"
                }
            };

            Assert.IsTrue(employee1.ValueEquals(employee2, true));
        }

        [TestMethod]
        public void InnerObjectNonEqualValue()
        {
            var employee1 = new EmployeeCar
            {
                Id = _employee.Id,
                FirstName = _employee.FirstName,
                LastName = _employee.LastName,
                DateOfBirth = _employee.DateOfBirth,
                Car = new Car
                {
                    Make = "Toyota",
                    Model = "Yaris"
                }
            };

            var employee2 = new EmployeeCar
            {
                Id = _employee.Id,
                FirstName = _employee.FirstName,
                LastName = _employee.LastName,
                DateOfBirth = _employee.DateOfBirth,
                Car = new Car
                {
                    Make = "Toyota",
                    Model = "Prius"
                }
            };

            Assert.IsFalse(employee1.ValueEquals(employee2, true));
        }

        [TestMethod]
        public void InnerObjectDefaultEqualsMethodIsEqual()
        {
            var car = new Car
            {
                Make = "Toyota",
                Model = "Prius"
            };

            var employee1 = new EmployeeCar
            {
                Id = _employee.Id,
                FirstName = _employee.FirstName,
                LastName = _employee.LastName,
                DateOfBirth = _employee.DateOfBirth,
                Car = car
            };

            var employee2 = new EmployeeCar
            {
                Id = _employee.Id,
                FirstName = _employee.FirstName,
                LastName = _employee.LastName,
                DateOfBirth = _employee.DateOfBirth,
                Car = car
            };

            Assert.IsTrue(employee1.ValueEquals(employee2));
        }

        [TestMethod]
        public void InnerObjectDefaultEqualsMethodIsNotEqual()
        {
            var car = new Car
            {
                Make = "Toyota",
                Model = "Prius"
            };

            var employee1 = new EmployeeCar
            {
                Id = _employee.Id,
                FirstName = _employee.FirstName,
                LastName = _employee.LastName,
                DateOfBirth = _employee.DateOfBirth,
                Car = car
            };

            var employee2 = new EmployeeCar
            {
                Id = _employee.Id,
                FirstName = _employee.FirstName,
                LastName = _employee.LastName,
                DateOfBirth = _employee.DateOfBirth,
                Car = new Car
                {
                    Make = car.Make,
                    Model = car.Model
                }
            };

            Assert.IsFalse(employee1.ValueEquals(employee2));
        }
    }
}