using Analizer;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace HierarhyDrawer
{
    public class Drawer : TreeDrawer.TreeDrawer<MyTypeInfo>
    {
        public Drawer(Canvas canvas) : base(canvas) { }

        protected override UIElement GetPayLoad(MyTypeInfo element)
        {
            return new TextBox() { Text = element.Type.FullName, IsReadOnly = true, Height = 25 };
        }

        protected override IEnumerable<MyTypeInfo> GetChildren(MyTypeInfo elment)
        {
            return elment.Children;
        }
    }
}
