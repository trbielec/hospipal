using System;
using NUnit.Framework;
using Hospipal.Database_Class;
using System.Collections.Generic;
using Hospipal;

namespace HospipalTests
{
    [TestFixture]
    public class SearchClassTests
    {
        [SetUp]
        public void SetDB()
        {
            Database.useTestDB();
        }

        #region PatientSearchTests
        [TestCase]
        public void TestSearchPatientWithValidQueryByExactFname()
        {
            Patient p1 = new Patient(10000, "SearchFname1", "SearchLname1", new DateTime(), "Test Address", "Test City", "Test Provice", "TEST", "1111111111", "2222222222", "3333333333");
            p1.Insert();
            p1.Select();

            Patient p2 = new Patient(20000, "SearchFname2", "SearchLname2", new DateTime(), "Test Address", "Test City", "Test Provice", "TEST", "4444444444", "5555555555", "6666666666");
            p2.Insert();
            p2.Select();

            List<string> db = new List<string>();
            db.Add("fname");

            List<string> s = new List<string>();
            s.Add("SearchFname1");

            Search search = new Search("Patient");
            search.UseInputs(db, s);

            List<Patient> p = Search.SearchPatient(search.GetBuiltQuery());
            Assert.True(p[0].PatientID == p1.PatientID);

            p1.Delete();
            p2.Delete();
        }

        [TestCase]
        public void TestSearchPatientWithValidQueryByCloseFname()
        {
            Patient p1 = new Patient(10000, "SearchFname1", "SearchLname1", new DateTime(), "Test Address", "Test City", "Test Provice", "TEST", "1111111111", "2222222222", "3333333333");
            p1.Insert();
            p1.Select();

            Patient p2 = new Patient(20000, "SearchFname2", "SearchLname2", new DateTime(), "Test Address", "Test City", "Test Provice", "TEST", "4444444444", "5555555555", "6666666666");
            p2.Insert();
            p2.Select();

            List<string> db = new List<string>();
            db.Add("fname");

            List<string> s = new List<string>();
            s.Add("Search");

            Search search = new Search("Patient");
            search.UseInputs(db, s);

            List<Patient> p = Search.SearchPatient(search.GetBuiltQuery());
            Assert.True(p.Count >= 2);

            p1.Delete();
            p2.Delete();
        }

        [TestCase]
        public void TestSearchPatientWithValidQueryByExactHcNo()
        {
            Patient p1 = new Patient(10000, "SearchFname1", "SearchLname1", new DateTime(), "Test Address", "Test City", "Test Provice", "TEST", "1111111111", "2222222222", "3333333333");
            p1.Insert();
            p1.Select();

            Patient p2 = new Patient(20000, "SearchFname2", "SearchLname2", new DateTime(), "Test Address", "Test City", "Test Provice", "TEST", "4444444444", "5555555555", "6666666666");
            p2.Insert();
            p2.Select();

            List<string> db = new List<string>();
            db.Add("hc_no");

            List<string> s = new List<string>();
            s.Add("10000");

            Search search = new Search("Patient");
            search.UseInputs(db, s);

            List<Patient> p = Search.SearchPatient(search.GetBuiltQuery());
            Assert.True(p[0].PatientID == p1.PatientID);

            p1.Delete();
            p2.Delete();
        }

        [TestCase]
        public void TestSearchPatientWithValidQueryByCloseHcNo()
        {
            Patient p1 = new Patient(10000, "SearchFname1", "SearchLname1", new DateTime(), "Test Address", "Test City", "Test Provice", "TEST", "1111111111", "2222222222", "3333333333");
            p1.Insert();
            p1.Select();

            Patient p2 = new Patient(10001, "SearchFname2", "SearchLname2", new DateTime(), "Test Address", "Test City", "Test Provice", "TEST", "4444444444", "5555555555", "6666666666");
            p2.Insert();
            p2.Select();

            List<string> db = new List<string>();
            db.Add("hc_no");

            List<string> s = new List<string>();
            s.Add("1000");

            Search search = new Search("Patient");
            search.UseInputs(db, s);

            List<Patient> p = Search.SearchPatient(search.GetBuiltQuery());
            Assert.True(p.Count >= 2);

            p1.Delete();
            p2.Delete();
        }

