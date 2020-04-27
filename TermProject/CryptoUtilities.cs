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
        //
        //Class used for password crypto
        //
        private static RNGCryptoServiceProvider rngCrypto = new RNGCryptoServiceProvider();

        public static byte[] GenerateSalt()
        {
            //The salt is just a random 16byte array

            byte[] saltArray = new byte[16];
            rngCrypto.GetNonZeroBytes(saltArray);
            return saltArray;

        }

        public static byte[] CalculateMD5Hash(byte[] saltArray, string password)
        {
            //
            //https://docs.microsoft.com/en-us/archive/blogs/csharpfaq/how-do-i-calculate-a-md5-hash-from-a-string
            //

            //Create the md5 hash when given the string password and the saltedArray
            MD5 md5 = MD5.Create();

            byte[] passwordArray = Encoding.ASCII.GetBytes(password);

            byte[] concatBytes = new byte[passwordArray.Length + saltArray.Length];

            //Append the salt to the password byte[] and then get the md5 of that.
            Array.Copy(passwordArray, concatBytes, passwordArray.Length);
            Array.Copy(saltArray, 0, concatBytes, passwordArray.Length, saltArray.Length);

            md5.ComputeHash(concatBytes);

            return md5.ComputeHash(concatBytes);
        }

        public static Boolean comparePassword(byte[] hashedPassword, byte[] salt, string enteredPassword)
        {
            //Method used for checking if the correct password was entered.
            if (CalculateMD5Hash(salt, enteredPassword).SequenceEqual(hashedPassword)) return true;
            return false;
        }
    }
}