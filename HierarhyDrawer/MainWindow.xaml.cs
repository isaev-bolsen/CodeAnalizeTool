using Analizer;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace HierarhyDrawer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HashSet<Assembly> Assembleys = new HashSet<Assembly>();
        private OpenFileDialog OpenFileDialog = new OpenFileDialog() { Multiselect = true, Filter = ".Net assembleys|*.dll; *.exe", CheckFileExists = true };
        private Drawer Drawer;

        public MainWindow()
        {
            InitializeComponent();
            Drawer = new Drawer(Canvas);
            SaveImageButton.Click += new ImageFromWPF.ImageFromWPF(Canvas).SaveImage;
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

        private void ClassName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) Draw(sender, e);
        }

        private void Draw(object sender, RoutedEventArgs e)
        {
            MyTypeInfo root;
            if (string.IsNullOrEmpty(ClassName.Text) || Assembleys.Count == 0) return;

            try
            {
                root = new Analizer.Analizer(Assembleys).GetHierarhy(ClassName.Text);
            }
            catch (Exception exc)
            {
                MessageBox.Show(this, exc.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Drawer.Draw(root);
        }

        private void View_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var pos = e.GetPosition(View);
                View.ScrollToHorizontalOffset(pos.X);
                View.ScrollToVerticalOffset(pos.Y);
            }
        }
    }
}
