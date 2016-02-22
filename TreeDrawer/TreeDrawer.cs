﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TreeDrawer
{
    public class TreeDrawer<T>
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

        public double stepY = 40;
        public double stepX = 100;
        List<List<Node>> Levels = new List<List<Node>>();

        private Canvas canvas;

        public delegate IEnumerable<T> GetChildren(T elment);
        public delegate UIElement GetPayLoad(T element);

        private GetChildren getChildren;
        private GetPayLoad getPayLoad;

        public TreeDrawer(Canvas canvas, GetChildren GetChildrenMethod, GetPayLoad GetPayLoadMethod)
        {
            this.canvas = canvas;
            getChildren = GetChildrenMethod;
            getPayLoad = GetPayLoadMethod;
        }

        public void Clear()
        {
            canvas.Children.Clear();
            Levels.Clear();
        }

        private IEnumerable<Node> CreateChildNodes(Node parent, int level)
        {
            return getChildren(parent.element).Select(e =>
            {
                var childnode = new Node { element = e, Parent = parent, level = level, payload = getPayLoad(e), YPosition = level * stepY };
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
            Node RootNode = new Node() { element = root, payload = getPayLoad(root) };
            Levels.Add(new List<Node> { RootNode });
            do
            {
                Levels.Add(GetLevel(currentLevel));
                currentLevel++;
            } while (Levels[currentLevel].Count > 0);

            foreach (var level in Levels)
            {
                foreach (var element in level)
                {
                    canvas.Children.Add(element.payload);
                    Canvas.SetTop(element.payload, element.YPosition);
                }

                double totalWidth = level.OfType<FrameworkElement>().Select(e => e.ActualWidth).Sum() + (level.Count - 1) * stepX;
                double reservedForElement = totalWidth / level.Count;
                double Half = totalWidth / 2;

                for (int i = 0; i < level.Count; ++i) level[i].XPosition = reservedForElement * i - Half;
                foreach (var element in level) Canvas.SetLeft(element.payload, element.XPosition);
            }
        }
    }
}