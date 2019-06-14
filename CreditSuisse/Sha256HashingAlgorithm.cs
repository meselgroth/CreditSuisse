using System.Security.Cryptography;
using System.Text;

namespace CreditSuisse
{
    public class Sha256HashingAlgorithm : IHashingAlgorithm
    {
        public string Hash(string pin)
        {
            using (var sha256Hash = SHA256.Create())
            {
                var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(pin));

                // Convert byte array to a string   
                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}