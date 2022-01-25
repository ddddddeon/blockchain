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

            Console.WriteLine("Sender signature: {0}\nRecipient signature: {1}", Util.BytesToHexString(fromSignature), Util.BytesToHexString(toSignature));

            var fromVerified = transaction.Verify(wallet.PublicKey, fromSignature, true);
            var toVerified = transaction.Verify(wallet2.PublicKey, toSignature, false);

            Console.WriteLine("Sender signature verified: {0}\nRecipient signature verified: {1}", fromVerified, toVerified);

            var published = transaction.Publish(block);

            if (block.TransactionCount == block.MaxTransactions)
            {
                block.Mine();
                chain.Append(block);
            }


            /*
                        for (int i = 2; i < 1000; i++)
                        {

                            chain.Append(new Block(chain.Last, "block number " + i.ToString()));
                        }

                        Block b = chain.Last;

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