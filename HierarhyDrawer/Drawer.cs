using Analizer;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HierarhyDrawer
{
    public class Drawer
    {
        private Canvas canvas;

        public Drawer(Canvas canvas)
        {
            this.canvas = canvas;
            this.canvas.Children.Clear();
        }

        protected virtual UIElement GetNodeRepresentation(MyTypeInfo typeInfo)
        {
            return new TextBox() { Text = typeInfo.Type.FullName, IsReadOnly = true, Height = 25 };
        }

        public void Draw(MyTypeInfo root)
        {
            UIElement UIroot = GetNodeRepresentation(root);
            canvas.Children.Add(UIroot);
            Draw(UIroot, root.Children);
        }

        private void Draw(UIElement Parent, IEnumerable<MyTypeInfo> Children)
        {
            IEnumerable<UIElement> ChildElemnts = Children.Select(N => GetNodeRepresentation(N));
            foreach (UIElement elt in ChildElemnts)
            {
                canvas.Children.Add(elt);
                Canvas.SetTop(elt, 30);
            }
        }
    }
}
