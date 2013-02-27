using System;
using NUnit.Framework;
using Hospipal.Database_Class;

namespace HospipalTests
{
    [TestFixture]
    public class BedClassTests
    {
        private Bed bed;

        [TestCase]
        public void TestBedSelect()
        {
            bed = new Bed();
            Assert.False(bed.Select());

            bed = new Bed(1, 0, 0, null, null);
            Assert.True(bed.Select());
        }

        [TestCase]
        public void TestInsertDeleteBeds()
        {
            bed = new Bed(1000, Bed.States.Occupied, 1, "Room", "Nurse");
            Assert.True(bed.Insert());
            Assert.False(bed.Insert());

            Assert.True(bed.Delete());
            Assert.False(bed.Delete());
        }

        [TestCase]
        public void TestGetBeds()
        {
            Assert.IsEmpty(Bed.GetBeds(""));

            bed = new Bed(1000, Bed.States.Occupied, 1, "TestRoom", "Nurse");
            Assert.True(bed.Insert());
            Assert.IsNotEmpty(Bed.GetBeds("TestRoom"));
            Assert.True(bed.Delete());
        }
    }
}
