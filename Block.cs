using System.Security.Cryptography;

namespace Blockchain
{
    public class Block
    {
        public bool IsFirst;
        public int Id { get; }
        public byte[] Nonce { get; private set; }
        public byte[] Hash { get; private set; }
        public string HashString { get; private set; }
        public Block Previous { get; }
        public string Contents { get; } = String.Empty;

        public Block(Block previous, string contents)
        {
            IsFirst = (previous == null);
            Previous = previous;
            Id = (!IsFirst ? Previous.Id + 1 : 0);
            Nonce = GenerateNonce();
            Contents = contents;
            Hash = SHA256.HashData(GetAllBytes());
            HashString = Util.BytesToHexString(Hash);
        }

        private byte[] GenerateNonce()
        {
            var random = new Random();
            byte[] nonce = new byte[4];
            random.NextBytes(nonce);
            return nonce;
        }

        private byte[] GetAllBytes()
        {
            return Util.StringToBytes(
                Id.ToString() +
                Util.BytesToString(Nonce) +
                Contents +
                (!IsFirst ? Util.BytesToString(Previous.Hash) : SHA256.HashData(new byte[] { }))
            );
        }

        public byte[] Mine()
        {
            byte[] hash = new byte[] { };
            byte[] nonce;

            Console.WriteLine("Mining block {0}...", HashString);
            do
            {
                Nonce = GenerateNonce();
                hash = SHA256.HashData(GetAllBytes());
                //Console.WriteLine("{0}: {1}", BytesToHexString(Nonce), BytesToHexString(hash));
            }
            while (!(hash[0] == 0 && hash[1] == 0));

            Hash = hash;
            HashString = Util.BytesToHexString(Hash);

            Console.WriteLine("Hash computed! New hash is {0}", HashString);
            return Hash;
        }
    }
}