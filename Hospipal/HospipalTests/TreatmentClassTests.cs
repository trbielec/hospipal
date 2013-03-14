using System;
using NUnit.Framework;
using Hospipal.Database_Class;
using System.Collections.Generic;

namespace HospipalTests
{
    [TestFixture]
    public class TreatmentClassTests
    {
        [TestCase]
        public void TestTreatmentSelect()
        {
            Treatment.Insert(100000, "", 1, 1, 1, "", "", 1);
            List<Treatment> list = Treatment.GetTreatments(100000);
            
            Treatment treatment = list[0];
            Assert.True(treatment.Select());

            treatment.Delete();
        }

        [TestCase]
        public void TestTreamentInsert()
        {
            Assert.True(Treatment.Insert(100000, "", 1, 1, 1, "", "", 1));
            List<Treatment> list = Treatment.GetTreatments(100000);

            Treatment treatment = list[0];
            treatment.Delete();
        }

        [TestCase]
        public void TestTreatmentUpdate()
        {
            Treatment.Insert(100000, "", 1, 1, 1, "", "", 1);
            List<Treatment> list = Treatment.GetTreatments(100000);

            Treatment treatment = list[0];
            treatment.Notes = "TEST NOTES";
            Assert.True(Treatment.Update(treatment.TreatmentID, treatment.Type, 2, 2, 2, treatment.Time, treatment.Notes, treatment.Doctor));

            treatment.Delete();
        }

        [TestCase]
        public void TestTreatmentDelete()
        {
            Treatment.Insert(100000, "", 1, 1, 1, "", "", 1);
            List<Treatment> list = Treatment.GetTreatments(100000);

            Treatment treatment = list[0];
            Assert.True(treatment.Delete());
        }

        [TestCase]
        public void TestGetTreatments()
        {
            Treatment.Insert(100000, "", 1, 1, 1, "", "", 1);
            List<Treatment> list = Treatment.GetTreatments(100000);
            Assert.True(list.Count >= 1);

            Treatment treatment = list[0];
            treatment.Delete();
        }

        [TestCase]
        public void TestGetTreatmentTypes()
        {
            Assert.IsNotEmpty(Treatment.GetTreatmentTypes());
        }
    }
}
