using System;
using NUnit.Framework;
using Hospipal.Database_Class;
using System.Collections.Generic;
using Hospipal;
using MySql.Data.Types;

namespace HospipalTests
{
    [TestFixture]
    class PrescriptionClassTests
    {
        [TestCase]
        public void TestPrescriptionSelect()
        {
            Prescription p = new Prescription(100000, 1,"Test-Prescription", "Test-Notes", new DateTime(), new DateTime(),"Test");

            p.Insert();
            List<Prescription> list = Prescription.GetPrescriptions(100000, "Test");

            Prescription prescription = list[0];
            Assert.True(prescription.Select());

            prescription.Delete();
        }

        [TestCase]
        public void TestPrescriptionInsert()
        {
            Prescription p = new Prescription(100000, 1, "Test-Prescription", "Test-Notes", new DateTime(), new DateTime(), "Test");
            Assert.True(p.Insert());
            List<Prescription> list = Prescription.GetPrescriptions(100000, "Test");

            Prescription prescription = list[0];
            prescription.Delete();
        }

        [TestCase]
        public void TestPrescriptionUpdate()
        {
            Prescription p = new Prescription(100000, 1, "Test-Prescription", "Test-Notes", new DateTime(), new DateTime(), "Test");

            p.Insert();
            List<Prescription> list = Prescription.GetPrescriptions(100000, "Test");

            Prescription prescription = list[0];
            prescription.Notes = "TEST NOTES";
            Assert.True(prescription.Update());

            prescription.Delete();
        }

        [TestCase]
        public void TestPrescriptionDelete()
        {
            Prescription p = new Prescription(100000, 1, "Test-Prescription", "Test-Notes", new DateTime(), new DateTime(), "Test");

            p.Insert();
            List<Prescription> list = Prescription.GetPrescriptions(100000, "Test");

            Prescription prescription = list[0];
            Assert.True(prescription.Delete());
        }

        [TestCase]
        public void TestGetPrescriptions()
        {
            Prescription p = new Prescription(100000, 1, "Test-Prescription", "Test-Notes", new DateTime(), new DateTime(), "Test");

            p.Insert();
            List<Prescription> list = Prescription.GetPrescriptions(100000, "Test");
            Assert.True(list.Count >= 1);

            Prescription prescription = list[0];
            prescription.Delete();
        }

        [TestCase]
        public void TestCheckDatesForStatusChanges()
        {
            Prescription p = new Prescription(100000, 1, "Test-Prescription", "Test-Notes", new DateTime(2013,4,6), new DateTime(2013,4,6), "Upcoming");
            p.Insert();
            Prescription.CheckDatesForStatusChanges();
            p.Select();
            Assert.True(p.Status == "History");
            p.Delete();

        }
        
        [TestCase]
        public void TestGenerateNextRtid()
        {
            List<object[]> obj = Database.Select("SELECT Auto_increment FROM information_schema.tables WHERE table_name= 'Prescription'AND table_schema = DATABASE();");
            int next = Convert.ToInt32(obj[0][0]);
            Assert.True(Prescription.GenerateNextprid() == next);
        }

        [TestCase]
        public void TestSelectWithNoTid()
        {
            Prescription p = new Prescription();
            Assert.False(p.Select());
        }

        #region Get/Set Tests
        [TestCase]
        public void TestGetSetPatientId()
        {
            Prescription r = new Prescription();
            r.PatientID = 1;
            Assert.True(r.PatientID == 1);
        }

        [TestCase]
        public void TestGetSetName()
        {
            Prescription r = new Prescription();
            r.PrescriptionName = "Test";
            Assert.True(r.PrescriptionName == "Test");
        }

        [TestCase]
        public void TestGetSetStartDateString()
        {
            Prescription r = new Prescription();
            r.StartDate = new MySqlDateTime(new DateTime(2012, 12, 12));
            String date = r.StartDate.ToString();
            Assert.True(r.StartDateToShortDateString == date);
        }

        [TestCase]
        public void TestGetSetEndDateString()
        {
            Prescription r = new Prescription();
            r.EndDate = new MySqlDateTime(new DateTime(2012, 12, 12));
            String date = r.EndDate.ToString();
            Assert.True(r.EndDateToShortDateString == date);
        }
        [TestCase]
        public void TestGetSetNotes()
        {
            Prescription r = new Prescription();
            r.Notes = "Test";
            Assert.True(r.Notes == "Test");
        }

        [TestCase]
        public void TestGetSetDoctor()
        {
            Prescription r = new Prescription();
            r.Doctor = 1;
            Assert.True(r.Doctor == 1);
        }

        [TestCase]
        public void TestGetSetStatus()
        {
            Prescription r = new Prescription();
            r.Status = "Test";
            Assert.True(r.Status == "Test");
        }
        #endregion
    }
}