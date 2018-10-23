using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleBlockChain.Tests
{
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
            Chain chain = new Chain { Difficulty = 1};
            for(int i = 1; i < 100000; i++)
            {
                chain.TryAdd(new Block { Data = System.Guid.NewGuid().ToString() });
            }
            Assert.IsTrue(chain.IsValid);
        }
    }
}
