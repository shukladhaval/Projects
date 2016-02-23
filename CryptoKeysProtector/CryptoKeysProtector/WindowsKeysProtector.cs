namespace CryptoKeysProtector
{
    using System;
    using System.Security.Cryptography;

    /// <summary>
    /// The windows keys protector. The RSA keys are securely stored in Crypto Service Provider (CSP) container
    /// </summary>
    internal class WindowsKeysProtector : IKeysProtector 
    {
        public WindowsKeysProtector()
        {
            this.ContainerName = Guid.NewGuid().ToString();
        }

        public WindowsKeysProtector(string containerName)
        {
            this.ContainerName = containerName;
        }

        public string ContainerName { get; set; }

        public string AddKey()
        {
            var cspParams = new CspParameters { KeyContainerName = this.ContainerName };
            using (var rsaAlgorithim = new RSACryptoServiceProvider(cspParams))
            {
                rsaAlgorithim.KeySize = 2048;
            }
            return this.ContainerName;
        }

        public string GetKeyPair( bool onlyPublic)
        {
            var cspParams = new CspParameters { KeyContainerName = this.ContainerName };
            string keys;
            using (var rsaAlgorithim = new RSACryptoServiceProvider(cspParams))
            {
                keys = rsaAlgorithim.ToXmlString(!onlyPublic);
            }

            return keys;
        }

        public void DeleteKeys()
        {
            var cspParams = new CspParameters { KeyContainerName = this.ContainerName };
            using (var rsaAlgorithim = new RSACryptoServiceProvider(cspParams))
            {
                rsaAlgorithim.PersistKeyInCsp = false;
                rsaAlgorithim.Clear();
            }
        }
    }
}