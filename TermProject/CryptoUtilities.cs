using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TermProject
{
    public static class CryptoUtilities
    {
        private static RNGCryptoServiceProvider rngCrypto = new RNGCryptoServiceProvider();

        public static byte[] GenerateSalt()
        {
            byte[] saltArray = new byte[16];
            rngCrypto.GetNonZeroBytes(saltArray);
            return saltArray;

        }

        public static byte[] CalculateMD5Hash(byte[] saltArray, string password)
        {
            //
            //https://docs.microsoft.com/en-us/archive/blogs/csharpfaq/how-do-i-calculate-a-md5-hash-from-a-string
            //

            // step 1, calculate MD5 hash from input
            MD5 md5 = MD5.Create();

            byte[] passwordArray = Encoding.ASCII.GetBytes(password);

            byte[] concatBytes = new byte[passwordArray.Length + saltArray.Length];

            Array.Copy(passwordArray, concatBytes, passwordArray.Length);
            Array.Copy(saltArray, 0, concatBytes, passwordArray.Length, saltArray.Length);

            md5.ComputeHash(concatBytes);

            return md5.ComputeHash(concatBytes);
        }

        public static Boolean comparePassword(byte[] hashedPassword, byte[] salt, string enteredPassword)
        {

            if (CalculateMD5Hash(salt, enteredPassword).SequenceEqual(hashedPassword)) return true;
            return false;
        }
    }
}