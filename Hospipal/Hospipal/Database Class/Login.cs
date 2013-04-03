using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Hospipal.Database_Class
{
    public class Login
    {
        /// Salted password hashing with PBKDF2-SHA1.
        /// Adapted for our needs from: www: http://crackstation.net/hashing-security.htm
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

        public static bool ValidatePassword(string password, byte[] storedHash, byte[] salt)
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
    }
}

