using System;
using NUnit.Framework;
using Hospipal.Database_Class;
using System.Collections.Generic;
using Hospipal;

namespace HospipalTests
{
    [TestFixture]
    public class TreatmentClassTests
    {
        [SetUp]
        public void SetDB()
        {
            Database.useTestDB();
            Console.WriteLine("Treatment Tests!");
        }

        [TestCase]
        public void TestTreatmentSelect()
        {
            Treatment t = new Treatment(100000, "UnitTestTreatment", 1, 1, 1, "", "", 1,"History1");

            t.Insert();
            List<Treatment> list = Treatment.GetTreatments(100000,"History1");
            
            Treatment treatment = list[0];
            Assert.True(treatment.Select());

            treatment.Delete();
        }

        [TestCase]
        public void TestTreamentInsert()
        {
            Treatment t = new Treatment(100000, "UnitTestTreatment", 1, 1, 1, "", "", 1, "History2");
            Assert.True(t.Insert());
            List<Treatment> list = Treatment.GetTreatments(100000, "History2");

            Treatment treatment = list[0];
            treatment.Delete();
        }

        [TestCase]
        public void TestTreatmentUpdate()
        {
            Treatment t = new Treatment(100000, "UnitTestTreatment", 1, 1, 1, "", "", 1, "History3");
            t.Insert();
            List<Treatment> list = Treatment.GetTreatments(100000, "History3");

            Treatment treatment = list[0];
            treatment.Notes = "TEST NOTES";
            Assert.True(treatment.Update());

            treatment.Delete();
        }

        [TestCase]
        public void TestTreatmentDelete()
        {
            Treatment t = new Treatment(100000, "UnitTestTreatment", 1, 1, 1, "", "", 1, "History4");
            t.Insert();
            List<Treatment> list = Treatment.GetTreatments(100000, "History4");

            Treatment treatment = list[0];
            Assert.True(treatment.Delete());
        }

        [TestCase]
        public void TestGetTreatments()
        {
            Treatment t = new Treatment(100000, "UnitTestTreatment", 1, 1, 1, "", "", 1, "History5");
            t.Insert();
            List<Treatment> list = Treatment.GetTreatments(100000, "History5");
            Assert.True(list.Count >= 1);

            Treatment treatment = list[0];
            treatment.Delete();
        }

        [TestCase]
        public void TestGetTreatmentTypes()
        {
            List<string> list = Treatment.GetTreatmentTypes();
            Assert.True(list.Count >= 1);
        }

        [TestCase]
        public void TestGenerateNextRtid()
        {
            List<object[]> obj = Database.Select("SELECT Auto_increment FROM information_schema.tables WHERE table_name= 'ReceivesTreatment'AND table_schema = DATABASE();");
            int next = Convert.ToInt32(obj[0][0]);
            Assert.True(Treatment.GenerateNextrtid() == next);
        }

        [TestCase]
        public void TestSelectWithNoTid()
        {
            Treatment t = new Treatment();
            Assert.False(t.Select());
        }

        #region Get/Set Tests
        [TestCase]
        public void TestGetSetPatientId()
        {
            Treatment r = new Treatment();
            r.PatientID = 1;
            Assert.True(r.PatientID == 1);
        }

        [TestCase]
        public void TestGetSetType()
        {
            Treatment r = new Treatment();
            r.Type = "Test";
            Assert.True(r.Type == "Test");
        }

        [TestCase]
        public void TestGetSetDate()
        {
            Treatment r = new Treatment();
            DateTime d = new DateTime(2013, 2, 3);
            r.Date = d;
            Assert.True(r.Date.CompareTo(d) == 0);
        }

        [TestCase]
        public void TestGetSetDateString()
        {
            Treatment r = new Treatment();
            r.Date = new DateTime(2012, 12, 12);
            String date = r.Date.ToShortDateString();
            Assert.True(r.DateToShortDateString == date); 
        }

        [TestCase]
        public void TestGetSetTime()
        {
            Treatment r = new Treatment();
            r.Time = "Test";
            Assert.True(r.Time == "Test");
        }

        [TestCase]
        public void TestGetSetNotes()
        {
            Treatment r = new Treatment();
            r.Notes = "Test";
            Assert.True(r.Notes == "Test");
        }

        [TestCase]
        public void TestGetSetDoctor()
        {
            Treatment r = new Treatment();
            r.Doctor = 1;
            Assert.True(r.Doctor == 1);
        }

        [TestCase]
        public void TestGetSetStatus()
        {
            Treatment r = new Treatment();
            r.Status = "Test";
            Assert.True(r.Status == "Test");
        }
        #endregion
    }
}
