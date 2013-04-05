using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace Hospipal.Database_Class
{
    class Login
    {

        public static bool CreateLogin(int eid, string fname, string lname)
        {
            string userName = fname + eid.ToString();
            string tempPassword = "password";
            int firstLogin = 1;
            
            byte[] salt = CreateSalt();
            byte[] hash = CreateHash(tempPassword, salt);

            MySqlCommand login = new MySqlCommand();
            string SQL = "INSERT INTO Login VALUES(@userName, @hashPw, @salt, @eid, @firstLogin)";
            login.CommandText = SQL;
            login.Parameters.AddWithValue("@userName", userName);
            login.Parameters.AddWithValue("@hashPw", hash);
            login.Parameters.AddWithValue("@salt", salt);
            login.Parameters.AddWithValue("@eid", eid);
            login.Parameters.AddWithValue("@firstLogin", firstLogin);
            return Database.Insert(login);
        }

        public static bool VerifyLogin(string userName, string password)
        {
            MySqlCommand login = new MySqlCommand();
            string SQL = "SELECT * FROM Login WHERE userName = @userName;";
            login.CommandText = SQL;
            login.Parameters.AddWithValue("@userName", userName);

            List<object[]> loginInfo = Database.Select(login);
            if (loginInfo == null || loginInfo.Count() != 1 || loginInfo[0][0].ToString() != userName)
            {
                return false;
            }

            byte[] hashedPassword = (byte[])loginInfo[0][1];
            byte[] salt = (byte[])loginInfo[0][2];

            if(ValidatePassword(password, hashedPassword, salt)) 
            {
                int firstLogin = Convert.ToInt32(loginInfo[0][4]);
                if (firstLogin == 1)
                {
                    ForcePasswordReset(userName, salt);
                }

                int eid = Convert.ToInt32(loginInfo[0][3]);
                SQL = "SELECT employee_type FROM Employee WHERE eid = " + eid;
                List<object[]> employee = Database.Select(SQL);
                if (employee != null && employee.Count != 0)
                {
                    Properties.Settings.Default.Role = employee[0][0].ToString();
                }
                else
                {
                    Properties.Settings.Default.Role = "Support Staff";
                }

                return true;
            }
            return false;
        }

        private static void ForcePasswordReset(string userName, byte[] salt)
        {

        }

        #region Hashing Functions
        /// Salted password hashing with PBKDF2-SHA1.
        /// Adapted for our needs from: www: http://crackstation.net/hashing-security.htm
        private const int SALT_BYTES = 24;
        private const int HASH_BYTES = 24;
        private const int PBKDF2_ITERATIONS = 100;

        public static byte[] CreateHash(string password, byte[] salt)
        {
            // Hash the password and encode the parameters
            return Hash(password, salt);
        }

        public static byte[] CreateSalt()
        {
            // Create the salt
            RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SALT_BYTES];
            rand.GetBytes(salt);

            return salt;
        }

        private static bool ValidatePassword(string password, byte[] storedHash, byte[] salt)
        {
            // Create test hash from salt and password provided and check
            byte[] testHash = Hash(password, salt);
            return SlowEquals(storedHash, testHash);
        }

        // Compares two byte arrays in length-constant time. This comparison
        // method is used so that password hashes cannot be extracted from
        // on-line systems using a timing attack and then attacked off-line.
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint)(a[i] ^ b[i]);
            return diff == 0;
        }

        // Hash the password with the salt
        private static byte[] Hash(string password, byte[] salt)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = PBKDF2_ITERATIONS;
            return pbkdf2.GetBytes(HASH_BYTES);
        }

        #endregion

    }
}
