using System;
using NUnit.Framework;
using Hospipal.Database_Class;

namespace HospipalTests
{
    [TestFixture]
    public class WardClassTests
    {
        private Ward ward;

        [TestCase]
        public void TestInsertUpdateDeleteWard()
        {
            ward = new Ward("TestWard", "TW");
            Assert.True(ward.Insert());
            Assert.False(ward.Insert());

            ward.SlugName = "CWS";
            Assert.True(ward.Update());

            Assert.True(ward.Delete());
            Assert.False(ward.Delete());
        }

        [TestCase]
        public void TestSelectWard()
        {
            ward = new Ward(null, null);
            Assert.False(ward.Select());

            Assert.True(new Ward("Test", "TW").Insert());
            ward = new Ward("Test", "TW");
            Assert.True(ward.Select());
            Assert.True(ward.Delete());
        }

        [TestCase]
        public void TestGetWards()
        {
            Assert.NotNull(Ward.GetWards());
        }
    }
}
