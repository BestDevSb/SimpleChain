namespace SimpleBlockChain.Core.Model
{
    public class Miner
    {
        public void Mine(Block block, int difficulty)
        {
            var leadingZeros = new string('0', difficulty);
            while (block.Hash == null || block.Hash.Substring(0, difficulty) != leadingZeros)
            {
                block.Nonce++;
                block.Hash = block.GetHash();
            }
        }
    }
}
