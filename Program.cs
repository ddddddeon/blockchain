namespace Blockchain
{
    class Program
    {
        public static void Main()
        {
            var chain = new Chain();

            var block = new Block(null, "block number 1");
            chain.Append(block);
            Console.WriteLine(chain.Last.Previous);

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
        }
    }
}