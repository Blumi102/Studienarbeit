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
using System.Windows.Shapes;

namespace MuMprint
{
    /// <summary>
    /// Interaktionslogik für SelectHost.xaml
    /// </summary>
    public partial class SelectHost : Window
    {

        public SelectHost(string hostname)
        {
            InitializeComponent();

            foreach (string item in TCP_Client.GetHosts(hostname))
            {
                Hosts_Box.Items.Add(item);
            }
        }

        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {

            //Application.Current.
            TCP_Client.ip = Hosts_Box.SelectedItem.ToString();
            this.Close();
        }
    }
}
