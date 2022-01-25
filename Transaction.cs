using System.Security.Cryptography;

namespace Blockchain
{
    public class Transaction
    {
        public RSAParameters From { get; private set; }
        public RSAParameters To { get; private set; }
        public double Amount { get; private set; }
        public byte[] FromSignature { get; private set; } = null;
        public byte[] ToSignature { get; private set; } = null;
        public byte[] Bytes { get; private set; }

        public Transaction(RSAParameters from, RSAParameters to, double amount)
        {
            From = from;
            To = to;
            Amount = amount;
            // TODO check this
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
            var rsaFrom = new RSACryptoServiceProvider();
            rsaFrom.ImportParameters(signer);

            if (rsaFrom.VerifyData(Bytes, SHA256.Create(), signature))
            {
                if (isSender)
                {
                    FromSignature = signature;

                }
                else
                {
                    ToSignature = signature;
                }

                SetBytes();
                return true;
            }
            return false;
        }

        public void SetBytes()
        {
            Bytes = Util.StringToBytes(From.ToString() + To.ToString() + Amount.ToString());
        }

    }


}