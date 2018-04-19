using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Xml;
using CommandHandling;

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
                    Printing.PrintingParameters.Commands.Clear();

                    while (!OpenedFile.EndOfStream)
                    {
                        CurLine = OpenedFile.ReadLine();

                        if (!CurLine.StartsWith(";")& !CurLine.StartsWith("M107")) //hide commands
                        {
                            Command com = new Command(CurLine);
                            Printing.PrintingParameters.Commands.Add(com);
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
        /// <summary>
        /// Creats a XML-file out of a objectlist generated from a GCode-file.
        /// This XML is used to transfer the model data to the control device of the printer using TCP/IP.
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="XMLpath"></param>
        /// 

            public static void CreatXML(List<Command> objects, string XMLpath)
            {

                XmlDocument doc = new XmlDocument();    //Instanz eines XML Dokuments in den RAM laden 

                XmlDeclaration Deklaration = doc.CreateXmlDeclaration("1.0", "utf-8", "yes");
                doc.AppendChild(Deklaration);

                //Root Element einfügen
                    XmlNode Project = doc.CreateElement("My3DPrintingProject");
                    doc.AppendChild(Project);
                int i = 0;
                System.Xml.XmlNode CurNode;

                foreach (var item in objects)
                {
                    i++;
                    CurNode = Project.AppendChild(doc.CreateElement("Command"));
                    CurNode.Attributes.Append(doc.CreateAttribute("Z")).InnerText = item.coordinates.Z.ToString().Replace(',', '.');
                    CurNode.Attributes.Append(doc.CreateAttribute("Y")).InnerText = item.coordinates.Y.ToString().Replace(',', '.');
                    CurNode.Attributes.Append(doc.CreateAttribute("X")).InnerText = item.coordinates.X.ToString().Replace(',', '.');
                    CurNode.Attributes.Append(doc.CreateAttribute("E")).InnerText = item.E.ToString().Replace(',', '.');
                    CurNode.Attributes.Append(doc.CreateAttribute("T")).InnerText = item.Temp.ToString();
                    CurNode.Attributes.Append(doc.CreateAttribute("L")).InnerText = item.Fan.ToString();

            }
            doc.Save(XMLpath); //Speichern des im RAM liegenden XML Dokuments auf die Festplatte
            }
        
        }

}
