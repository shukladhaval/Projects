namespace CryptoKeysProtector
{
    using System.IO;
    using System.Security.Cryptography;

    /// <summary>
    /// The AES crypto functions.
    /// </summary>
    internal class AesCryptoFunctions : ICryptoFunctions
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="AesCryptoFunctions"/> class.
        /// </summary>

        public AesCryptoFunctions()
        {
            this.Key = this.GenerateRandomNumber(32);
            this.InitVector = this.GenerateRandomNumber(16);
        }

        public AesCryptoFunctions(byte[] key, byte[] initVector)
        {
            this.Key = key;
            this.InitVector = initVector;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        public byte[] Key { get; set; }

        /// <summary>
        /// Gets or sets the initialization vector.
        /// </summary>
        public byte[] InitVector { get; set; } 
        #endregion

        public byte[] Encrypt(byte[] dataToBeEncrypted)
        {
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                aes.Key = this.Key;
                aes.IV = this.InitVector;

                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);

                    cryptoStream.Write(dataToBeEncrypted, 0, dataToBeEncrypted.Length);
                    cryptoStream.FlushFinalBlock();

                    return memoryStream.ToArray();
                }
            }
        }

        public byte[] Decrypt(byte[] dataToBeDecrypted)
        {
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                aes.Key = this.Key;
                aes.IV = this.InitVector;

                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);

                    cryptoStream.Write(dataToBeDecrypted, 0, dataToBeDecrypted.Length);
                    cryptoStream.FlushFinalBlock();

                    var decryptBytes = memoryStream.ToArray();

                    return decryptBytes;
                }
            }
        }

       

        private byte[] GenerateRandomNumber(int length)
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[length];
                randomNumberGenerator.GetBytes(randomNumber);

                return randomNumber;
            }
        }
    }
}