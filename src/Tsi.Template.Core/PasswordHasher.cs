using System;
using System.Security.Cryptography;
using System.Text;

using Tsi.Template.Core.Abstractions;
using Tsi.Template.Core.Attributes;

namespace Tsi.Template.Core
{
    [Injectable(typeof(IPasswordHasher))]
    public class PasswordHasher : IPasswordHasher
    { 
        public string GenerateHash(string input, string salt)
        {
            using var alg = new HMACSHA256(GetBytes(salt));
            var result = alg.ComputeHash(GetBytes(input));
            return Convert.ToBase64String(result);
        }

        public string GenerateSalt(int size = 32)
        {
            byte[] buff = new byte[size];
            using RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        private static byte[] GetBytes(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        } 
    }
}
