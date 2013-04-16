using System;
using NUnit.Framework;
using Hospipal.Database_Class;

namespace HospipalTests
{
    [TestFixture]
    public class LoginClassTests
    {
        [TestCase]
        public void TestCreateSalt()
        {
            byte[] salt = Login.CreateSalt();
            Assert.True(salt.Length == 24);
        }

        [TestCase]
        public void TestCreateHash()
        {
            byte[] salt = Login.CreateSalt();
            byte[] hash = Login.CreateHash("password", salt);
            Assert.True(hash.Length == 24);
        }
    }
}
