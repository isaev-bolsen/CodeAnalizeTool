using System;
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

namespace HierarhyDrawer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] Assembleys = new string[0];
        private OpenFileDialog OpenFileDialog = new OpenFileDialog() { Multiselect = true, Filter = "C# assembleys|*.dll; *.exe", CheckFileExists = true };
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectAssembleys(object sender, RoutedEventArgs e)
        {
            if (!OpenFileDialog.ShowDialog().GetValueOrDefault(false)) return;
            else Assembleys = OpenFileDialog.FileNames;
        }
    }
}
