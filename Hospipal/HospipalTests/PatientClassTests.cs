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
    }
}
