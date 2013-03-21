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
            Employee testEmployee = new Employee(Employee.GenerateNextEid(), "UnitTestEmployee", "UnitTestLname", "", "", 0);
            Assert.True(testEmployee.Insert());

            testEmployee.Delete();
        }

        [Test]
        public void TestEmployeeUpdate()
        {
            Employee testEmployee = new Employee(Employee.GenerateNextEid(), "UnitTestEmployee", "UnitTestLname", "", "", 0);
            testEmployee.Insert();

            testEmployee.Fname = "ChangedName";
            Assert.True(testEmployee.Update());

            testEmployee.Delete();
        }

        [TestCase]
        public void TestEmployeeDelete()
        {
            Employee testEmployee = new Employee(Employee.GenerateNextEid(), "UnitTestEmployee", "UnitTestLname", "", "", 0);
            testEmployee.Insert();

            Assert.True(testEmployee.Delete());
        }

        [TestCase]
        public void TestGetEmployees()
        {
            Employee testEmployee = new Employee(Employee.GenerateNextEid(), "UnitTestEmployee", "UnitTestLname", "", "", 0);
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

        #region Get/Set Tests
        [TestCase]
        public void TestGetSetEid()
        {
            Employee e = new Employee(1);
            Assert.True(e.Eid == 1);
            e.Eid = 2;
            Assert.True(e.Eid == 2);
        }

        [TestCase]
        public void TestGetSetFname()
        {
            Employee e = new Employee();
            e.Fname = "Test";
            Assert.True(e.Fname == "Test");
        }

        [TestCase]
        public void TestGetSetLname()
        {
            Employee e = new Employee();
            e.Lname = "Test";
            Assert.True(e.Lname == "Test");
        }

        [TestCase]
        public void TestGetSetSpecialty()
        {
            Employee e = new Employee();
            e.Specialty = "Test";
            Assert.True(e.Specialty == "Test");
        }

        [TestCase]
        public void TestGetSetType()
        {
            Employee e = new Employee();
            e.Employee_type = "Test";
            Assert.True(e.Employee_type == "Test");
        }

        [TestCase]
        public void TestGetSetSupervisor()
        {
            Employee e = new Employee();
            e.Supervisor_id = 1;
            Assert.True(e.Supervisor_id == 1);
        }
        #endregion
    }
}
