using Analizer;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HierarhyDrawer
{
    public class Drawer
    {
        private Canvas Canvas;

       public Drawer (Canvas canvas)
        {
            Canvas = canvas;
            Canvas.Children.Clear();
        }

        protected virtual UIElement GetNodeRepresentation(MyTypeInfo typeInfo)
        {
            return new TextBox() { Text = typeInfo.Type.FullName, IsReadOnly = true, Height = 25 };
        }

        public void Draw(MyTypeInfo root)
        {
            UIElement UIroot = GetNodeRepresentation(root);
            Canvas.Children.Add(UIroot);
            Draw(UIroot, root.Children);
        }

        private void Draw(UIElement Parent, IEnumerable<MyTypeInfo> Children)
        {
            IEnumerable<UIElement> CildElemnts = Children.Select(N => GetNodeRepresentation(N));
        }
    }
}