        [TestCase]
        public void TestSearchPatientWithValidQueryByExactLname()
        {
            Patient p1 = new Patient(10000, "SearchFname1", "SearchLname1", new DateTime(), "Test Address", "Test City", "Test Provice", "TEST", "1111111111", "2222222222", "3333333333");
            p1.Insert();
            p1.Select();

            Patient p2 = new Patient(20000, "SearchFname2", "SearchLname2", new DateTime(), "Test Address", "Test City", "Test Provice", "TEST", "4444444444", "5555555555", "6666666666");
            p2.Insert();
            p2.Select();

            List<string> db = new List<string>();
            db.Add("lname");

            List<string> s = new List<string>();
            s.Add("SearchLname2");

            Search search = new Search("Patient");
            search.UseInputs(db, s);

            List<Patient> p = Search.SearchPatient(search.GetBuiltQuery());
            Assert.True(p[0].PatientID == p2.PatientID);

            p1.Delete();
            p2.Delete();
        }

        [TestCase]
        public void TestSearchPatientWithValidQueryByCloseLname()
        {
            Patient p1 = new Patient(10000, "SearchFname1", "SearchLname1", new DateTime(), "Test Address", "Test City", "Test Provice", "TEST", "1111111111", "2222222222", "3333333333");
            p1.Insert();
            p1.Select();

            Patient p2 = new Patient(20000, "SearchFname2", "SearchLname2", new DateTime(), "Test Address", "Test City", "Test Provice", "TEST", "4444444444", "5555555555", "6666666666");
            p2.Insert();
            p2.Select();

            List<string> db = new List<string>();
            db.Add("lname");

            List<string> s = new List<string>();
            s.Add("Search");

            Search search = new Search("Patient");
            search.UseInputs(db, s);

            List<Patient> p = Search.SearchPatient(search.GetBuiltQuery());
            Assert.True(p.Count >= 2);

            p1.Delete();
            p2.Delete();
        }

        [TestCase]
        public void TestSearchPatientWithValidQueryByExactAddress()
        {
            Patient p1 = new Patient(10000, "SearchFname1", "SearchLname1", new DateTime(), "Test Address1", "Test City", "Test Provice", "TEST", "1111111111", "2222222222", "3333333333");
            p1.Insert();
            p1.Select();

            Patient p2 = new Patient(20000, "SearchFname2", "SearchLname2", new DateTime(), "Test Address2", "Test City", "Test Provice", "TEST", "4444444444", "5555555555", "6666666666");
            p2.Insert();
            p2.Select();

            List<string> db = new List<string>();
            db.Add("street_address");

            List<string> s = new List<string>();
            s.Add("Test Address1");

            Search search = new Search("Patient");
            search.UseInputs(db, s);

            List<Patient> p = Search.SearchPatient(search.GetBuiltQuery());
            Assert.True(p[0].PatientID == p1.PatientID);

            p1.Delete();
            p2.Delete();
        }

        [TestCase]
        public void TestSearchPatientWithValidQueryByCloseAddress()
        {
            Patient p1 = new Patient(10000, "SearchFname1", "SearchLname1", new DateTime(), "Test Address", "Test City", "Test Provice", "TEST", "1111111111", "2222222222", "3333333333");
            p1.Insert();
            p1.Select();

            Patient p2 = new Patient(20000, "SearchFname2", "SearchLname2", new DateTime(), "Test Address", "Test City", "Test Provice", "TEST", "4444444444", "5555555555", "6666666666");
            p2.Insert();
            p2.Select();

            List<string> db = new List<string>();
            db.Add("street_address");

            List<string> s = new List<string>();
            s.Add("Test Add");

            Search search = new Search("Patient");
            search.UseInputs(db, s);

            List<Patient> p = Search.SearchPatient(search.GetBuiltQuery());
            Assert.True(p.Count >= 2);

            p1.Delete();
            p2.Delete();
        }

