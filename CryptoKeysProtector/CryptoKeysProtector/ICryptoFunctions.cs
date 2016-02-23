namespace CryptoKeysProtector
{
    /// <summary>
    /// The Cryptography Functions interface.
    /// </summary>
    internal interface ICryptoFunctions
    {
        /// <summary>
        /// The encrypt.
        /// </summary>
        /// <param name="dataToBeEncrypted">
        /// The data to be encrypted.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        byte[] Encrypt(byte[] dataToBeEncrypted);

        /// <summary>
        /// The decrypt.
        /// </summary>
        /// <param name="dataToBeDecrypted">
        /// The data to be decrypted.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        byte[] Decrypt(byte[] dataToBeDecrypted);
    }
}