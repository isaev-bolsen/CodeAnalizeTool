using Analizer;
using System.Windows;
using System.Windows.Controls;

namespace HierarhyDrawer
{
    public class Drawer
    {
        private TreeDrawer.TreeDrawer<MyTypeInfo> treeDrawer;
        public Drawer(Canvas canvas)
        {
            treeDrawer = new TreeDrawer.TreeDrawer<MyTypeInfo>(canvas,TI=>TI.Children, GetNodeRepresentation);
        }

        private static UIElement GetNodeRepresentation(MyTypeInfo typeInfo)
        {
            return new TextBox() { Text = typeInfo.Type.FullName, IsReadOnly = true, Height = 25 };
        }

        public void Draw(MyTypeInfo root)
        {
            treeDrawer.Clear();
            treeDrawer.Draw(root);
        }
    }
}