        [TestCase]
        public void TestSearchPatientWithValidQueryByExactCity()
        {
            Patient p1 = new Patient(10000, "SearchFname1", "SearchLname1", new DateTime(), "Test Address", "Test City1", "Test Provice", "TEST", "1111111111", "2222222222", "3333333333");
            p1.Insert();
            p1.Select();

            Patient p2 = new Patient(20000, "SearchFname2", "SearchLname2", new DateTime(), "Test Address", "Test City2", "Test Provice", "TEST", "4444444444", "5555555555", "6666666666");
            p2.Insert();
            p2.Select();

            List<string> db = new List<string>();
            db.Add("city");

            List<string> s = new List<string>();
            s.Add("Test City1");

            Search search = new Search("Patient");
            search.UseInputs(db, s);

            List<Patient> p = Search.SearchPatient(search.GetBuiltQuery());
            Assert.True(p[0].PatientID == p1.PatientID);

            p1.Delete();
            p2.Delete();
        }

        [TestCase]
        public void TestSearchPatientWithValidQueryByCloseCity()
        {
            Patient p1 = new Patient(10000, "SearchFname1", "SearchLname1", new DateTime(), "Test Address", "Test City", "Test Provice", "TEST", "1111111111", "2222222222", "3333333333");
            p1.Insert();
            p1.Select();

            Patient p2 = new Patient(20000, "SearchFname2", "SearchLname2", new DateTime(), "Test Address", "Test City", "Test Provice", "TEST", "4444444444", "5555555555", "6666666666");
            p2.Insert();
            p2.Select();

            List<string> db = new List<string>();
            db.Add("city");

            List<string> s = new List<string>();
            s.Add("Test");

            Search search = new Search("Patient");
            search.UseInputs(db, s);

            List<Patient> p = Search.SearchPatient(search.GetBuiltQuery());
            Assert.True(p.Count >= 2);

            p1.Delete();
            p2.Delete();
        }

        [TestCase]
        public void TestSearchPatientWithValidQueryByExactProvice()
        {
            Patient p1 = new Patient(10000, "SearchFname1", "SearchLname1", new DateTime(), "Test Address", "Test City", "Test Provice1", "TEST", "1111111111", "2222222222", "3333333333");
            p1.Insert();
            p1.Select();

            Patient p2 = new Patient(20000, "SearchFname2", "SearchLname2", new DateTime(), "Test Address", "Test City", "Test Provice2", "TEST", "4444444444", "5555555555", "6666666666");
            p2.Insert();
            p2.Select();

            List<string> db = new List<string>();
            db.Add("province");

            List<string> s = new List<string>();
            s.Add("Test Provice1");

            Search search = new Search("Patient");
            search.UseInputs(db, s);

            List<Patient> p = Search.SearchPatient(search.GetBuiltQuery());
            Assert.True(p[0].PatientID == p1.PatientID);

            p1.Delete();
            p2.Delete();
        }

        [TestCase]
        public void TestSearchPatientWithValidQueryByCloseProvice()
        {
            Patient p1 = new Patient(10000, "SearchFname1", "SearchLname1", new DateTime(), "Test Address", "Test City", "Test Provice", "TEST", "1111111111", "2222222222", "3333333333");
            p1.Insert();
            p1.Select();

            Patient p2 = new Patient(20000, "SearchFname2", "SearchLname2", new DateTime(), "Test Address", "Test City", "Test Provice", "TEST", "4444444444", "5555555555", "6666666666");
            p2.Insert();
            p2.Select();

            List<string> db = new List<string>();
            db.Add("province");

            List<string> s = new List<string>();
            s.Add("Test");

            Search search = new Search("Patient");
            search.UseInputs(db, s);

            List<Patient> p = Search.SearchPatient(search.GetBuiltQuery());
            Assert.True(p.Count >= 2);

            p1.Delete();
            p2.Delete();
        }

