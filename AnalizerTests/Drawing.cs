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
    public class Drawing
    {
        Canvas canvas = new Canvas();
        Drawer drawer;

        public Drawing()
        {
            drawer = new Drawer(canvas);
        }

        [TestMethod]
        public void DrawSimpleHierarhy()
        {
            drawer.Draw(new Analizer.Analizer(this.GetType().Assembly).GetHierarhy("AnalizerTests.SimpleHierarhyRoot"));
            Assert.AreEqual(2, canvas.Children.OfType<TextBox>().Count());
        }
    }
}
