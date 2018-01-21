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
using CommandHandling;

namespace MuMprint
{
    /// <summary>
    /// Interaktionslogik für Test_Control.xaml
    /// </summary>
    /// 

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

            Command com = new Command("G92 X" + x + " Y" + y + " Z" + z);
            Printing.Printing.Commands.Add(com);
            FileHandling.XMLCreator.CreatXML(Printing.Printing.Commands, Environment.CurrentDirectory + @"\Test.xml");

            try
            {
                TCP_Client.CreateTCPClient(IP_Box.Text, Environment.CurrentDirectory + @"\Test.xml");
                Connected_Box.Text = "Connected";
            }
            catch (Exception ex)
            {
                Connected_Box.Text = "Connection error";
                MessageBox.Show("Die TCP-Verbindung konnte nicht aufgebaut werden!\r\nBitte versuchen Sie es erneut.\r\n\r\nError-Beschreibung:\r\n" + ex.Message, "Öffnen - Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }


        private void home_button_Click(object sender, RoutedEventArgs e)
        {
            Command com = new Command("G28");
            Printing.Printing.Commands.Clear();
            Printing.Printing.Commands.Add(com);
            FileHandling.XMLCreator.CreatXML(Printing.Printing.Commands, Environment.CurrentDirectory + @"\Test.xml");

            try
            {
                TCP_Client.CreateTCPClient(IP_Box.Text, Environment.CurrentDirectory + @"\Test.xml");
                Connected_Box.Text = "Connected";
            }
            catch (Exception ex)
            {
                Connected_Box.Text = "Connection error";
                MessageBox.Show("Die TCP-Verbindung konnte nicht aufgebaut werden!\r\nBitte versuchen Sie es erneut.\r\n\r\nError-Beschreibung:\r\n" + ex.Message, "Öffnen - Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        private void x_home_Button_Click(object sender, RoutedEventArgs e)
        {
            Command com = new Command("G28 X");
            Printing.Printing.Commands.Add(com);
            FileHandling.XMLCreator.CreatXML(Printing.Printing.Commands, Environment.CurrentDirectory + @"\Test.xml");

            try
            {
                TCP_Client.CreateTCPClient(IP_Box.Text, Environment.CurrentDirectory + @"\Test.xml");
                Connected_Box.Text = "Connected";
            }
            catch (Exception ex)
            {
                Connected_Box.Text = "Connection error";
                MessageBox.Show("Die TCP-Verbindung konnte nicht aufgebaut werden!\r\nBitte versuchen Sie es erneut.\r\n\r\nError-Beschreibung:\r\n" + ex.Message, "Öffnen - Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        private void y_home_Button_Click(object sender, RoutedEventArgs e)
        {
            Command com = new Command("G28 Y");
            Printing.Printing.Commands.Add(com);
            FileHandling.XMLCreator.CreatXML(Printing.Printing.Commands, Environment.CurrentDirectory + @"\Test.xml");

            try
            {
                TCP_Client.CreateTCPClient(IP_Box.Text, Environment.CurrentDirectory + @"\Test.xml");
                Connected_Box.Text = "Connected";
            }
            catch (Exception ex)
            {
                Connected_Box.Text = "Connection error";
                MessageBox.Show("Die TCP-Verbindung konnte nicht aufgebaut werden!\r\nBitte versuchen Sie es erneut.\r\n\r\nError-Beschreibung:\r\n" + ex.Message, "Öffnen - Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        private void z_home_Button_Click(object sender, RoutedEventArgs e)
        {
            Command com = new Command("G28 Z");
            Printing.Printing.Commands.Add(com);
            FileHandling.XMLCreator.CreatXML(Printing.Printing.Commands, Environment.CurrentDirectory + @"\Test.xml");

            try
            {
                TCP_Client.CreateTCPClient(IP_Box.Text, Environment.CurrentDirectory + @"\Test.xml");
                Connected_Box.Text = "Connected";
            }
            catch (Exception ex)
            {
                Connected_Box.Text = "Connection error";
                MessageBox.Show("Die TCP-Verbindung konnte nicht aufgebaut werden!\r\nBitte versuchen Sie es erneut.\r\n\r\nError-Beschreibung:\r\n" + ex.Message, "Öffnen - Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
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

    }
}
