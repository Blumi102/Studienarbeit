using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace FileHandling
{
    public class GCode
    { 
        /// <summary>
        /// This method opens a GCode-File and reads in the included coordinates. 
        /// </summary>
        /// <returns>
        /// The return value is the path of the selected file.
        /// </returns>
        public static string HandleGCode()
        {
            OpenFileDialog OpenGC = new OpenFileDialog();
            OpenGC.Title = "G-Code öffnen";
            OpenGC.Filter = "GCode files (*.gcode)|*.gcode";
            try
            {
                if (OpenGC.ShowDialog() == true)
                {                    
                    StreamReader OpenedFile = new StreamReader(OpenGC.FileName);
                    String CurLine = "";

                    while (!OpenedFile.EndOfStream)
                    {
                        CurLine = OpenedFile.ReadLine();
                        ReadOutFile(CurLine);                      
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

        private static void ReadOutFile(string CurLine)
        {   
            if (!CurLine.StartsWith(";")) //hide commands
            {
                MessageBox.Show(CurLine);
                int trenner = CurLine.IndexOf(" ");
                if (trenner != 1)
                {
                    string Command = "";
                    for (int i = 0; i <= trenner; i++)
                    {
                        Command = Command + CurLine[i];
                    }
                    MessageBox.Show(Command);

                    

                }

            }
        }

    }
    public class Command
    {
        public Instructions Instruction= Instructions.NaN;
        public int x = 0;
        public int y = 0;
        public int z = 0;
        public char ValueChar;
        public int Value;

        Command(string setInstruction, string setValue)
        {
           
            switch (setInstruction)
            {
                case "G1":
                    Instruction = Instructions.G1;
                    //Lineare Bewegung
                    return;

                case "G28":
                    Instruction = Instructions.G28;
                    //Homing
                    return;

                case "G90":
                    Instruction = Instructions.G90;
                    //Absolute Positionierung
                    return;

                case "G91":
                    Instruction = Instructions.G91;
                    //Relative Positionierung
                    return;

                case "G92":
                    Instruction = Instructions.G92;
                    //Aktuelle Position
                    return;

                case "M104":
                    Instruction = Instructions.M104;
                    //Aufheizen Extruder (ohne Temperaturüberwachung)
                    return;

                case "M106":
                    Instruction = Instructions.M106;
                    //Lüftergeschwindigkeit
                    return;

                case "M109":
                    Instruction = Instructions.M109;
                    //Aufheizen Extruder (mit Temperaturüberwachung)
                    return;

                default:
                    Instruction = Instructions.NaN;
                    //Fehler: Befehl wurde nicht erkannt
                    break;
            }
        }
        public enum Instructions
        {
            G1, G28, G90, G91, G92, M104, M106, M109, NaN
        }

    }
}