        [TestCase]
        public void TestSearchPatientWithValidQueryByExactPostalCode()
        {
            Patient p1 = new Patient(10000, "SearchFname1", "SearchLname1", new DateTime(), "Test Address", "Test City", "Test Provice", "TPC1", "1111111111", "2222222222", "3333333333");
            p1.Insert();
            p1.Select();

            Patient p2 = new Patient(20000, "SearchFname2", "SearchLname2", new DateTime(), "Test Address", "Test City", "Test Provice", "TPC2", "4444444444", "5555555555", "6666666666");
            p2.Insert();
            p2.Select();

            List<string> db = new List<string>();
            db.Add("postal_code");

            List<string> s = new List<string>();
            s.Add("TPC1");

            Search search = new Search("Patient");
            search.UseInputs(db, s);

            List<Patient> p = Search.SearchPatient(search.GetBuiltQuery());
            Assert.True(p[0].PatientID == p1.PatientID);

            p1.Delete();
            p2.Delete();
        }

        [TestCase]
        public void TestSearchPatientWithValidQueryByClosePostalCode()
        {
            Patient p1 = new Patient(10000, "SearchFname1", "SearchLname1", new DateTime(), "Test Address", "Test City", "Test Provice", "TEST", "1111111111", "2222222222", "3333333333");
            p1.Insert();
            p1.Select();

            Patient p2 = new Patient(20000, "SearchFname2", "SearchLname2", new DateTime(), "Test Address", "Test City", "Test Provice", "TEST", "4444444444", "5555555555", "6666666666");
            p2.Insert();
            p2.Select();

            List<string> db = new List<string>();
            db.Add("postal_code");

            List<string> s = new List<string>();
            s.Add("TEST");

            Search search = new Search("Patient");
            search.UseInputs(db, s);

            List<Patient> p = Search.SearchPatient(search.GetBuiltQuery());
            Assert.True(p.Count >= 2);

            p1.Delete();
            p2.Delete();
        }

        [TestCase]
        public void TestSearchPatientWithValidQueryByExactPhone()
        {
            Patient p1 = new Patient(10000, "SearchFname1", "SearchLname1", new DateTime(), "Test Address", "Test City", "Test Provice", "TEST", "1111111111", "2222222222", "3333333333");
            p1.Insert();
            p1.Select();

            Patient p2 = new Patient(20000, "SearchFname2", "SearchLname2", new DateTime(), "Test Address", "Test City", "Test Provice", "TEST", "4444444444", "5555555555", "6666666666");
            p2.Insert();
            p2.Select();

            List<string> db = new List<string>();
            db.Add("home_phone_no");

            List<string> s = new List<string>();
            s.Add("1111111111");

            Search search = new Search("Patient");
            search.UseInputs(db, s);

            List<Patient> p = Search.SearchPatient(search.GetBuiltQuery());
            Assert.True(p[0].PatientID == p1.PatientID);

            p1.Delete();
            p2.Delete();
        }

        [TestCase]
        public void TestSearchPatientWithValidQueryByClosePhone()
        {
            Patient p1 = new Patient(10000, "SearchFname1", "SearchLname1", new DateTime(), "Test Address", "Test City", "Test Provice", "TEST", "1111111111", "2222222222", "3333333333");
            p1.Insert();
            p1.Select();

            Patient p2 = new Patient(20000, "SearchFname2", "SearchLname2", new DateTime(), "Test Address", "Test City", "Test Provice", "TEST", "1444444444", "5555555555", "6666666666");
            p2.Insert();
            p2.Select();

            List<string> db = new List<string>();
            db.Add("home_phone_no");

            List<string> s = new List<string>();
            s.Add("1");

            Search search = new Search("Patient");
            search.UseInputs(db, s);

            List<Patient> p = Search.SearchPatient(search.GetBuiltQuery());
            Assert.True(p.Count >= 2);

            p1.Delete();
            p2.Delete();
        }

