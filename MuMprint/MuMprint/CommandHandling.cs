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
        public Instructions Instruction = Instructions.NaN;
        public Point3D coordinates = new Point3D(0, 0, 0);
        public char ValueChar = ' ';
        public int Value = 0;

        public Command(string setInstruction, string setValue)
        {
            switch (setInstruction)
            {
                case "G1":
                    Instruction = Instructions.G1;
                    //Lineare Bewegung
                    G1(setValue, coordinates);
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

        private void G1(string _setValue, Point3D coordinates)
        {
            if (Printing.Printing.relativeCoordinates == false) //absolute Bemaßung
            {
                if (_setValue.IndexOf("X") != -1)
                {
                    int CurPos = _setValue.IndexOf("X")+1;
                    int res = 0;
                    bool EndOfString = false;
                    string Value = "";
                
                    while(EndOfString == false &(int.TryParse(_setValue[CurPos].ToString(), out res)|_setValue[CurPos] == '.'))
                    {
                        Value += _setValue[CurPos];
                        if (CurPos+1 < _setValue.Length)
                        {
                            CurPos += 1;
                        }
                        else
                        {
                            EndOfString = true;
                        }
                        
                    }
                    this.coordinates.X = Convert.ToDouble(Value.Replace('.', ','));
                }
            }
        }
    }
}
