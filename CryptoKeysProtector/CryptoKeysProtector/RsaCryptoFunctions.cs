namespace CryptoKeysProtector
{
    using System.Security.Cryptography;

    /// <summary>
    /// The rsa crypto functions.
    /// </summary>
    internal class RsaCryptoFunctions : ICryptoFunctions
    {
        private readonly IKeysProtector keysProtector;

        private readonly string containerName;

        public RsaCryptoFunctions(IKeysProtector keysProtector)
        {
            this.keysProtector = keysProtector;
            this.containerName = keysProtector.AddKey();
        }

        public RsaCryptoFunctions(string containerName)
        {
            this.containerName = containerName;
            this.keysProtector = new WindowsKeysProtector(containerName);
        }

        public byte[] Encrypt(byte[] dataToBeEncrypted)
        {
            byte[] cipherbytes;
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(this.keysProtector.GetKeyPair(true));
                cipherbytes = rsa.Encrypt(dataToBeEncrypted, true);
            }

            return cipherbytes;
        }

        public byte[] Decrypt(byte[] dataToBeDecrypted)
        {
            byte[] plain;
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(this.keysProtector.GetKeyPair(false));
                plain = rsa.Decrypt(dataToBeDecrypted, true);
            }

            return plain;
        }
    }
}