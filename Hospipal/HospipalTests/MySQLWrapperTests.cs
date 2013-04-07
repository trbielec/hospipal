using System;
using System.Collections.Generic;
using NUnit.Framework;
using MySql.Data.MySqlClient;
using Hospipal;


namespace HospipalTests
{
    [TestFixture]
    public class MySQLWrapperTests
    {
        private MySQLWrapper wrapper;
      
        [TestFixtureSetUp]
        public void setup() 
        {
        }

        [TestCase]
        public void TestTestConnectionMethidWithNullValues() 
        {
            wrapper = new MySQLWrapper(null, null, null, null);
            Assert.False(wrapper.TestConnection());
        }

        [TestCase]
        public void TestTestConnectionMethodWithInvalidServer()
        {
            wrapper = new MySQLWrapper("", "ismacaul_HospiPalTestDB", "ismacaul_seng403", "seng403");
            Assert.False(wrapper.TestConnection());
        }

        [TestCase]
        public void TestTestConnectionMethidWithInvalidDatabase() 
        {
            wrapper = new MySQLWrapper("s403g01.cpsc.ucalgary.ca", "ismacaul_hos", "ismacaul_seng403", "seng403");
            Assert.False(wrapper.TestConnection());
        }

        [TestCase]
        public void TestTestConnectionMethidWithInvalidUserID() 
        {
            wrapper = new MySQLWrapper("s403g01.cpsc.ucalgary.ca", "ismacaul_HospiPalTestDB", "seng403", "seng403");
            Assert.False(wrapper.TestConnection());
        }

        [TestCase]
        public void TestTestConnectionMethidInvalidPassword() 
        {
            wrapper = new MySQLWrapper("s403g01.cpsc.ucalgary.ca", "ismacaul_HospiPalTestDB", "ismacaul_seng403", "seng400");
            Assert.False(wrapper.TestConnection());
        }

        [TestCase]
        public void TestTestConnectionMethidWithValidValues() 
        {
            wrapper = new MySQLWrapper("s403g01.cpsc.ucalgary.ca", "ismacaul_HospiPalTestDB", "ismacaul_seng403", "seng403");
            Assert.True(wrapper.TestConnection());
        }

        [TestCase]
        public void TestQueryFunctionWithValidQueries()
        {
            wrapper = new MySQLWrapper("s403g01.cpsc.ucalgary.ca", "ismacaul_HospiPalTestDB", "ismacaul_seng403", "seng403");
            string query = "INSERT INTO TestTable VALUES (1);";
            Assert.True(wrapper.Insert(query));

            query = "UPDATE TestTable SET TestColumn = 2 WHERE TestColumn = 1";
            Assert.True(wrapper.Update(query));

            query = "SELECT * FROM TestTable";
            List<object[]> returnList = wrapper.Select(query);
            object[] row = returnList[0];
            int value = (int)row[0];
            Assert.True(value == 2);

            query = "DELETE FROM TestTable WHERE TestColumn = 2;";
            Assert.True(wrapper.Delete(query));
        }

        [TestCase]
        public void TestQueryFunctionsWithInvalidQueries()
        {
            wrapper = new MySQLWrapper("s403g01.cpsc.ucalgary.ca", "ismacaul_HospiPalTestDB", "ismacaul_seng403", "seng403");
            string query = "INSERT INTO table VALUES (1);";
            Assert.False(wrapper.Insert(query));

            query = "UPDATE table SET TestColumn = 2 WHERE TestColumn = 1";
            Assert.False(wrapper.Update(query));

            query = "SELECT * FROM table";
            List<object[]> returnList = wrapper.Select(query);
            Assert.True(returnList == null);

            query = "DELETE FROM table WHERE TestColumn = 2;";
            Assert.False(wrapper.Delete(query));
        }

        [TestCase]
        public void TestDatabaseCheckConnection()
        {
            Assert.True(Database.CheckConnection());
        }

        [TestFixtureTearDown]
        public void tearDown()
        {
        }
    }
}
