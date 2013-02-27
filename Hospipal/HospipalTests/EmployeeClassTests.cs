using System;
using NUnit.Framework;
using Hospipal.Database_Class;

namespace HospipalTests
{
    [TestFixture]
    public class EmployeeClassTests
    {
        private Employee employee;

        [TestCase]
        public void TestEmployeeInsertUpdateDeleteMethods()
        {
            employee = new Employee(1000, "Test", "Test", "Test", "Test", 1);
            Assert.True(employee.Insert());
            Assert.False(employee.Insert());

            employee.Fname = "Test3";
            Assert.True(employee.Update());

            Assert.True(employee.Delete());
            Assert.False(employee.Delete());
        }

        [TestCase]
        public void TestGetNewEid()
        {
            Assert.Greater(Employee.GenerateNewEid(), -1);
        }

        [TestCase]
        public void TestGetEmployees()
        {
            Assert.NotNull(Employee.GetEmployees());
        }
    }
}
