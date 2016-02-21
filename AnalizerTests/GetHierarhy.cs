using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace AnalizerTests
{
    [TestClass]
    public class GetHierarhy
    {
        [TestMethod]
        public void GetHierarhySimple()
        {
            Analizer.MyTypeInfo res = new Analizer.Analizer(this.GetType().Assembly).GetHierarhy("AnalizerTests.SimpleHierarhyRoot");
            Assert.AreSame(typeof(SimpleHierarhyRoot), res.Type);
            Assert.AreSame(typeof(SimpleHierarhyChild), res.Children.Single().Type);
        }
    }
}
