using System;
using NUnit.Framework;
using Hospipal.Database_Class;
using System.Collections.Generic;

namespace HospipalTests
{
    [TestFixture]
    public class WardClassTests
    {
        [TestCase]
        public void TestWardSelect()
        {
            Ward ward = new Ward("UnitTestWard", "UTW");
            ward.Insert();

            Assert.True(ward.Select());

            ward.Delete();
        }

        [TestCase]
        public void TestWardInsert()
        {
            Ward ward = new Ward("UnitTestWard", "UTW");
            Assert.True(ward.Insert());

            ward.Delete();   
        }

        [TestCase]
        public void TestWardUpdate()
        {
            Ward ward = new Ward("UnitTestWard", "UTW");
            ward.Insert();

            ward.SlugName = "TEST";
            Assert.True(ward.Update());

            ward.Delete();
        }

        [TestCase]
        public void TestWardDelete()
        {
            Ward ward = new Ward("UnitTestWard", "UTW");
            ward.Insert();

            Assert.True(ward.Delete());
        }

        [TestCase]
        public void TestGetWards()
        {
            Ward ward = new Ward("UnitTestWard", "UTW");
            ward.Insert();

            List<Ward> w = Ward.GetWards();
            Assert.True(w.Count >= 1);

            ward.Delete();
        }
    }
}
