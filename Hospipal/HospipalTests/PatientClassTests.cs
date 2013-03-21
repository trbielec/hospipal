using System;
using NUnit.Framework;
using Hospipal.Database_Class;
using System.Collections.Generic;

namespace HospipalTests
{
    [TestFixture]
    public class PatientClassTests
    {
        [TestCase]
        public void TestPatientSelect()
        {
            Patient patient = new Patient(10000000, "UnitTestPatient", "UnitTestLName", new DateTime(), "", "", "", "", "", "", "");
            patient.Insert();

            Assert.True(patient.Select());

            patient.Delete();
        }

        [TestCase]
        public void TestPatientInsert()
        {
            Patient patient = new Patient(10000000, "UnitTestPatient", "UnitTestLName", new DateTime(), "", "", "", "", "", "", "");
            Assert.True(patient.Insert());

            patient.Delete();
        }

        [TestCase]
        public void TestPatientUpdate()
        {
            Patient patient = new Patient(10000000, "UnitTestPatient", "UnitTestLName", new DateTime(), "", "", "", "", "", "", "");
            patient.Insert();
            patient.Select();

            patient.FirstName = "RenamedPatient";
            Assert.True(patient.Update());

            patient.Delete();
        }

        [TestCase]
        public void TestPatientDelete()
        {
            Patient patient = new Patient(10000000, "UnitTestPatient", "UnitTestLName", new DateTime(), "", "", "", "", "", "", "");
            patient.Insert();

            Assert.True(patient.Delete());
        }

        [TestCase]
        public void TestGetPatients()
        {
            Patient patient = new Patient(10000000, "UnitTestPatient", "UnitTestLName", new DateTime(), "", "", "", "", "", "", "");
            patient.Insert();

            List<Patient> p = Patient.GetPatients();
            Assert.IsNotEmpty(p);

            patient.Delete();
        }

        [TestCase]
        public void TestGetPatientsWithInvalidPid()
        {
            Patient p = new Patient();
            Assert.False(p.GetPatient(-1));
        }

        [TestCase]
        public void TestGetPatientsWithValidPid()
        {
            Patient p = new Patient(10000);
            p.Insert();
            p.Select();
            Assert.True(p.GetPatient(p.PatientID));
            p.Delete();
        }

        #region Get/Set Tests
        [TestCase]
        public void TestGetSetPatientID()
        {
            Patient p = new Patient();
            p.PatientID = 1;
            Assert.True(p.PatientID == 1);
        }

        [TestCase]
        public void TestGetSetHealthCareNo()
        {
            Patient p = new Patient(1);
            Assert.True(p.HealthCareNo == 1);
            p.HealthCareNo = 2;
            Assert.True(p.HealthCareNo == 2);
        }

        [TestCase]
        public void TestGetSetFname()
        {
            Patient p = new Patient();
            p.FirstName = "Test";
            Assert.True(p.FirstName == "Test");
        }

        [TestCase]
        public void TestGetSetLname()
        {
            Patient p = new Patient();
            p.LastName = "Test";
            Assert.True(p.LastName == "Test");
        }

        [TestCase]
        public void TestGetSetDob()
        {
            Patient p = new Patient();
            DateTime d = new DateTime(2013, 2, 2);
            p.DOB = d;
            Assert.True(p.DOB == d);
        }

        [TestCase]
        public void TestGetSetAddress()
        {
            Patient p = new Patient();
            p.StreetAddress = "Test";
            Assert.True(p.StreetAddress == "Test");
        }

        [TestCase]
        public void TestGetSetCity()
        {
            Patient p = new Patient();
            p.City = "Test";
            Assert.True(p.City == "Test");
        }

        [TestCase]
        public void TestGetSetProvince()
        {
            Patient p = new Patient();
            p.Province = "Test";
            Assert.True(p.Province == "Test");
        }

        [TestCase]
        public void TestGetSetPostalCode()
        {
            Patient p = new Patient();
            p.PostalCode = "Test";
            Assert.True(p.PostalCode == "Test");
        }

        [TestCase]
        public void TestGetSetHomePhone()
        {
            Patient p = new Patient();
            p.HomePhoneNo = "Test";
            Assert.True(p.HomePhoneNo == "Test");
        }

        [TestCase]
        public void TestGetSetWorkPhone()
        {
            Patient p = new Patient();
            p.WorkPhoneNo = "Test";
            Assert.True(p.WorkPhoneNo == "Test");
        }

        [TestCase]
        public void TestGetSetMobilePhone()
        {
            Patient p = new Patient();
            p.MobilePhoneNo = "Test";
            Assert.True(p.MobilePhoneNo == "Test");
        }

        [TestCase]
        public void TestGetDob()
        {
            Patient p = new Patient();
            p.DOB = new DateTime(2013, 2, 3);
            Assert.True(p.getDOB == "2/3/2013");
        }
        #endregion
    }
}
