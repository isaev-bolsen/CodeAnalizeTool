using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace AnalizerTests
{
    [TestClass]
    public class GetHierarhyTests
    {
        [TestMethod]
        public void GetHierarhySimple()
        {
            Analizer.MyTypeInfo res = new Analizer.Analizer(this.GetType().Assembly).GetHierarhy("AnalizerTests.SimpleHierarhyRoot");
            Assert.AreSame(typeof(SimpleHierarhyRoot), res.Type);
            Assert.AreSame(typeof(SimpleHierarhyChild), res.Children.Single().Type);
        }

        [TestMethod]
        public void GetHierarhyMoreComplex()
        {
            Analizer.MyTypeInfo res = new Analizer.Analizer(this.GetType().Assembly).GetHierarhy("AnalizerTests.MoreComplexHierarhyRoot");
            var children = res.Children.ToArray();
            Assert.AreEqual(2, children.Length);
            Assert.AreEqual(2, children[0].Children.Count());
            Assert.AreEqual(2, children[1].Children.Count());
        }

        [TestMethod]
        public void GetHierarhyMoreComplexFromChild()
        {
            Analizer.MyTypeInfo res = new Analizer.Analizer(this.GetType().Assembly).GetHierarhy("AnalizerTests.MoreComplexHierarhyChild22");
            var children = res.Children.ToArray();
            Assert.AreEqual(2, children.Length);
            Assert.AreEqual(2, children[0].Children.Count());
            Assert.AreEqual(2, children[1].Children.Count());
        }
    }
}