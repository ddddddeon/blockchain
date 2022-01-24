namespace Blockchain
{
    class Program
    {
        public static void Main()
        {
            var block = new Block(null, "first block");
            var secondBlock = block.Append("second block");

            Console.WriteLine(BytesToHexString(secondBlock.Hash));
            Console.WriteLine(secondBlock.Contents);
            Console.WriteLine(BytesToHexString(secondBlock.Previous.Hash));
            Console.WriteLine(secondBlock.Previous.Contents);
        }

        public static string BytesToHexString(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}