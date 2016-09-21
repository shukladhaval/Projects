namespace CryptoKeysProtector.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    using Xunit;

    [ExcludeFromCodeCoverage]
    public class HybridCryptoTests
    {
        [Fact]
        [Trait("Happy Path", "When everything should work")]
        public void Hybrid_Crypt_Should_Work_As_Expected()
        {
            // ARRANGE
            var hybridCrypto = new HybridWithDataIntegrity();
            var secretData = "this is a super secret";

            // ACT
            var encryptedPacket = hybridCrypto.Encrypt(Encoding.UTF8.GetBytes(secretData));
            var decryptedText = hybridCrypto.Decrypt(encryptedPacket);

            // ASSERT
            Assert.Equal(secretData, Encoding.UTF8.GetString(decryptedText));
        }

        [Fact]
        public void Should_Throw_An_Exception_When_Data_Is_Tampered()
        {
            // ARRANGE
            var hybridCrypto = new HybridWithDataIntegrity();
            var secretData = "this is a super secret";

            // ACT
            var encryptedPacket = hybridCrypto.Encrypt(Encoding.UTF8.GetBytes(secretData));

            // data tampering (man in the middle attack)

            var tamperedPacket = GetTamperedData(encryptedPacket.EncryptedData);
            encryptedPacket.EncryptedData = tamperedPacket;

            // ASSERT
            Assert.ThrowsAny<CryptographicException>(() => { var decryptedText = hybridCrypto.Decrypt(encryptedPacket); });

        }

        private byte[] GetTamperedData(byte[] encryptedData)
        {
            return encryptedData.Reverse().ToArray();
        }
    }
}