        [TestCase]
        public void TestSearchPatientWithValidQueryByMultipleExactParamters()
        {
            Patient p1 = new Patient(10000, "SearchFname1", "SearchLname1", new DateTime(), "Test Address", "Test City1", "Test Provice", "TEST", "1111111111", "2222222222", "3333333333");
            p1.Insert();
            p1.Select();

            Patient p2 = new Patient(20000, "SearchFname2", "SearchLname2", new DateTime(), "Test Address", "Test City2", "Test Provice", "TEST", "4444444444", "5555555555", "6666666666");
            p2.Insert();
            p2.Select();

            List<string> db = new List<string>();
            db.Add("fname");
            db.Add("lname");
            db.Add("city");

            List<string> s = new List<string>();
            s.Add("SearchFname1");
            s.Add("SearchLname1");
            s.Add("Test City1");

            Search search = new Search("Patient");
            search.UseInputs(db, s);

            List<Patient> p = Search.SearchPatient(search.GetBuiltQuery());
            //Assert.True(p[0].PatientID == p1.PatientID);

            p1.Delete();
            p2.Delete();
        }

        [TestCase]
        public void TestSearchPatientWithValidQueryByMultipleCloseParameters()
        {
            Patient p1 = new Patient(10000, "SearchFname1", "SearchLname1", new DateTime(), "Test Address", "Test City", "Test Provice", "TEST", "1111111111", "2222222222", "3333333333");
            p1.Insert();
            p1.Select();

            Patient p2 = new Patient(20000, "SearchFname2", "SearchLname2", new DateTime(), "Test Address", "Test City", "Test Provice", "TEST", "4444444444", "5555555555", "6666666666");
            p2.Insert();
            p2.Select();

            List<string> db = new List<string>();
            db.Add("fname");
            db.Add("lname");
            db.Add("city");

            List<string> s = new List<string>();
            s.Add("Search");
            s.Add("Search");
            s.Add("Test");

            Search search = new Search("Patient");
            search.UseInputs(db, s);

            List<Patient> p = Search.SearchPatient(search.GetBuiltQuery());
            Assert.True(p.Count >= 2);

            p1.Delete();
            p2.Delete();
        }
        #endregion

        #region Employee Search Tests
        [TestCase]
        public void TestSearchEmployeesWithValidQueryWithExactEid()
        {
            int eid1 = Employee.GenerateNextEid();
            Employee e1 = new Employee(eid1, "SearchFname1", "SearchLname1", "", "", 1);
            e1.Insert();

            int eid2 = Employee.GenerateNextEid();
            Employee e2 = new Employee(eid2, "SearchFname2", "SearchLname2", "", "", 1);
            e2.Insert();

            List<string> db = new List<string>();
            db.Add("eid");

            List<string> s = new List<string>();
            s.Add(eid1.ToString());

            Search search = new Search("Employee");
            search.UseInputs(db, s);

            List<Employee> e = Search.SearchEmployees(search.GetBuiltQuery());
            Assert.True(e[0].Eid == eid1);

            e1.Delete();
            e2.Delete();
        }

        [TestCase]
        public void TestSearchEmployeesWithValidQueryWithCloseEid()
        {
            int eid1 = Employee.GenerateNextEid();
            Employee e1 = new Employee(eid1, "SearchFname1", "SearchLname1", "", "", 1);
            e1.Insert();

            int eid2 = Employee.GenerateNextEid();
            Employee e2 = new Employee(eid2, "SearchFname2", "SearchLname2", "", "", 1);
            e2.Insert();

            List<string> db = new List<string>();
            db.Add("eid");

            List<string> s = new List<string>();
            int test = eid1 / 10;
            s.Add(test.ToString());

            Search search = new Search("Employee");
            search.UseInputs(db, s);

            List<Employee> e = Search.SearchEmployees(search.GetBuiltQuery());
            Assert.True(e.Count >= 2);

            e1.Delete();
            e2.Delete();
        }

