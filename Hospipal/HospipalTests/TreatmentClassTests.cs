using System;
using NUnit.Framework;
using Hospipal.Database_Class;

namespace HospipalTests
{
    [TestFixture]
    public class TreatmentClassTests
    {
        private Treatment treatment;

        /*
        [TestCase]
        public void TestInitalizeMethodsWithValidPatient()
        {
            treatment = new Treatment(1);

            Assert.IsNotEmpty(treatment.initializeDoctorList());
            Assert.IsNotEmpty(treatment.initializeTreatmentHistory());
            Assert.IsNotEmpty(treatment.initializeTreatmentList());
        }*/

        [TestCase]
        public void TestInitalizeMethodsWithInvalidPatient()
        {
            treatment = new Treatment(-1);

            Assert.IsEmpty(treatment.initializeTreatmentHistory());
        }
    }
}
