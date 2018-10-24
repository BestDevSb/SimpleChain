using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleBlockChain.Tests
{
    using System.Linq;
    using Core.Model;

    [TestClass]
    public class ChainTests
    {
        [TestMethod]
        public void CanCreateChain()
        {
            Chain chain = new Chain();
            Assert.IsTrue(chain.IsValid);
        }

        [TestMethod]
        public void CanAddBlocks()
        {
            const int nCount = 10;
            Chain chain = new Chain { Difficulty = 1};
            for(int i = 1; i < nCount; i++)
            {
                chain.TryAdd(new Block { Data = System.Guid.NewGuid().ToString() });
            }
            Assert.IsTrue(chain.IsValid);
            Assert.IsTrue(chain.Blocks.Count() == nCount);
        }

        [TestMethod]
        public void NewBlockRefecencesToPrevious()
        {
            Chain chain = new Chain { Difficulty = 1 };
            Block firstBlock = new Block { Data = System.Guid.NewGuid().ToString() };
            Block secondBlock = new Block { Data = System.Guid.NewGuid().ToString() };
            chain.TryAdd(firstBlock);
            chain.TryAdd(secondBlock);

            Assert.IsTrue(chain.Blocks.Count() == 3);
            Assert.IsTrue(string.Equals(firstBlock.Hash, secondBlock.PreviousHash, System.StringComparison.OrdinalIgnoreCase));
        }
    }
}
