using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MuMprint
{
    /// <summary>
    /// This class is able to create a TCP-Connection where this program is on the client computer.
    /// You have to enter the IP-address of the server you want to connect to.
    /// In addition to that a XML-file can be read in and send to the server.
    /// </summary>

    class TCP_Client
    {
        public static string ip;

        public static List<string> GetHosts(string hostname)
        {
            List<string> ips = new List<string>();

            try
            {
                IPHostEntry hostInfo;

                // Versuche die DNS für die übergebenen Host und IP-Adressen aufzulösen
                hostInfo = Dns.GetHostEntry(hostname);

                foreach (IPAddress ipaddr in hostInfo.AddressList)
                {
                    if (ipaddr.ToString().Contains("."))
                    {
                        ips.Add(ipaddr.ToString());
                    }
                }

            }
            catch (Exception)
            {
                Console.WriteLine("\tUnable to resolve host: " + hostname + "\n");
            }

            return ips;
        }


        public static void CreateTCPClient(string ip, string XMLpath)
        {
            TcpClient client = new TcpClient();

            try
            {
                client.Connect(ip, 8000);
//https://www.entwickler-ecke.de/topic_Datei+ueber+Stream+versenden_100789.html&sid=ed63cf0d58e1e84100e96567d6727cad

                NetworkStream stream = client.GetStream();

                FileStream fs = File.OpenRead(XMLpath);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                stream.Write(bytes, 0, bytes.Length);
                fs.Close();
                stream.Close();
                client.Close();

            }

            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }

    }

}

