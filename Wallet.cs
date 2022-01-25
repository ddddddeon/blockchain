using System.Security.Cryptography;

namespace Blockchain
{
    public class Wallet
    {
        private readonly int _keySize = 2048;
        private readonly RSACryptoServiceProvider Rsa;
        private readonly RSAParameters PrivateKey;
        public RSAParameters PublicKey;

        public Wallet()
        {
            Rsa = new RSACryptoServiceProvider(_keySize);
            PrivateKey = Rsa.ExportParameters(true);
            PublicKey = Rsa.ExportParameters(false);
        }

        public byte[] Sign(byte[] input)
        {
            return Rsa.SignData(input, SHA256.Create());
        }

        public bool Verify(byte[] input, RSAParameters publicKey, byte[] signature)
        {
            var rsa = new RSACryptoServiceProvider(_keySize);
            rsa.ImportParameters(publicKey);
            return rsa.VerifyData(input, SHA256.Create(), signature);
        }
    }
}