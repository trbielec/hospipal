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
    public class Login
    {
        public const int SUCCESS = 0;
        public const int FAILURE = -1;
        public const int RESET = 1;

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

        public static int VerifyLogin(string userName, string password)
        {
            MySqlCommand login = new MySqlCommand();
            string SQL = "SELECT * FROM Login WHERE userName = @userName;";
            login.CommandText = SQL;
            login.Parameters.AddWithValue("@userName", userName);

            List<object[]> loginInfo = Database.Select(login);
            if (loginInfo == null || loginInfo.Count() != 1 || loginInfo[0][0].ToString() != userName)
            {
                return FAILURE;
            }

            byte[] hashedPassword = (byte[])loginInfo[0][1];
            byte[] salt = (byte[])loginInfo[0][2];

            if(ValidatePassword(password, hashedPassword, salt)) 
            {
                int firstLogin = Convert.ToInt32(loginInfo[0][4]);
                if (firstLogin != 0)
                {
                    ForcePasswordReset(userName, password, salt);
                    return RESET;
                }

                int eid = Convert.ToInt32(loginInfo[0][3]);
                SQL = "SELECT employee_type FROM Employee WHERE eid = " + eid;
                List<object[]> employee = Database.Select(SQL);
                if (employee != null && employee.Count != 0)
                {
                    string employeeType = employee[0][0].ToString();
                    if (employeeType == "Doctor" || employeeType == "Nurse")
                    {
                        Properties.Settings.Default.Role = "employee";
                    }
                    else if (employeeType == "Admin")
                    {
                        Properties.Settings.Default.Role = "admin";
                    }
                    else
                    {
                        Properties.Settings.Default.Role = "support staff";
                    }
                }
                else
                {
                    Properties.Settings.Default.Role = "support staff";
                }

                return SUCCESS;
            }
            return FAILURE;
        }

        private static bool ForcePasswordReset(string userName, string password, byte[] salt)
        {
            PasswordReset window = new PasswordReset(password);
            window.ShowDialog();

            if (window.Status)
            {
                byte[] hash = CreateHash(window.Password, salt);

                MySqlCommand update = new MySqlCommand();
                string SQL = "UPDATE Login SET hashedPassword = @newHash, firstLogin = 0 WHERE userName = @userName;";
                update.CommandText = SQL;
                update.Parameters.AddWithValue("@newHash", hash);
                update.Parameters.AddWithValue("@userName", userName);

                return Database.Update(update);
            }

            return false;            
        }

        #region Hashing Functions
        /// Salted password hashing with PBKDF2-SHA1.
        /// Adapted for our needs from: http://crackstation.net/hashing-security.htm
        private const int SALT_BYTES = 24;
        private const int HASH_BYTES = 24;
        private const int PBKDF2_ITERATIONS = 1000;

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
