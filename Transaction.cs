using System.Security.Cryptography;

namespace Blockchain
{
    public class Transaction
    {
        public RSAParameters From { get; private set; }
        public RSAParameters To { get; private set; }
        public byte[] FromKey;
        public byte[] ToKey;
        public double Amount { get; private set; }
        public byte[] FromSignature { get; private set; } = null;
        public byte[] ToSignature { get; private set; } = null;
        public byte[] Bytes { get; private set; }
        public byte[] Hash { get; private set; }

        public Transaction(RSAParameters from, RSAParameters to, double amount)
        {
            From = from;
            To = to;
            Amount = amount;

            var fromRsa = new RSACryptoServiceProvider();
            fromRsa.ImportParameters(From);
            FromKey = fromRsa.ExportRSAPublicKey();

            var toRsa = new RSACryptoServiceProvider();
            toRsa.ImportParameters(To);
            ToKey = toRsa.ExportRSAPublicKey();

            SetBytes();
        }

        // TODO publish to a p2p socket rather than a block
        public bool Publish(Block block)
        {
            if (FromSignature != null && ToSignature != null)
            {
                block.AddTransaction(this);
                return true;
            }
            return false;
        }

        public bool Verify(RSAParameters signer, byte[] signature, bool isSender)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(signer);

            if (rsa.VerifyData(Bytes, SHA256.Create(), signature))
            {
                if (isSender)
                {
                    FromSignature = signature;

                }
                else
                {
                    ToSignature = signature;
                }

                if (FromSignature != null && ToSignature != null)
                {
                    SetBytes();
                    HashBytes();
                }
                return true;
            }
            return false;
        }

        private void SetBytes()
        {
            Bytes = Util.StringToBytes(
                From.ToString() +
                To.ToString() +
                Amount.ToString() +
                FromSignature?.ToString() ?? "" +
                ToSignature?.ToString() ?? ""
            );
        }

        private void HashBytes()
        {
            Hash = SHA256.HashData(Bytes);
        }
    }
}