using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnalizerTests
{
    [TestClass]
    public class RootTests
    {
        [TestMethod]
        public void SimpleRootNormalByType()
        {
            Assert.AreSame(typeof(SimpleHierarhyRoot), new Analizer.Analizer(typeof(SimpleHierarhyChild).Assembly).GetRootForHierarhy(typeof(SimpleHierarhyChild)));
        }

        [TestMethod]
        public void SimpleRootNormalByTypeName()
        {
            Assert.AreSame(typeof(SimpleHierarhyRoot), new Analizer.Analizer(typeof(SimpleHierarhyChild).Assembly).GetRootForHierarhy("AnalizerTests.SimpleHierarhyChild"));
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void TrashRequest()
        {
            Assert.AreSame(typeof(SimpleHierarhyRoot), new Analizer.Analizer(typeof(SimpleHierarhyChild).Assembly).GetRootForHierarhy("Anild"));
        }
    }
}
