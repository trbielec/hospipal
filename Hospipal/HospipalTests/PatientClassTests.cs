using System;
using NUnit.Framework;
using Hospipal.Database_Class;

namespace HospipalTests
{
    [TestFixture]
    public class PatientClassTests
    {
        private Patient patient;

        [TestCase]
        public void TestSelectMethodWithValidPatient()
        {
            patient = new Patient(10);

            Assert.True(patient.Select());
        }

        [TestCase]
        public void TestSelectMethodWithInvalidPatient()
        {
            patient = new Patient(-1);
            Assert.False(patient.Select());
        }

        [TestCase]
        public void TestInsetUpdateDeletePatient()
        {
            patient = new Patient(1000, "Test", "Case", new DateTime(), "Test Add", "Test City", "Test Province", "Test PC", "Test homephone", "Test mobile", "test work");

            Assert.True(patient.Insert());
            Assert.False(patient.Insert());

            patient = new Patient(1000);
            patient.FirstName = "Test1";
            Assert.True(patient.Update());

            patient.HealthCareNo = 10;
            Assert.False(patient.Update());

            patient = new Patient(1000);
            Assert.True(patient.Delete());
        }

        [TestCase]
        public void TestGetPatients()
        {
            Assert.NotNull(Patient.GetPatients());
        }
    }
}