        [TestCase]
        public void TestSearchEmployeesWithValidQueryWithExactFname()
        {
            int eid1 = Employee.GenerateNextEid();
            Employee e1 = new Employee(eid1, "SearchFname1", "SearchLname1", "", "", 1);
            e1.Insert();

            int eid2 = Employee.GenerateNextEid();
            Employee e2 = new Employee(eid2, "SearchFname2", "SearchLname2", "", "", 1);
            e2.Insert();

            List<string> db = new List<string>();
            db.Add("fname");

            List<string> s = new List<string>();
            s.Add("SearchFname1");

            Search search = new Search("Employee");
            search.UseInputs(db, s);

            List<Employee> e = Search.SearchEmployees(search.GetBuiltQuery());
            Assert.True(e[0].Eid == eid1);

            e1.Delete();
            e2.Delete();
        }

        [TestCase]
        public void TestSearchEmployeesWithValidQueryWithCloseFname()
        {
            int eid1 = Employee.GenerateNextEid();
            Employee e1 = new Employee(eid1, "SearchFname1", "SearchLname1", "", "", 1);
            e1.Insert();

            int eid2 = Employee.GenerateNextEid();
            Employee e2 = new Employee(eid2, "SearchFname2", "SearchLname2", "", "", 1);
            e2.Insert();

            List<string> db = new List<string>();
            db.Add("fname");

            List<string> s = new List<string>();
            s.Add("Search");

            Search search = new Search("Employee");
            search.UseInputs(db, s);

            List<Employee> e = Search.SearchEmployees(search.GetBuiltQuery());
            Assert.True(e.Count >= 2);

            e1.Delete();
            e2.Delete();
        }

        [TestCase]
        public void TestSearchEmployeesWithValidQueryWithExactLname()
        {
            int eid1 = Employee.GenerateNextEid();
            Employee e1 = new Employee(eid1, "SearchFname1", "SearchLname1", "", "", 1);
            e1.Insert();

            int eid2 = Employee.GenerateNextEid();
            Employee e2 = new Employee(eid2, "SearchFname2", "SearchLname2", "", "", 1);
            e2.Insert();

            List<string> db = new List<string>();
            db.Add("lname");

            List<string> s = new List<string>();
            s.Add("SearchLname1");

            Search search = new Search("Employee");
            search.UseInputs(db, s);

            List<Employee> e = Search.SearchEmployees(search.GetBuiltQuery());
            Assert.True(e[0].Eid == eid1);

            e1.Delete();
            e2.Delete();
        }

        [TestCase]
        public void TestSearchEmployeesWithValidQueryWithCloseLname()
        {
            int eid1 = Employee.GenerateNextEid();
            Employee e1 = new Employee(eid1, "SearchFname1", "SearchLname1", "", "", 1);
            e1.Insert();

            int eid2 = Employee.GenerateNextEid();
            Employee e2 = new Employee(eid2, "SearchFname2", "SearchLname2", "", "", 1);
            e2.Insert();

            List<string> db = new List<string>();
            db.Add("fname");

            List<string> s = new List<string>();
            s.Add("Search");

            Search search = new Search("Employee");
            search.UseInputs(db, s);

            List<Employee> e = Search.SearchEmployees(search.GetBuiltQuery());
            Assert.True(e.Count >= 2);

            e1.Delete();
            e2.Delete();
        }

        [TestCase]
        public void TestSearchEmployeesWithValidQueryWithExactSupervisorId()
        {
            int eid1 = Employee.GenerateNextEid();
            Employee e1 = new Employee(eid1, "SearchFname1", "SearchLname1", "", "", 100000);
            e1.Insert();

            int eid2 = Employee.GenerateNextEid();
            Employee e2 = new Employee(eid2, "SearchFname2", "SearchLname2", "", "", 120000);
            e2.Insert();

            List<string> db = new List<string>();
            db.Add("supervisor_id");

            List<string> s = new List<string>();
            s.Add("100000");

            Search search = new Search("Employee");
            search.UseInputs(db, s);

            List<Employee> e = Search.SearchEmployees(search.GetBuiltQuery());
            //Assert.True(e[0].Eid == eid1);

            e1.Delete();
            e2.Delete();
        }

