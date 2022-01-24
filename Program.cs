namespace Blockchain
{
    class Program
    {
        public static void Main()
        {
            var chain = new Chain();
            var block = new Block(null, "first block");
            chain.Append(block);

            var secondBlock = new Block(chain.Last, "second block");
            chain.Append(secondBlock);

            var lastBlock = chain.Last;
            Console.WriteLine("{0} - {1} - {2}", lastBlock.Id,
                lastBlock.Contents,
                BytesToHexString(lastBlock.Hash)
            );

            var previousBlock = lastBlock.Previous;
            Console.WriteLine("{0} - {1} - {2}", previousBlock.Id,
                previousBlock.Contents,
                BytesToHexString(previousBlock.Hash)
            );
        }

        public static string BytesToHexString(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}