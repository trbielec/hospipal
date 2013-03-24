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
            Ward w = new Ward("UnitTestWard-WLP", "UTW");
            w.Insert();
            Room r = new Room(1, "UTW", 1);
            r.Insert();
            Bed b = new Bed(1, Bed.States.Available, 0, 1, "", "UTW");
            b.Insert();
            Patient p = new Patient(10000, "Test-WLP", "Test-WLP", new DateTime(), "", "", "", "", "", "", "");
            p.Insert();
            p.Select();
            Treatment treat = new Treatment(p.PatientID, "UnitTestTreatemnt-WLP", 1, 1, 1, "", "", 1, "WLP");
            treat.Insert();

            List<Treatment> list = Treatment.GetTreatments(p.PatientID,"WLP");
            Treatment t = list[0];
            WaitlistedPatient.AddPatientToWaitlist(p.PatientID, "UTW", "High", t.TreatmentID);
            List<WaitlistedPatient> patients = WaitlistedPatient.GetWaitlistedPatientsForWard("UTW");
            List<Bed> bed = WaitlistedPatient.GetOpenBedsForWard("UTW");

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
            Ward w = new Ward("UnitTestWard-WLP", "UTW");
            w.Insert();
            Room r = new Room(1, "UTW", 1);
            r.Insert();
            Bed b = new Bed(1, Bed.States.Available, 0, 1, "", "UTW");
            b.Insert();
            Patient p = new Patient(10000, "Test-WLP", "Test-WLP", new DateTime(), "", "", "", "", "", "", "");
            p.Insert();
            p.Select();
            Treatment treat = new Treatment(p.PatientID, "UTW", 1, 1, 1, "", "", 1, "WLP");
            treat.Insert();
            List<Treatment> list = Treatment.GetTreatments(p.PatientID, "WLP");
            Treatment t = list[0];

            WaitlistedPatient.AddPatientToWaitlist(p.PatientID, "UTW", "High", t.TreatmentID);
            List<WaitlistedPatient> patients = WaitlistedPatient.GetWaitlistedPatientsForWard("UTW");
            List<Bed> bed = WaitlistedPatient.GetOpenBedsForWard("UTW");

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
            List<WaitlistedPatient> patients = WaitlistedPatient.GetWaitlistedPatientsForWard("UnitTestWard-WLP");
            Assert.True(patients.Count == 0);
        }

        [TestCase]
        public void TestGetWaitlistForWardWithValidWard()
        {
            Ward w = new Ward("UnitTestWard-WLP", "UTW");
            w.Insert();
            Room r = new Room(1, "UnitTestWard-WLP", 1);
            r.Insert();
            Bed b = new Bed(1, Bed.States.Available, 0, 1, "", "UnitTestWard-WLP");
            b.Insert();
            Patient p = new Patient(10000, "Test-WLP", "Test-WLP", new DateTime(), "", "", "", "", "", "", "");
            p.Insert();
            p.Select();
            Treatment treat = new Treatment(p.PatientID, "UnitTestTreatment-WLP", 1, 1, 1, "", "", 1, "WLP");
            treat.Insert();
            List<Treatment> list = Treatment.GetTreatments(p.PatientID, "WLP");
            Treatment t = list[0];

            WaitlistedPatient.AddPatientToWaitlist(p.PatientID, "UnitTestWard-WLP", "High", t.TreatmentID);
            List<WaitlistedPatient> patients = WaitlistedPatient.GetWaitlistedPatientsForWard("UnitTestWard-WLP");

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
            List<Bed> bed = WaitlistedPatient.GetOpenBedsForWard("UnitTestWard-WLP");
            Assert.True(bed.Count == 0);
        }

        [TestCase]
        public void TestGetOpenBedsForWardWithValidWard()
        {
            Ward w = new Ward("UnitTestWard-WLP", "UTW");
            w.Insert();
            Room r = new Room(1, "UnitTestWard-WLP", 1);
            r.Insert();
            Bed b = new Bed(1, Bed.States.Available, 0, 1, "", "UnitTestWard-WLP");
            b.Insert();

            List<Bed> bed = WaitlistedPatient.GetOpenBedsForWard("UnitTestWard-WLP");
            Assert.True(bed.Count >= 1);

            b.Delete();
            r.Delete();
            w.Delete();
        }

        [TestCase]
        public void TestSelectWithValidRtid()
        {
            Ward w = new Ward("UnitTestWard-WLP", "UTW");
            w.Insert();
            Room r = new Room(1, "UnitTestWard-WLP", 1);
            r.Insert();
            Bed b = new Bed(1, Bed.States.Available, 0, 1, "", "UnitTestWard-WLP");
            b.Insert();
            Patient p = new Patient(10000, "Test-WLP", "Test-WLP", new DateTime(), "", "", "", "", "", "", "");
            p.Insert();
            p.Select();
            Treatment treat = new Treatment(p.PatientID, "UnitTestTreatment-WLP", 1, 1, 1, "", "", 1, "WLP");
            treat.Insert();
            List<Treatment> list = Treatment.GetTreatments(p.PatientID, "WLP");
            Treatment t = list[0];

            WaitlistedPatient.AddPatientToWaitlist(p.PatientID, "UnitTestWard-WLP", "High", t.TreatmentID);
            List<WaitlistedPatient> patients = WaitlistedPatient.GetWaitlistedPatientsForWard("UnitTestWard-WLP");

            WaitlistedPatient waitlist = new WaitlistedPatient(t.TreatmentID);

            Assert.True(waitlist.Pid == p.PatientID);

            patients[0].RemovedPatientFromWaitlist();
            t.Delete();
            p.Delete();
            b.Delete();
            r.Delete();
            w.Delete();
        }

        [TestCase]
        public void TestEditPatientInWaitlist()
        {
            Ward w = new Ward("UnitTestWard-WLP", "UTW");
            w.Insert();
            Room r = new Room(1, "UnitTestWard-WLP", 1);
            r.Insert();
            Bed b = new Bed(1, Bed.States.Available, 0, 1, "", "UnitTestWard-WLP");
            b.Insert();
            Patient p = new Patient(10000, "Test-WLP", "Test-WLP", new DateTime(), "", "", "", "", "", "", "");
            p.Insert();
            p.Select();
            Treatment treat = new Treatment(p.PatientID, "UnitTestTreatment-WLP", 1, 1, 1, "", "", 1, "WLP");
            treat.Insert();
            List<Treatment> list = Treatment.GetTreatments(p.PatientID, "WLP");
            Treatment t = list[0];

            WaitlistedPatient.AddPatientToWaitlist(p.PatientID, "UnitTestWard-WLP", "High", t.TreatmentID);
            List<WaitlistedPatient> patients = WaitlistedPatient.GetWaitlistedPatientsForWard("UnitTestWard-WLP");

            Assert.True(WaitlistedPatient.EditPatientInWaitlist(p.PatientID, "UTW", "High", t.TreatmentID));

            patients[0].RemovedPatientFromWaitlist();
            t.Delete();
            p.Delete();
            b.Delete();
            r.Delete();
            w.Delete();
        }

        [TestCase]
        public void TestSelectWithInvalidRtid()
        {
            WaitlistedPatient w = new WaitlistedPatient(-1);
            Assert.True(w.Pid == 0);
        }

        #region Get/Set Tests

        [TestCase]
        public void TestGetSetPid()
        {
            WaitlistedPatient w = new WaitlistedPatient();
            w.Pid = 1;
            Assert.True(w.Pid == 1);
        }

        [TestCase]
        public void TestGetSetFname()
        {
            WaitlistedPatient w = new WaitlistedPatient();
            w.Fname = "Test";
            Assert.True(w.Fname == "Test");

        }

        [TestCase]
        public void TestGetSetLname()
        {
            WaitlistedPatient w = new WaitlistedPatient();
            w.Lname = "Test";
            Assert.True(w.Lname == "Test");
        }

        [TestCase]
        public void TestGetSetWard()
        {
            WaitlistedPatient w = new WaitlistedPatient();
            w.Ward = "Test";
            Assert.True(w.Ward == "Test");
        }

        [TestCase]
        public void TestGetSetPriority()
        {
            WaitlistedPatient w = new WaitlistedPatient();
            w.Priority = "Test";
            Assert.True(w.Priority == "Test");
        }

        [TestCase]
        public void TestGetSetTreatment()
        {
            WaitlistedPatient w = new WaitlistedPatient();
            w.Treatment = "Test";
            Assert.True(w.Treatment == "Test");
        }
        #endregion
    }
}
