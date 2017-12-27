using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MuMprint
{
    class TCP_Client
    {
        public static void CreateTCPClient(string ip, string XMLpath)
        {
            TcpClient client = new TcpClient();

            try
            {
                client.Connect(ip, 5000);

                // Den Netzwerk-Stream abrufen.
                NetworkStream stream = client.GetStream();

                // Einen BinaryWriter erstellen, um in den Stream zu schreiben.
                BinaryWriter w = new BinaryWriter(stream);

                StreamReader file = new StreamReader(XMLpath);
                w.Write(file.Read());

                // Einen BinaryReader erstellen, um aus dem Stream zu lesen.
                // BinaryReader r = new BinaryReader(stream);              
            }

            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }

    }

}

