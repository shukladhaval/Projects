namespace CryptoKeysProtector
{
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// The hybrid with data integrity.
    /// </summary>
    public class HybridWithDataIntegrity
    {
        /// <summary>
        /// The encrypt.
        /// </summary>
        /// <param name="dataToBeEncrypted">
        /// The data to be encrypted.
        /// </param>
        /// <returns>
        /// The <see cref="EncryptedPacket"/>.
        /// </returns>
        public EncryptedPacket Encrypt(byte[] dataToBeEncrypted)
        {
            var keysProtector = new WindowsKeysProtector();
            var rsaCryptoFunctions = new RsaCryptoFunctions(keysProtector);
            var aesCryptoFunctions = new AesCryptoFunctions();
            var sessionKey = aesCryptoFunctions.Key;
            
            // Encrypt data with AES and AES Key with RSA
            var encryptedPacket = new EncryptedPacket();

            var encryptedData = aesCryptoFunctions.Encrypt(dataToBeEncrypted);
            var encryptedKey = rsaCryptoFunctions.Encrypt(sessionKey);
            var containerName = Encoding.UTF8.GetBytes(keysProtector.ContainerName);

            // hash the encrypted Data with the Key. This will ensure that at the receiver end we will verify that it is coming from correct origin.
            using (var hmac = new HMACSHA256(sessionKey))
            {
                encryptedPacket.Hmac = hmac.ComputeHash(encryptedData);
            }

            encryptedPacket.EncryptedData = encryptedData;
            encryptedPacket.EncryptedSessionKey = encryptedKey;
            encryptedPacket.Iv = aesCryptoFunctions.InitVector;
            encryptedPacket.ContainerName = containerName;

            return encryptedPacket;
       }

        /// <summary>
        /// The decrypt.
        /// </summary>
        /// <param name="encryptedPacket">
        /// The encrypted packet.
        /// </param>
        /// <returns>
        /// The <see cref="byte[]"/>.
        /// </returns>
        /// <exception cref="CryptographicException">
        /// </exception>
        public byte[] Decrypt(EncryptedPacket encryptedPacket,bool deleteRSAKeys = true)
        {

            IKeysProtector keysProtector = new WindowsKeysProtector(Encoding.UTF8.GetString(encryptedPacket.ContainerName));
            var rsaCryptoFunctions = new RsaCryptoFunctions(Encoding.UTF8.GetString(encryptedPacket.ContainerName));


            var decryptedSessionKey = rsaCryptoFunctions.Decrypt(encryptedPacket.EncryptedSessionKey);
            var aesCryptoFunctions = new AesCryptoFunctions(decryptedSessionKey,encryptedPacket.Iv);
            using (var hmac = new HMACSHA256(decryptedSessionKey))
            {
                var hmacToCheck = hmac.ComputeHash(encryptedPacket.EncryptedData);

                if (!Compare(encryptedPacket.Hmac, hmacToCheck))
                {
                    throw new CryptographicException("HMAC for decryption does not match encrypted packet.");
                }

            }

            var decryptedData = aesCryptoFunctions.Decrypt(encryptedPacket.EncryptedData);
            if (deleteRSAKeys)
            {
                keysProtector.DeleteKeys();
            }
            return decryptedData;
        }

        private static bool Compare(byte[] array1, byte[] array2)
        {
            var result = array1.Length == array2.Length;

            for (var i = 0; i < array1.Length && i < array2.Length; ++i)
            {
                result &= array1[i] == array2[i];
            }

            return result;
        }


    }

    /// <summary>
    /// </summary>
    public class EncryptedPacket
    {
        public byte[] EncryptedSessionKey;
        public byte[] EncryptedData;
        public byte[] Iv;
        public byte[] Hmac;

        public byte[] ContainerName { get; set; }
    }
}