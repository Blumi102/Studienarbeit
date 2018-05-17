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

        private void Button_Click(object sender, RoutedEventArgs E)
        {
            string x = x_box.Text;
            string y = y_box.Text;
            string z = z_box.Text;
            string e = e_box.Text;

            Command com = new Command("G92 X" + x + " Y" + y + " Z" + z + "E" + e);
            Printing.PrintingParameters.Commands.Clear();
            Printing.PrintingParameters.Commands.Add(com);
            FileHandling.XMLCreator.CreatXML(Printing.PrintingParameters.Commands, Environment.CurrentDirectory + @"\Test.xml");

            try 
            {
                TCP_Client.CreateTCPClient(TCP_Client.ip, Environment.CurrentDirectory + @"\Test.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Die TCP-Verbindung konnte nicht aufgebaut werden!\r\nBitte versuchen Sie es erneut.\r\n\r\nError-Beschreibung:\r\n" + ex.Message, "Öffnen - Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }


        private void home_button_Click(object sender, RoutedEventArgs e)
        {
            Command com = new Command("G28");
            Printing.PrintingParameters.Commands.Clear();
            Printing.PrintingParameters.Commands.Add(com);
            FileHandling.XMLCreator.CreatXML(Printing.PrintingParameters.Commands, Environment.CurrentDirectory + @"\Test.xml");

            try
            {
                TCP_Client.CreateTCPClient(TCP_Client.ip, Environment.CurrentDirectory + @"\Test.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Die TCP-Verbindung konnte nicht aufgebaut werden!\r\nBitte versuchen Sie es erneut.\r\n\r\nError-Beschreibung:\r\n" + ex.Message, "Öffnen - Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        private void x_home_Button_Click(object sender, RoutedEventArgs e)
        {
            Command com = new Command("G28 X");
            Printing.PrintingParameters.Commands.Clear();
            Printing.PrintingParameters.Commands.Add(com);
            FileHandling.XMLCreator.CreatXML(Printing.PrintingParameters.Commands, Environment.CurrentDirectory + @"\Test.xml");

            try
            {
                TCP_Client.CreateTCPClient(TCP_Client.ip, Environment.CurrentDirectory + @"\Test.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Die TCP-Verbindung konnte nicht aufgebaut werden!\r\nBitte versuchen Sie es erneut.\r\n\r\nError-Beschreibung:\r\n" + ex.Message, "Öffnen - Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        private void y_home_Button_Click(object sender, RoutedEventArgs e)
        {
            Command com = new Command("G28 Y");
            Printing.PrintingParameters.Commands.Clear();
            Printing.PrintingParameters.Commands.Add(com);
            FileHandling.XMLCreator.CreatXML(Printing.PrintingParameters.Commands, Environment.CurrentDirectory + @"\Test.xml");

            try
            {
                TCP_Client.CreateTCPClient(TCP_Client.ip, Environment.CurrentDirectory + @"\Test.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Die TCP-Verbindung konnte nicht aufgebaut werden!\r\nBitte versuchen Sie es erneut.\r\n\r\nError-Beschreibung:\r\n" + ex.Message, "Öffnen - Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        private void z_home_Button_Click(object sender, RoutedEventArgs e)
        {
            Command com = new Command("G28 Z");
            Printing.PrintingParameters.Commands.Clear();
            Printing.PrintingParameters.Commands.Add(com);
            FileHandling.XMLCreator.CreatXML(Printing.PrintingParameters.Commands, Environment.CurrentDirectory + @"\Test.xml");

            try
            {
                TCP_Client.CreateTCPClient(TCP_Client.ip, Environment.CurrentDirectory + @"\Test.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Die TCP-Verbindung konnte nicht aufgebaut werden!\r\nBitte versuchen Sie es erneut.\r\n\r\nError-Beschreibung:\r\n" + ex.Message, "Öffnen - Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        private void Fan_On_Button_Click(object sender, RoutedEventArgs e)
        {
            Command com = new Command("M106 S1");
            Printing.PrintingParameters.Commands.Clear();
            Printing.PrintingParameters.Commands.Add(com);
            FileHandling.XMLCreator.CreatXML(Printing.PrintingParameters.Commands, Environment.CurrentDirectory + @"\Test.xml");

            try
            {
                TCP_Client.CreateTCPClient(TCP_Client.ip, Environment.CurrentDirectory + @"\Test.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Die TCP-Verbindung konnte nicht aufgebaut werden!\r\nBitte versuchen Sie es erneut.\r\n\r\nError-Beschreibung:\r\n" + ex.Message, "Öffnen - Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        private void Fan_Off_Button_Click(object sender, RoutedEventArgs e)
        {
            Command com = new Command("M106 S0");
            Printing.PrintingParameters.Commands.Clear();
            Printing.PrintingParameters.Commands.Add(com);
            FileHandling.XMLCreator.CreatXML(Printing.PrintingParameters.Commands, Environment.CurrentDirectory + @"\Test.xml");

            try
            {
                TCP_Client.CreateTCPClient(TCP_Client.ip, Environment.CurrentDirectory + @"\Test.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Die TCP-Verbindung konnte nicht aufgebaut werden!\r\nBitte versuchen Sie es erneut.\r\n\r\nError-Beschreibung:\r\n" + ex.Message, "Öffnen - Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        private void Heat_On_Button_Click(object sender, RoutedEventArgs e)
        {
            Command com = new Command("M104 S1");
            Printing.PrintingParameters.Commands.Clear();
            Printing.PrintingParameters.Commands.Add(com);
            FileHandling.XMLCreator.CreatXML(Printing.PrintingParameters.Commands, Environment.CurrentDirectory + @"\Test.xml");

            try
            {
                TCP_Client.CreateTCPClient(TCP_Client.ip, Environment.CurrentDirectory + @"\Test.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Die TCP-Verbindung konnte nicht aufgebaut werden!\r\nBitte versuchen Sie es erneut.\r\n\r\nError-Beschreibung:\r\n" + ex.Message, "Öffnen - Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        private void Heat_Off_Button_Click(object sender, RoutedEventArgs e)
        {
            Command com = new Command("M104 S-1");
            Printing.PrintingParameters.Commands.Clear();
            Printing.PrintingParameters.Commands.Add(com);
            FileHandling.XMLCreator.CreatXML(Printing.PrintingParameters.Commands, Environment.CurrentDirectory + @"\Test.xml");

            try
            {
                TCP_Client.CreateTCPClient(TCP_Client.ip, Environment.CurrentDirectory + @"\Test.xml");
            }
            catch (Exception ex)
            {
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

        private void e_box_GotFocus(object sender, RoutedEventArgs e)
        {
            e_box.Text = "";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Printing.PrintingParameters.Commands.Clear();
            MuMprint.MainWindow main = Application.Current.MainWindow as MuMprint.MainWindow;
            main.IsEnabled = true;
        }

        private void GetHost_Button_Click(object sender, RoutedEventArgs e)
        {
            SelectHost SelectWindow = new SelectHost("MuMprint");
            SelectWindow.Show();
        }

        private void Absolut_button_Click(object sender, RoutedEventArgs e)
        {
            Printing.PrintingParameters.RelativeCoordinates = false;
        }

        private void Relativ_Button_Click(object sender, RoutedEventArgs e)
        {
            Printing.PrintingParameters.RelativeCoordinates = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TCP_Client.GetPrintingStatus("192.168.2.200");
        }
    }
}
