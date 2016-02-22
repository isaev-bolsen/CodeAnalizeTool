﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Reflection;
using Analizer;

namespace HierarhyDrawer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HashSet<Assembly> Assembleys = new HashSet<Assembly>();
        private OpenFileDialog OpenFileDialog = new OpenFileDialog() { Multiselect = true, Filter = ".Net assembleys|*.dll; *.exe", CheckFileExists = true };
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectAssembleys(object sender, RoutedEventArgs e)
        {
            if (OpenFileDialog.ShowDialog().GetValueOrDefault(false))
                foreach (string filename in OpenFileDialog.FileNames)
                    try
                    {
                        Assembleys.Add(Assembly.LoadFile(filename));
                    }
                    catch (BadImageFormatException)
                    {
                        MessageBox.Show(this, filename + " is not a .Net assebly", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
        }

        private void Draw(object sender, RoutedEventArgs e)
        {
            MyTypeInfo root;
            if (string.IsNullOrEmpty(ClassName.Text)) return;

            try
            {
                root = new Analizer.Analizer(Assembleys).GetHierarhy(ClassName.Text);
            }
            catch (Exception exc)
            {
                MessageBox.Show(this, exc.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Draw(root, Canvas);
        }

        private static UIElement GetClassRepresentation(MyTypeInfo typeInfo)
        {
            var res = new TextBlock() { Text = typeInfo.Type.FullName };
            return res;
        }

        private static void Draw(MyTypeInfo root, Canvas canvas)
        {
            canvas.Children.Add(GetClassRepresentation(root));
        }
    }
}
