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
                    string Value = "";

                    for (int i = 0; i < trenner; i++)
                    {
                        Command += CurLine[i];
                    }

                    for (int i = trenner+1; i<= CurLine.Length-1; i++)
                    {
                        Value += CurLine[i];
                    }

                    MuMprint.Command instruction = new MuMprint.Command(Command, Value);                 
                    Printing.Printing.Commands.Add(instruction);

    }

            }
        }

    }
}
