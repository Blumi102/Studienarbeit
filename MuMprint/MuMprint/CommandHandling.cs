using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace MuMprint
{
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
            if (trenner != 1) //-1?
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

                case "G28": //!!nur x-Achse nullen!
                    Instruction = Instructions.G28;
                    //Homing
                    this.coordinates.X = 0;
                    this.coordinates.Y = 0;
                    this.coordinates.Z = 0;
                    return;

                case "G90":
                    Instruction = Instructions.G90;
                    //Absolute Positionierung
                    Printing.Printing.relativeCoordinates = false;
                    return;

                case "G91":
                    Instruction = Instructions.G91;
                    //Relative Positionierung
                    Printing.Printing.relativeCoordinates = true;
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
                    //Lüftergeschwindigkeit
                    return;

                case "M109":
                    Instruction = Instructions.M109;
                    //Aufheizen Extruder (mit Temperaturüberwachung)
                    return;

                default:
                    Instruction = Instructions.NaN;
                    //Fehler: Befehl wurde nicht erkannt
                    MessageBox.Show("Der eingelesene Befehl wurde nicht erkannt.", "Befehls - Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }
        public enum Instructions
        {
            G1, G28, G90, G91, G92, M104, M106, M109, NaN
        }
    }
}
