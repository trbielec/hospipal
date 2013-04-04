using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Security;

namespace Hospipal.Database_Class
{
    class Login
    {
     
        public static List<string> GetUserData(string userName)
        {
            List<string> fieldsFromDatabase = new List<string>();
            try
            {
                List<object[]> dbLogin = Database.Select("SELECT * FROM Employee WHERE userName='" + userName +"'");
                foreach (object[] row in dbLogin)
                {
                    fieldsFromDatabase.Add(row[0].ToString());
                    fieldsFromDatabase.Add(row[1].ToString());
                    fieldsFromDatabase.Add(row[2].ToString());
                    fieldsFromDatabase.Add(row[3].ToString());
                    fieldsFromDatabase.Add(row[4].ToString());
                }
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Error getting data from database and/or converting");
            }
            return fieldsFromDatabase;
        }

        public static bool ValidateUser(string userName, string password)
        {
            int hash;
            int salt;
            int eid;
            int login;

            bool validated = false;
            List<string> fieldsFromDatabase = Login.GetUserData(userName);

            hash = Convert.ToInt32(fieldsFromDatabase[1]);
            salt = Convert.ToInt32(fieldsFromDatabase[2]);
            eid = Convert.ToInt32(fieldsFromDatabase[3]);
            login = Convert.ToInt32(fieldsFromDatabase[4]);

            return validated;
        }
    }

}
