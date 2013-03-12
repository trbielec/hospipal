using System;
using NUnit.Framework;
using Hospipal.Database_Class;
using System.Collections.Generic;
using Hospipal;

namespace HospipalTests
{
    [TestFixture]
    public class EmployeeClassTests
    {

        [TestCase]
        public void TestEmployeeInsert()
        {
            Employee testEmployee = new Employee(Employee.GenerateNextEid(), "TestEmployee", "TestLname", "", "", 0);
            Assert.True(testEmployee.Insert());

            testEmployee.Delete();
        }

        [Test]
        public void TestEmployeeUpdate()
        {
            Employee testEmployee = new Employee(Employee.GenerateNextEid(), "TestEmployee", "TestLname", "", "", 0);
            testEmployee.Insert();

            testEmployee.Fname = "ChangedName";
            Assert.True(testEmployee.Update());

            testEmployee.Delete();
        }

        [TestCase]
        public void TestEmployeeDelete()
        {
            Employee testEmployee = new Employee(Employee.GenerateNextEid(), "TestEmployee", "TestLname", "", "", 0);
            testEmployee.Insert();

            Assert.True(testEmployee.Delete());
        }

        [TestCase]
        public void TestGetEmployees()
        {
            Employee testEmployee = new Employee(Employee.GenerateNextEid(), "TestEmployee", "TestLname", "", "", 0);
            testEmployee.Insert();

            List<Employee> employees = Employee.GetEmployees();
            Assert.IsNotEmpty(employees);

            testEmployee.Delete();
        }

        [TestCase]
        public void TestGenerateNextEid()
        {
            List<object[]> obj = Database.Select("SELECT Auto_increment FROM information_schema.tables WHERE table_name= 'Employee'AND table_schema = DATABASE();");
            int next = Convert.ToInt32(obj[0][0]);
            Assert.True(Employee.GenerateNextEid() == next);
        }
    }
}
