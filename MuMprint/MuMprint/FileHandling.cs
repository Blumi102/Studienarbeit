using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Xml;

namespace FileHandling
{
    public class GCodeReader
    {
        /// <summary>
        /// This method opens a GCode-File and reads in the included coordinates. 
        /// </summary>
        /// <returns>
        /// The return value is the path of the selected file.
        /// </returns>
        /// 

        public static double curX = 0.0;
        public static double curY = 0.0;
        public static double curZ = 0.0;
        public static double curE = 0.0;

        public static string HandleGCode()
        {
            OpenFileDialog OpenGC = new OpenFileDialog
            {
                Title = "G-Code öffnen",
                Filter = "GCode files (*.gcode)|*.gcode"
            };

            try
            {
                if (OpenGC.ShowDialog() == true)
                {
                    StreamReader OpenedFile = new StreamReader(OpenGC.FileName);
                    String CurLine = "";


                    while (!OpenedFile.EndOfStream)
                    {
                        CurLine = OpenedFile.ReadLine();

                        if (!CurLine.StartsWith(";")) //hide commands
                        {
                            MuMprint.Command com = new MuMprint.Command(CurLine);
                            Printing.Printing.Commands.Add(com);
                        }
                    }

                    OpenedFile.Close();
                    return OpenGC.FileName;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Die Datei konnte leider nicht geöffnet werden!\r\nBitte versuchen Sie es erneut.\r\n\r\nError-Beschreibung:\r\n" + e.Message, "Öffnen - Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return "error";
        }
    }

    public class XMLCreator
        {
            public static void CreatXML(List<MuMprint.Command> objects, string XMLpath)
            {

                XmlDocument doc = new XmlDocument();    //Instanz eines XML Dokuments in den RAM laden 

                XmlDeclaration Deklaration = doc.CreateXmlDeclaration("1.0", "utf-8", "yes");
                doc.AppendChild(Deklaration);

            //Root Element einfügen
                XmlNode Project = doc.CreateElement("My3DPrintingProject");
                doc.AppendChild(Project);
                int i = 0;

            foreach (var item in objects)
            {
                i++;
                Project.AppendChild(doc.CreateElement("Command"));
                Project.SelectSingleNode("Command").Attributes.Append(doc.CreateAttribute("X")).InnerText = item.coordinates.X.ToString().Replace(',', '.');
                Project.SelectSingleNode("Command").Attributes.Append(doc.CreateAttribute("Y")).InnerText = item.coordinates.Y.ToString().Replace(',', '.');
                Project.SelectSingleNode("Command").Attributes.Append(doc.CreateAttribute("Z")).InnerText = item.coordinates.Z.ToString().Replace(',', '.');
            }
                doc.Save(XMLpath); //Speichern des im RAM liegenden XML Dokuments auf die Festplatte
            }
        
        }

}
