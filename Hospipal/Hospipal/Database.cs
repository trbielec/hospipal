using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospipal
{
    public static class Database
    {
        private static MySQLWrapper db = new MySQLWrapper("s403g01.cpsc.ucalgary.ca", "ismacaul_HospiPal", "ismacaul_seng403", "seng403");


        public static bool Insert(string query)
        {
            return db.Insert(query);
        }

        public static bool Update(string query)
        {
            return db.Update(query);
        }

        public static bool Delete(string query)
        {
            return db.Delete(query);
        }

        public static List<object[]> Select(string query)
        {
            return db.Select(query);
        }

        internal static bool Update(MySql.Data.MySqlClient.MySqlCommand command)
        {
            return db.Update(command);
        }

        internal static bool Insert(MySql.Data.MySqlClient.MySqlCommand command)
        {
            return db.Insert(command);
        }

        internal static List<object[]> Select(MySql.Data.MySqlClient.MySqlCommand command)
        {
            return db.Select(command);
        }

        public static bool CheckConnection()
        {
            return db.TestConnection();
        }

        public static void useTestDB()
        {
            db = new MySQLWrapper("s403g01.cpsc.ucalgary.ca", "ismacaul_HospiPalTestDB", "ismacaul_seng403", "seng403");
        }
    }
}