        [TestCase]
        public void TestSearchEmployeesWithValidQueryWithCloseSupervisorId()
        {
            int eid1 = Employee.GenerateNextEid();
            Employee e1 = new Employee(eid1, "SearchFname1", "SearchLname1", "", "", 120001);
            e1.Insert();

            int eid2 = Employee.GenerateNextEid();
            Employee e2 = new Employee(eid2, "SearchFname2", "SearchLname2", "", "", 120000);
            e2.Insert();

            List<string> db = new List<string>();
            db.Add("supervisor_id");

            List<string> s = new List<string>();
            s.Add("12");

            Search search = new Search("Employee");
            search.UseInputs(db, s);

            List<Employee> e = Search.SearchEmployees(search.GetBuiltQuery());
            //Assert.True(e.Count >= 2);

            e1.Delete();
            e2.Delete();
        }

        [TestCase]
        public void TestSearchEmployeesWithValidQueryWithExactType()
        {
            int eid1 = Employee.GenerateNextEid();
            Employee e1 = new Employee(eid1, "SearchFname1", "SearchLname1", "", "Type1", 100000);
            e1.Insert();

            int eid2 = Employee.GenerateNextEid();
            Employee e2 = new Employee(eid2, "SearchFname2", "SearchLname2", "", "Type2", 120000);
            e2.Insert();

            List<string> db = new List<string>();
            db.Add("employee_type");

            List<string> s = new List<string>();
            s.Add("Type1");

            Search search = new Search("Employee");
            search.UseInputs(db, s);

            List<Employee> e = Search.SearchEmployees(search.GetBuiltQuery());
            //Assert.True(e[0].Eid == eid1);

            e1.Delete();
            e2.Delete();
        }

        [TestCase]
        public void TestSearchEmployeesWithValidQueryWithCloseType()
        {
            int eid1 = Employee.GenerateNextEid();
            Employee e1 = new Employee(eid1, "SearchFname1", "SearchLname1", "", "Type1", 100000);
            e1.Insert();

            int eid2 = Employee.GenerateNextEid();
            Employee e2 = new Employee(eid2, "SearchFname2", "SearchLname2", "", "Type2", 120000);
            e2.Insert();

            List<string> db = new List<string>();
            db.Add("employee_type");

            List<string> s = new List<string>();
            s.Add("Type");

            Search search = new Search("Employee");
            search.UseInputs(db, s);

            List<Employee> e = Search.SearchEmployees(search.GetBuiltQuery());
            //Assert.True(e.Count >= 2);

            e1.Delete();
            e2.Delete();
        }

        [TestCase]
        public void TestSearchEmployeesWithValidQueryWithGeneralSpecialty()
        {
            int eid1 = Employee.GenerateNextEid();
            Employee e1 = new Employee(eid1, "SearchFname1", "SearchLname1", "Spec1", "Doctor", 100000);
            e1.Insert();

            int eid2 = Employee.GenerateNextEid();
            Employee e2 = new Employee(eid2, "SearchFname2", "SearchLname2", "Spec2", "Nurse", 120000);
            e2.Insert();

            List<string> db = new List<string>();
            db.Add("specialty");

            List<string> s = new List<string>();
            s.Add("Spec1");

            Search search = new Search("Employee");
            search.UseInputs(db, s);

            List<Employee> e = Search.SearchEmployees(search.GetBuiltQuery());
            Assert.True(e[0].Eid == eid1);

            e1.Delete();
            e2.Delete();
        }

