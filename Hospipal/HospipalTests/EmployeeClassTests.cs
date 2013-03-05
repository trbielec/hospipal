using System;
using NUnit.Framework;
using Hospipal.Database_Class;

namespace HospipalTests
{
    [TestFixture]
    public class EmployeeClassTests
    {
        private Employee employee;

        [SetUp]
        public void setup()
        {
            employee = new Employee(1, "TestFname", "TestLname", "TestSpec", "TestType", 1);
            employee.Insert();
        }

        [TearDown]
        public void teardown()
        {
            employee.Delete();
        }

        [TestCase]
        public void TestEmployeeInsert()
        {
            Employee testEmployee = new Employee(1, null, null, null, null, 0);
            Assert.True(testEmployee.Insert());

            testEmployee.Delete();
        }

        [TestCase]
        public void TestGetEmployees()
        {
            Assert.NotNull(Employee.GetEmployees());
        }
    }
}
