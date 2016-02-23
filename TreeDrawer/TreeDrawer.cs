using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

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
            public FrameworkElement payload;
            public Line Edge = new Line() { Stroke = Brushes.Black };

            public void CorrectEdgeposition()
            {
                if (Parent == null) return; //root node
                Canvas.SetLeft(Edge, XPosition);
                Canvas.SetTop(Edge, YPosition);
                Edge.X1 = 0;
                Edge.Y1 = 0;
                Edge.X2 = Parent.XPosition - XPosition;
                Edge.Y2 = Parent.YPosition - YPosition + payload.DesiredSize.Height;
            }
        }

        public double stepY = 50;
        public double stepX = 50;

        public double Width { get; private set; }

        List<List<Node>> Levels = new List<List<Node>>();

        private Canvas canvas;
        protected abstract IEnumerable<T> GetChildren(T elment);
        protected abstract FrameworkElement GetPayLoad(T element);

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
                    canvas.Children.Add(element.Edge);
                    Canvas.SetTop(element.payload, element.YPosition);
                    element.payload.SizeChanged += CorrectPosition;
                }
        }

        private void CorrectPosition(object sender, RoutedEventArgs e)
        {
            Width = Levels.Select(level => level.Select(n => n.payload.DesiredSize.Width).Sum() + (level.Count - 1) * stepX).Max();
            canvas.Width = Width;
            foreach (var level in Levels) SetX(level);
            SetY();
            foreach (var level in Levels) DrawLinks(level);
        }

        private void SetY()
        {
            double TotalHeight = stepY;

            for (int i = 0; i < Levels.Count; ++i)
            {
                foreach (Node node in Levels[i]) node.YPosition = TotalHeight;
                TotalHeight += Levels[i].Select(n => n.payload.DesiredSize.Height).Union(new double[] { stepY }).Max() + stepY;
            }

            foreach (var level in Levels)
                foreach (var element in level)
                    Canvas.SetTop(element.payload, element.YPosition);

            canvas.Height = TotalHeight;
        }

        private void DrawLinks(List<Node> level)
        {
            foreach (Node element in level) element.CorrectEdgeposition();
        }

        private void SetX(List<Node> level)
        {
            for (int i = 0; i < level.Count; ++i) level[i].XPosition = Width / level.Count * (i + 0.5);
            foreach (var element in level) Canvas.SetLeft(element.payload, element.XPosition - element.payload.DesiredSize.Width / 2);
        }
    }
}
