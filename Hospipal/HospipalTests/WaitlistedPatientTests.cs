using System;
using NUnit.Framework;
using Hospipal;
using Hospipal.Database_Class;
using System.Collections.Generic;

namespace HospipalTests
{
    [TestFixture]
    public class WaitlistedPatientTests
    {
        [TestCase]
        public void TestAssignPatientToBed()
        {
            Ward w = new Ward("UnitTestWard", "UTW");
            w.Insert();
            Room r = new Room(1, "UnitTestWard", 1);
            r.Insert();
            Bed b = new Bed(1, Bed.States.Available, 0, 1, "", "UnitTestWard");
            b.Insert();
            Patient p = new Patient(10000, "Test", "Test", new DateTime(), "", "", "", "", "", "", "");
            p.Insert();
            p.Select();
            List<Treatment> list = Treatment.GetTreatments(10000,"");
            Treatment t = list[0];

            WaitlistedPatient.AddPatientToWaitlist(p.PatientID, "UnitTestWard", "High", t.TreatmentID);
            List<WaitlistedPatient> patients = WaitlistedPatient.GetWaitlistedPatientsForWard("UnitTestWard");
            List<Bed> bed = WaitlistedPatient.GetOpenBedsForWard("UnitTestWard");

            Assert.True(patients[0].AssignPatientToBed(bed[0].bedNo));

            patients[0].RemovedPatientFromWaitlist();
            t.Delete();
            p.Delete();
            b.Delete();
            r.Delete();
            w.Delete();
        }

        [TestCase]
        public void TestRemovePatientFromWaitlist()
        {
            Ward w = new Ward("UnitTestWard", "UTW");
            w.Insert();
            Room r = new Room(1, "UnitTestWard", 1);
            r.Insert();
            Bed b = new Bed(1, Bed.States.Available, 0, 1, "", "UnitTestWard");
            b.Insert();
            Patient p = new Patient(10000, "Test", "Test", new DateTime(), "", "", "", "", "", "", "");
            p.Insert();
            p.Select();
            Treatment treat = new Treatment(10000, "TestTreatment", 1, 1, 1, "", "", 1,"History");
            treat.Insert();
            List<Treatment> list = Treatment.GetTreatments(10000,"");
            Treatment t = list[0];

            WaitlistedPatient.AddPatientToWaitlist(p.PatientID, "UnitTestWard", "High", t.TreatmentID);
            List<WaitlistedPatient> patients = WaitlistedPatient.GetWaitlistedPatientsForWard("UnitTestWard");
            List<Bed> bed = WaitlistedPatient.GetOpenBedsForWard("UnitTestWard");

            patients[0].AssignPatientToBed(bed[0].bedNo);

            Assert.True(patients[0].RemovedPatientFromWaitlist());
            t.Delete();
            p.Delete();
            b.Delete();
            r.Delete();
            w.Delete();
        }

        [TestCase]
        public void TestGetWaitlistForWardWithInvalidWard()
        {
            List<WaitlistedPatient> patients = WaitlistedPatient.GetWaitlistedPatientsForWard("UnitTestWard");
            Assert.True(patients.Count == 0);
        }

        [TestCase]
        public void TestGetWaitlistForWardWithValidWard()
        {
            Ward w = new Ward("UnitTestWard", "UTW");
            w.Insert();
            Room r = new Room(1, "UnitTestWard", 1);
            r.Insert();
            Bed b = new Bed(1, Bed.States.Available, 0, 1, "", "UnitTestWard");
            b.Insert();
            Patient p = new Patient(10000, "Test", "Test", new DateTime(), "", "", "", "", "", "", "");
            p.Insert();
            p.Select();
            Treatment treat = new Treatment(100000, "UnitTestTreatment", 1, 1, 1, "", "", 1, "History");
            treat.Insert();
            List<Treatment> list = Treatment.GetTreatments(10000,"");
            Treatment t = list[0];

            //WaitlistedPatient.AddPatientToWaitlist(p.PatientID, "UnitTestWard", "High", t.TreatmentID,t.Type);
            List<WaitlistedPatient> patients = WaitlistedPatient.GetWaitlistedPatientsForWard("UnitTestWard");

            Assert.True(patients.Count >= 1);

            patients[0].RemovedPatientFromWaitlist();
            t.Delete();
            p.Delete();
            b.Delete();
            r.Delete();
            w.Delete();
        }

        [TestCase]
        public void TestGetOpenBedsForWardWithInvalidWard()
        {
            List<Bed> bed = WaitlistedPatient.GetOpenBedsForWard("UnitTestWard1");
            Assert.True(bed.Count == 0);
        }

        [TestCase]
        public void TestGetOpenBedsForWardWithValidWard()
        {
            Ward w = new Ward("UnitTestWard", "UTW");
            w.Insert();
            Room r = new Room(1, "UnitTestWard", 1);
            r.Insert();
            Bed b = new Bed(1, Bed.States.Available, 0, 1, "", "UnitTestWard");
            b.Insert();

            List<Bed> bed = WaitlistedPatient.GetOpenBedsForWard("UnitTestWard");
            Assert.True(bed.Count >= 1);

            b.Delete();
            r.Delete();
            w.Delete();
        }
    }
}
