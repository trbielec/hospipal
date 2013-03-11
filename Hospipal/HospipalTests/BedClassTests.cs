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
    }
}
