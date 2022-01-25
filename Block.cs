using System.Text;
using System.Security.Cryptography;

namespace Blockchain
{
    public class Block
    {
        public int MaxTransactions = 5;
        public int TransactionCount { get; private set; } = 0;
        public List<Transaction> Transactions { get; private set; }
        public bool IsFirst;
        public int Id { get; }
        public byte[] Nonce { get; private set; }
        public byte[] Hash { get; private set; }
        public string HashString { get; private set; }
        public Block Previous { get; }

        public Block(Block previous)
        {
            IsFirst = (previous == null);
            Previous = previous;
            Id = (!IsFirst ? Previous.Id + 1 : 0);
            Transactions = new List<Transaction>();
            Nonce = GenerateNonce();
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
            var transactionsConcat = new StringBuilder();
            foreach (Transaction t in Transactions)
            {
                transactionsConcat.Append(t.Bytes);
            }

            return Util.StringToBytes(
                Id.ToString() +
                Util.BytesToString(Nonce) +
                transactionsConcat +
                (!IsFirst ? Util.BytesToString(Previous.Hash) : SHA256.HashData(new byte[] { }))
            );
        }

        public bool AddTransaction(Transaction transaction)
        {
            Transactions.Add(transaction);
            return true;
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