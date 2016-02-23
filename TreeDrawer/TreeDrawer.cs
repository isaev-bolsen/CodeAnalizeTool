using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TreeDrawer
{
    public abstract class TreeDrawer<T>
    {
        private class Node
        {
            public T element;
            public Node Parent;
            public List<Node> Children = new List<Node>();
            public int level = 0;
            public double XPosition = 0;
            public double YPosition = 0;
            public UIElement payload;
        }

        public double stepY = 50;
        public double stepX = 50;

        public double Width { get; private set; }

        List<List<Node>> Levels = new List<List<Node>>();

        private Canvas canvas;
        protected abstract IEnumerable<T> GetChildren(T elment);
        protected abstract UIElement GetPayLoad(T element);

        public TreeDrawer(Canvas canvas)
        {
            this.canvas = canvas;
        }

        public void Clear()
        {
            canvas.Children.Clear();
            Levels.Clear();
        }

        private IEnumerable<Node> CreateChildNodes(Node parent, int level)
        {
            return GetChildren(parent.element).Select(e =>
            {
                var childnode = new Node { element = e, Parent = parent, level = level, payload = GetPayLoad(e), YPosition = level * stepY };
                parent.Children.Add(childnode);
                return childnode;
            }).ToList();
        }

        private List<Node> GetLevel(int PreviousLevel)
        {
            return Levels[PreviousLevel].SelectMany(n => CreateChildNodes(n, PreviousLevel + 1)).ToList();
        }

        public void Draw(T root)
        {
            int currentLevel = 0;
            Clear();
            Node RootNode = new Node() { element = root, payload = GetPayLoad(root) };
            Levels.Add(new List<Node> { RootNode });
            do
            {
                Levels.Add(GetLevel(currentLevel));
                currentLevel++;
            } while (Levels[currentLevel].Count > 0);

            foreach (var level in Levels)
                foreach (var element in level)
                {
                    canvas.Children.Add(element.payload);
                    Canvas.SetTop(element.payload, element.YPosition);
                    FrameworkElement fEl = element.payload as FrameworkElement;
                    if (fEl != null) fEl.SizeChanged += SetX;
                }
        }

        private void SetX(object sender, RoutedEventArgs e)
        {
            Width = Levels.Select(level => level.Select(n => n.payload.DesiredSize.Width).Sum() + (level.Count - 1) * stepX).Max();
            canvas.Width = Width;
            foreach (var level in Levels) SetX(level);
            canvas.UpdateLayout();
        }

        private void SetX(List<Node> level)
        {
            for (int i = 0; i < level.Count; ++i) level[i].XPosition = Width / level.Count * (i + 0.5);
            foreach (var element in level) Canvas.SetLeft(element.payload, element.XPosition - element.payload.DesiredSize.Width / 2);
        }
    }
}
