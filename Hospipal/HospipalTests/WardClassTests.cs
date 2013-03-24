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

        [TestCase]
        public void TestOverrideToString()
        {
            Ward w = new Ward();
            w.WardName = "Test";
            Assert.True(w.ToString() == "Test");
        }

        [TestCase]
        public void TestSelectWithInvalidWard()
        {
            Ward w = new Ward("InvalidWard");
            Assert.False(w.Select());
        }

        #region Get/Set Tests
        [TestCase]
        public void TestGetSetWardName()
        {
            Ward w = new Ward();
            w.WardName = "Test";
            Assert.True(w.WardName == "Test");
        }

        [TestCase]
        public void TestGetSetWardSlug()
        {
            Ward w = new Ward();
            w.SlugName = "Test";
            Assert.True(w.SlugName == "Test");
        }

        #endregion
    }
}
