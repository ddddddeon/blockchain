using System.Text;
using System.Security.Cryptography;

namespace Blockchain
{
    public class Block
    {
        public int Id { get; }
        public byte[] Nonce { get; }
        public byte[] Hash { get; }
        public Block Previous { get; }
        public string Contents { get; } = String.Empty;

        public Block(Block previous, string contents)
        {
            bool isFirst = previous == null;

            Id = (!isFirst ? previous.Id : 0);
            Nonce = GenerateNonce();
            Contents = contents;

            byte[] bytes = Encoding.ASCII.GetBytes(
                Id.ToString() +
                Nonce.ToString() +
                Contents +
                (!isFirst ? previous.Hash.ToString() : SHA256.HashData(new byte[] { }))
            );

            Hash = SHA256.HashData(bytes);
            Previous = previous;
        }

        public Block Append(string contents)
        {
            // TODO validate
            var block = new Block(this, contents);
            return block;
        }

        private byte[] GenerateNonce()
        {
            var random = new Random();
            byte[] nonce = new byte[4];
            random.NextBytes(nonce);
            return nonce;
        }
    }
}