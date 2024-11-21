using System.Security.Cryptography;
using System.Text;

namespace WebApplication1.Services.PasswordHasher
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly HashAlgorithm _algorithm;

        public PasswordHasher(
            HashAlgorithm algorithm)
        {
            _algorithm = algorithm;
        }

        public bool Compare(string password, string with)
        {
            string hashed = Hash(password);

            return hashed == with;
        }

        public string Hash(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);

            byte[] hash = _algorithm.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }
    }
}
