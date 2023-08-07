using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Cryptography;
using System.Text;

namespace blogapp.Controllers
{
    /// <summary>
    /// Controller that is used to hash password data
    /// </summary>
    public class HashController : Controller
    {
        /// <summary>
        /// Hashes string value
        /// </summary>
        /// <param name="input">String to be hashed</param>
        /// <returns>Hashed string value</returns>
        public static string HashString(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    // "x2" formats each byte as a hexadecimal string
                    builder.Append(hashBytes[i].ToString("x2")); 
                }

                return builder.ToString();
            }
        }
    }
}
