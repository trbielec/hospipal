using System;
using NUnit.Framework;
using Hospipal.Database_Class;

namespace HospipalTests
{
    [TestFixture]
    public class BedClassTests
    {
        private Bed bed;

        [SetUp]
        public void setup()
        {
            bed = new Bed(-1, Bed.States.Available, 0, 0, "TestNurse", "TestWard");
            bed.Insert();
        }

        [TearDown]
        public void teardown()
        {
            bed.Delete();
        }

        [TestCase]
        public void TestBedSelect()
        {
            Assert.True(bed.Select());
        }

        [TestCase]
        public void TestInsertBeds()
        {
            Bed testBed = new Bed(0, Bed.States.Available, 0, 0, "TestNurse", "TestWard");
            Assert.True(testBed.Insert());
            testBed.Delete();
        }

        [TestCase]
        public void TestDeleteBeds()
        {
            Bed testBed = new Bed(0, Bed.States.Available, 0, 0, "TestNurse", "TestWard");
            testBed.Insert();
            Assert.True(testBed.Delete());
        }

        [TestCase]
        public void TestGetBedsInvalidInput()
        {
            Assert.IsEmpty(Bed.GetBeds(-1,null));
        }
    }
}
