using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace _3D_printing
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFileButtin_Click(object sender, RoutedEventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog Dialog1 = new OpenFileDialog();

            Dialog1.InitialDirectory = "d:\\";
            d
            Dialog1.RestoreDirectory = true;

            if (Dialog1.ShowDialog() == true)
            {
                try
                {
                    
                   // if ((myStream = Dialog1.OpenFile()) != null)
                   // {
                        FilePathBox.Text = System.IO.Path.GetFullPath(Dialog1.FileName);

                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
           
        }
    }
}
