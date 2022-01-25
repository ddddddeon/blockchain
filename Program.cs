namespace Blockchain
{
    class Program
    {
        public static void Main()
        {
            var chain = new Chain();
            var block = new Block(null);

            var wallet = new Wallet();
            var wallet2 = new Wallet();

            // TODO implement wallet.Send / wallet.Receive
            var transaction = new Transaction(wallet.PublicKey, wallet2.PublicKey, 1.00);

            var fromSignature = wallet.Sign(transaction.Bytes);
            var toSignature = wallet2.Sign(transaction.Bytes);

            Console.WriteLine("Sender {0} signature: {1}", Util.BytesToHexString(transaction.FromKey).Substring(18, 32), Util.BytesToHexString(fromSignature));
            Console.WriteLine("Recipient {0} signature: {1}", Util.BytesToHexString(transaction.ToKey).Substring(18, 32), Util.BytesToHexString(toSignature));

            var fromVerified = transaction.Verify(wallet.PublicKey, fromSignature, true);
            var toVerified = transaction.Verify(wallet2.PublicKey, toSignature, false);

            Console.WriteLine("Sender signature verified: {0}", fromVerified.ToString());
            Console.WriteLine("Recipient signature verified: {0}", toVerified.ToString());

            var published = transaction.Publish(block);

            Console.WriteLine("Transaction {0} published: {1}", Util.BytesToHexString(transaction.Hash), published);
            Console.WriteLine("Transactions in block: {0}/{1}", block.TransactionCount, block.MaxTransactions);

            if (block.TransactionCount == 1 /* block.MaxTransactions */)
            {
                block.Mine();
                Block appended = chain.Append(block);
                Console.WriteLine("Block {0} added to the blockchain: {1}\nChain length: {2}", block.HashString, appended != null, chain.Length.ToString());
            }


            /*
                        for (int i = 2; i < 1000; i++)
                        {

                            chain.Append(new Block(chain.Last, "block number " + i.ToString()));
                        }

                        while (b != null)
                        {
                            Console.WriteLine("{0} - {1} - {2}", b.Id,
                                    b.Contents,
                                    b.HashString
                            );

                            b = b.Previous;
                        }

                        Console.WriteLine("Chain length: {0}", chain.Length);
                        */
        }
    }
}