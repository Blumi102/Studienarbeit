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
    /// Interaktionslogik für Test_Control.xaml
    /// </summary>
    public partial class Test_Control : Window
    {
        public Test_Control()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string x = x_box.Text;
            string y = y_box.Text;
            string z = z_box.Text;

            MuMprint.Command com = new MuMprint.Command("G92 X" + x + " Y" + y + " Z" + z);
            Printing.Printing.Commands.Add(com);
            FileHandling.XMLCreator.CreatXML(Printing.Printing.Commands, Environment.CurrentDirectory + @"\Test.xml");

            try
            {
                TCP_Client.CreateTCPClient(IP_Box.Text, Environment.CurrentDirectory + @"\Test.xml");
                Connected_Box.Content = "Connected to " + IP_Box.Text;
            }
            catch (Exception ex)
            {
                Connected_Box.Content = "Connection error";
                MessageBox.Show("Die TCP-Verbdinung konnte nicht aufgebaut werden!\r\nBitte versuchen Sie es erneut.\r\n\r\nError-Beschreibung:\r\n" + ex.Message, "Öffnen - Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                TCP_Client.CreateTCPClient(IP_Box.Text, Environment.CurrentDirectory + @"\Test.xml");
            }
            catch (Exception ex)
            {
                Connected_Box.Content = "Connection error";
                MessageBox.Show("Die TCP-Verbdinung konnte nicht aufgebaut werden!\r\nBitte versuchen Sie es erneut.\r\n\r\nError-Beschreibung:\r\n" + ex.Message, "Öffnen - Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }


        }

        private void home_button_Click(object sender, RoutedEventArgs e)
        {
            MuMprint.Command com = new MuMprint.Command("G28");
            Printing.Printing.Commands.Add(com);
            FileHandling.XMLCreator.CreatXML(Printing.Printing.Commands, Environment.CurrentDirectory + @"\Test.xml");

            try
            {
                TCP_Client.CreateTCPClient(IP_Box.Text, Environment.CurrentDirectory + @"\Test.xml");
                Connected_Box.Content = "Connected to " + IP_Box.Text;
            }
            catch (Exception ex)
            {
                Connected_Box.Content = "Connection error";
                MessageBox.Show("Die TCP-Verbdinung konnte nicht aufgebaut werden!\r\nBitte versuchen Sie es erneut.\r\n\r\nError-Beschreibung:\r\n" + ex.Message, "Öffnen - Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        private void x_box_GotFocus(object sender, RoutedEventArgs e)
        {
            x_box.Text = "";
        }

        private void y_box_GotFocus(object sender, RoutedEventArgs e)
        {
            y_box.Text = "";
        }

        private void z_box_GotFocus(object sender, RoutedEventArgs e)
        {
            z_box.Text = "";
        }

        private void IP_Box_GotFocus(object sender, RoutedEventArgs e)
        {
            IP_Box.Text = "";
        }
    }
}
