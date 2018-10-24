using System.Linq;
using System.Collections.Generic;

namespace SimpleBlockChain.Core.Model
{
    public class Chain
    {        
        public Chain()
        {
            _blocks = new List<ChainedBlock>();
            _blocks.Add(new GenesisBlock());
            _syncLock = new object();
        }

        #region private
        private readonly List<ChainedBlock> _blocks;
        private readonly object _syncLock;
        
        private class ChainedBlock
        {
            public Block Block { get; set; }
            public Block PreviousBlock { get; set; }

            public virtual bool IsValid
            {
                get
                {
                    return (Block.PreviousHash == PreviousBlock.Hash && Block.Hash == Block.GetHash());
                }
            }
        }

        private class GenesisBlock: ChainedBlock
        {
            public GenesisBlock()
            {
                Block = new Block
                {
                    Data = nameof(GenesisBlock)
                };
                Block.Hash = Block.GetHash();
            }

            public override bool IsValid
            {
                get
                {
                    return Block.Hash == Block.GetHash();
                }
            }            
        }


        #endregion
        public int Difficulty { get; set; }

        public IEnumerable<Block> Blocks
        {
            get
            {
                return _blocks.Select(chained => chained.Block);
            }
        }

        public Block GetLatest() => _blocks.Last().Block;

        public bool TryAdd(Block block)
        {
            lock(_syncLock)
            {
                Block latest = GetLatest();
                Miner miner = new Miner();
                block.PreviousHash = latest.Hash;
                miner.Mine(block, Difficulty);
                var chainedBlock = new ChainedBlock { Block = block, PreviousBlock = latest };
                if (chainedBlock.IsValid)
                {
                    block.Index = latest.Index + 1;
                    _blocks.Add(chainedBlock);
                    
                    return true;
                }
                return false;
            }
        }

        public bool IsValid => _blocks.AsParallel().All(block => block.IsValid);
              
    }
}
