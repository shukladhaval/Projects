namespace CryptoKeysProtector
{
    /// <summary>
    /// The WindowsKeysProtector <see langword="interface"/>.
    /// </summary>
    internal interface IKeysProtector
    {
        /// <summary>
        /// The add key.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string AddKey();

        /// <summary>
        /// Get RSA key pair
        /// </summary>
        /// <param name="onlyPublic">
        /// Get only public key
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetKeyPair(bool onlyPublic);

        /// <summary>
        /// The delete keys.
        /// </summary>
        /// <param name="containerName">
        /// The container name.
        /// </param>
        void DeleteKeys();

        string ContainerName { get; set; }
    }
}