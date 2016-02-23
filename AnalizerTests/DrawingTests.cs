using HierarhyDrawer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Windows.Controls;

namespace AnalizerTests
{
    /// <summary>
    /// Сводное описание для Drawing
    /// </summary>
    [TestClass]
    public class DrawingTests
    {
        Canvas canvas = new Canvas();
        Drawer drawer;

        public DrawingTests()
        {
            drawer = new Drawer(canvas);
        }

        [TestMethod]
        public void DrawSimpleHierarhy()
        {
            drawer.Draw(new Analizer.Analizer(this.GetType().Assembly).GetHierarhy("AnalizerTests.SimpleHierarhyRoot"));
            Assert.AreEqual(2, canvas.Children.OfType<TextBox>().Count());
        }

        [TestMethod]
        public void DrawMoreComplexHierarhy()
        {
            drawer.Draw(new Analizer.Analizer(this.GetType().Assembly).GetHierarhy("AnalizerTests.MoreComplexHierarhyRoot"));
            Assert.AreEqual(7, canvas.Children.OfType<TextBox>().Count());
        }
    }
}
