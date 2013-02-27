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
            patient = new Patient(40);

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
            /*
            patient.FirstName = "Test1";
            Assert.True(patient.Update());

            patient.HealthCareNo = 40;
            Assert.False(patient.Update());

            Assert.True(patient.Delete());
            Assert.False(patient.Delete());*/
        }
    }
}
