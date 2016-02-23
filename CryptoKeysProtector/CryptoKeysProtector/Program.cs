using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoKeysProtector
{
    class Program
    {
        static void Main(string[] args)
        {
            var secret = "This is super secret";

            var hybrid = new HybridWithDataIntegrity();
            var encryptedPacket = hybrid.Encrypt(Encoding.UTF8.GetBytes(secret));

            var decrypt = hybrid.Decrypt(encryptedPacket);

            Console.WriteLine(Encoding.UTF8.GetString(decrypt));
            Console.Read();
        }
    }
}