        [TestCase]
        public void TestSearchEmployeesWithValidQueryWithOtherSpecialty()
        {
            int eid1 = Employee.GenerateNextEid();
            Employee e1 = new Employee(eid1, "SearchFname1", "SearchLname1", "Spec1", "Doctor", 100000);
            e1.Insert();

            int eid2 = Employee.GenerateNextEid();
            Employee e2 = new Employee(eid2, "SearchFname2", "SearchLname2", "Spec2", "Nurse", 120000);
            e2.Insert();

            List<string> db = new List<string>();
            db.Add("specialty");

            List<string> s = new List<string>();
            s.Add("Spec");

            Search search = new Search("Employee");
            search.UseInputs(db, s);

            List<Employee> e = Search.SearchEmployees(search.GetBuiltQuery());
            Assert.True(e.Count >= 2);

            e1.Delete();
            e2.Delete();
        }

        [TestCase]
        public void TestSearchEmployeeWithMultipleExactFields()
        {
            int eid1 = Employee.GenerateNextEid();
            Employee e1 = new Employee(eid1, "SearchFname1", "SearchLname1", "Spec1", "Doctor", 100000);
            e1.Insert();

            int eid2 = Employee.GenerateNextEid();
            Employee e2 = new Employee(eid2, "SearchFname2", "SearchLname2", "Spec2", "Nurse", 120000);
            e2.Insert();

            List<string> db = new List<string>();
            db.Add("fname");
            db.Add("lname");
            db.Add("specialty");

            List<string> s = new List<string>();
            s.Add("SearchFname1");
            s.Add("SearchLname1");
            s.Add("Spec1");

            Search search = new Search("Employee");
            search.UseInputs(db, s);

            List<Employee> e = Search.SearchEmployees(search.GetBuiltQuery());
            Assert.True(e[0].Eid == eid1);

            e1.Delete();
            e2.Delete();
        }

        [TestCase]
        public void TestSearchEmployeeWithMultipleCloseFields()
        {
            int eid1 = Employee.GenerateNextEid();
            Employee e1 = new Employee(eid1, "SearchFname1", "SearchLname1", "Spec", "Doctor", 100000);
            e1.Insert();

            int eid2 = Employee.GenerateNextEid();
            Employee e2 = new Employee(eid2, "SearchFname2", "SearchLname2", "Spec", "Nurse", 120000);
            e2.Insert();

            List<string> db = new List<string>();
            db.Add("fname");
            db.Add("lname");
            db.Add("specialty");

            List<string> s = new List<string>();
            s.Add("Search");
            s.Add("Search");
            s.Add("Spec");

            Search search = new Search("Employee");
            search.UseInputs(db, s);

            List<Employee> e = Search.SearchEmployees(search.GetBuiltQuery());
            Assert.True(e.Count >= 2);

            e1.Delete();
            e2.Delete();
        }
        #endregion

        #region Other Tests
        [TestCase]
        public void TestUseInputs()
        {
            List<string> db = new List<string>();
            db.Add("DB Test1");
            db.Add("DB Test2");

            List<string> s = new List<string>();
            s.Add("S Test1");
            s.Add("S Test2");

            Search search = new Search("Test");

            search.UseInputs(db, s);

            string expected = "SELECT * FROM Test WHERE (DB Test1 LIKE '%S Test1%') AND (DB Test2 LIKE '%S Test2%')";
            string actual = search.GetBuiltQuery();

            Assert.True(actual.Equals(expected));
        }

        [TestCase]
        public void TestBuildStringQueryWithTwoArguments()
        {
            Search s = new Search("Patient");
            s.BuildStringQuery("Test Arg1", "Test Arg2");

            string q = s.GetBuiltQuery();

            Assert.True(q.Equals("SELECT * FROM Patient WHERE (Test Arg1 LIKE '%Test Arg2%')"));
        }

        [TestCase]
        public void TestBuildStringQueryWithSingleArgument()
        {
            Search s = new Search("Patient");
            s.BuildStringQuery("Test Arg");

            string q = s.GetBuiltQuery();

            Assert.True(q.Equals("SELECT * FROM Patient WHERE Test Arg"));
        }

        [TestCase]
        public void TestGetBuiltQuery()
        {
            Search s = new Search("Test");
            string q = s.GetBuiltQuery();

            Assert.True(q.Equals("SELECT * FROM Test WHERE "));
        }
        #endregion

    }
}
