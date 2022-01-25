namespace Blockchain
{
    public class Chain
    {
        public long Length { get; private set; } = 0;
        public Block Last { get; private set; } = null;

        public Chain() { }

        public Chain(Block firstBlock)
        {
            Append(firstBlock);
        }

        public Block Append(Block block)
        {
            if (block.Previous == Last)
            {
                Last = block;
                Length++;
                return Last;
            }
            return null;
        }
    }
}