using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;
using MuMprint;

namespace CommandHandling
{
    /// <summary>
    /// This class represents every action of the printer which is defined in the read in GCode-file.
    /// </summary>
    /// 

    public class Command
    {
        string _Command = "";
        string _Value = "";
        public Instructions Instruction = Instructions.NaN;
        public Point3D coordinates = new Point3D(0, 0, 0);
        public double E = 0.0;

        public Command(string CurLine)
        {
            int trenner = CurLine.IndexOf(" ");
           
            if (trenner != -1)
            {
                for (int i = 0; i < trenner; i++)
                {
                    _Command += CurLine[i];
                }

                for (int i = trenner + 1; i <= CurLine.Length - 1; i++)
                {
                    _Value += CurLine[i];
                }
            }
            else
            {
                _Command = CurLine;
            }

            switch (_Command)
            {
                case "G0":
                    Instruction = Instructions.G1;
                    //Lineare Bewegung
                    Utilities.GetMoveValues(_Value, this);
                    return;

                case "G1":
                    Instruction = Instructions.G1;
                    //Lineare Bewegung
                    Utilities.GetMoveValues(_Value, this);
                    return;

                case "G28": 
                    Instruction = Instructions.G28;
                    //Homing
                    if (_Value.Contains("X"))
                    {
                        this.coordinates.X = - 1000;
                    }
                    if (_Value.Contains("Y"))
                    {
                        this.coordinates.Y = - 1000;
                    }
                    if (_Value.Contains("Z"))
                    {
                        this.coordinates.Z = - 1000;
                    }
                    if (_Value.Contains("E"))
                    {
                        this.E = - 1000;
                    }
                    if (!_Value.Contains("X") & !_Value.Contains("Y") & !_Value.Contains("Z") & !_Value.Contains("E"))
                    {
                        this.coordinates.X = - 1000;
                        this.coordinates.Y = - 1000;
                        this.coordinates.Z = - 1000;
                        this.E = - 1000;
                    }
                    return;

                case "G90":
                    Instruction = Instructions.G90;
                    //Absolute Positionierung
                    Printing.Printing.RelativeCoordinates = false;
                    return;

                case "G91":
                    Instruction = Instructions.G91;
                    //Relative Positionierung
                    Printing.Printing.RelativeCoordinates = true;
                    return;

                case "G92":
                    Instruction = Instructions.G92;
                    //Aktuelle Position
                    Utilities.GetMoveValues(_Value, this);
                    return;

                case "M104":
                    Instruction = Instructions.M104;
                    //Aufheizen Extruder (ohne Temperaturüberwachung)
                    return;

                case "M106":
                    Instruction = Instructions.M106;
                    //Lüfter einschalten
                    Utilities.SetFan(_Value, this);
                    return;

                case "M109":
                    Instruction = Instructions.M109;
                    //Aufheizen Extruder (mit Temperaturüberwachung)
                    Utilities.GetTemp(_Value, this);
                    return;

                default:
                    Instruction = Instructions.NaN;
                    //Fehler: Befehl wurde nicht erkannt
                    MessageBox.Show("Der eingelesene Befehl wurde nicht erkannt.", "Befehls - Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                    MessageBox.Show(_Value.ToString());
                    break;
            }
        }
        public enum Instructions
        {
            G1, G28, G90, G91, G92, M104, M106, M109, NaN
        }
    }
}
