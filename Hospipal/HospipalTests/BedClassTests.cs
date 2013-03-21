using System;
using NUnit.Framework;
using Hospipal.Database_Class;
using System.Collections.Generic;

namespace HospipalTests
{
    [TestFixture]
    public class BedClassTests
    {
        [TestCase]
        public void TestBedSelect()
        {
            Bed bed = new Bed(1, Bed.States.Available, 0, 1, "", "UnitTestWard");
            bed.Insert();

            Assert.True(bed.Select());

            bed.Delete();
        }

        [TestCase]
        public void TestBedSelectWithInvalidBedNo()
        {
            Bed bed = new Bed(-1, Bed.States.Available, 0, 1, "", "UnitTestWard");
            Assert.False(bed.Select());
        }

        [TestCase]
        public void TestInsertBeds()
        {
            Bed bed = new Bed(1, Bed.States.Available, 0, 1, "", "UnitTestWard");
            Assert.True(bed.Insert());
            bed.Delete();
        }

        [TestCase]
        public void TestDeleteBeds()
        {
            Bed bed = new Bed(1, Bed.States.Available, 0, 1, "", "UnitTestWard");
            bed.Insert();
            Assert.True(bed.Delete());
        }

        [TestCase]
        public void TestGetBeds()
        {
            Ward ward = new Ward("UnitTestWard", "UTW");
            ward.Insert();
            Room room = new Room(1, "UnitTestWard", 1);
            room.Insert();
            List<Bed> beds = Bed.GetBeds(1, "UnitTestWard");
            Assert.True(beds.Count == 0);

            Bed bed = new Bed(1, Bed.States.Available, 0, 1, "", "UnitTestWard");
            bed.Insert();

            beds = Bed.GetBeds(1, "UnitTestWard");
            Assert.True(beds.Count == 1);

            Bed bed2 = new Bed(2, Bed.States.Available, 0, 1, "", "UnitTestWard");
            bed2.Insert();

            beds = Bed.GetBeds(1, "UnitTestWard");
            Assert.True(beds.Count == 2);

            bed2.Delete();
            bed.Delete();
            room.Delete();
            ward.Delete();
        }

        #region Get/Set Tests

        [TestCase]
        public void TestGetSetBedState()
        {
            Bed bed = new Bed(1, Bed.States.Available, 1, 1, "", "UnitTestWard");

            int state = (int)(bed.state);
            Assert.True(state == 1);

            bed.state = Bed.States.Occupied;
            state = (int)(bed.state);
            Assert.True(state == 0);
        }

        [TestCase]
        public void TestGetSetPid()
        {
            Bed bed = new Bed(1, Bed.States.Available, 1, 1, "", "UnitTestWard");

            int pid = bed.pid;
            Assert.True(pid == 1);

            bed.pid = 2;
            Assert.True(bed.pid == 2);
        }

        [TestCase]
        public void TestGetSetRoomNo()
        {
            Bed bed = new Bed();
            bed.roomNo = 1;
            Assert.True(bed.roomNo == 1);
        }

        [TestCase]
        public void TestGetSetWard()
        {
            Bed bed = new Bed(1, Bed.States.Available, 1, 1, "", "UnitTestWard");
            Assert.True(bed.ward == "UnitTestWard");
            bed.ward = "UTW";
            Assert.True(bed.ward == "UTW");
        }

        [TestCase]
        public void TestGetSetAssigningNurse()
        {
            Bed bed = new Bed(1, Bed.States.Available, 1, 1, "TestNurse", "UnitTestWard");
            Assert.True(bed.assigningNurse == "TestNurse");
            bed.assigningNurse = "Nurse";
            Assert.True(bed.assigningNurse == "Nurse");
        }

        [TestCase]
        public void TestGetBed()
        {
            Bed b = new Bed(1, Bed.States.Available, 0, 1, "", "UTW");
            Assert.True(b.bed == "UTW001001");

            b = new Bed(11, Bed.States.Available, 0, 1,"", "UTW");
            Assert.True(b.bed == "UTW001011");
        }

        #endregion
    }
}
