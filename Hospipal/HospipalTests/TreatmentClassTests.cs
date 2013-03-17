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
            Treatment t = new Treatment(100000, "UnitTestTreatment", 1, 1, 1, "", "", 1,"History");

            t.Insert();
            List<Treatment> list = Treatment.GetTreatments(100000,"History");
            
            Treatment treatment = list[0];
            Assert.True(treatment.Select());

            treatment.Delete();
        }

        [TestCase]
        public void TestTreamentInsert()
        {
            Treatment t = new Treatment(100000, "UnitTestTreatment", 1, 1, 1, "", "", 1, "History");
            t.Insert();
            Assert.True(t.Insert());
            List<Treatment> list = Treatment.GetTreatments(100000, "History");

            Treatment treatment = list[0];
            treatment.Delete();
        }

        [TestCase]
        public void TestTreatmentUpdate()
        {
            Treatment t = new Treatment(100000, "UnitTestTreatment", 1, 1, 1, "", "", 1, "History");
            t.Insert();
            List<Treatment> list = Treatment.GetTreatments(100000, "History");

            Treatment treatment = list[0];
            treatment.Notes = "TEST NOTES";
            Assert.True(treatment.Update());

            treatment.Delete();
        }

        [TestCase]
        public void TestTreatmentDelete()
        {
            Treatment t = new Treatment(100000, "UnitTestTreatment", 1, 1, 1, "", "", 1, "History");
            t.Insert();
            List<Treatment> list = Treatment.GetTreatments(100000, "History");

            Treatment treatment = list[0];
            Assert.True(treatment.Delete());
        }

        [TestCase]
        public void TestGetTreatments()
        {
            Treatment t = new Treatment(100000, "UnitTestTreatment", 1, 1, 1, "", "", 1, "History");
            t.Insert();
            List<Treatment> list = Treatment.GetTreatments(100000, "History");
            Assert.True(list.Count >= 1);

            Treatment treatment = list[0];
            treatment.Delete();
        }
        /*
        [TestCase]
        public void TestGetTreatmentTypes()
        {
            Assert.IsNotEmpty(Treatment.GetTreatmentTypes());
        }
         */  //No Treatment Types is not a test.
    }
}
