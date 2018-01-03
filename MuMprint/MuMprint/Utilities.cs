using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace MuMprint
{
    public class Utilities
    {
        public static void GetMoveValues(string ComandValue, Command Com)
        {
            //X-Koordinate
            if (ComandValue.Contains("X")) 
            {
                if (Printing.Printing.RelativeCoordinates == true) //relative Bemaßung
                {
                    Com.coordinates.X = GetValue('X', ComandValue);
                }
                else //absolute Bemaßung
                {
                    Com.coordinates.X = GetValue('X', ComandValue) - FileHandling.GCodeReader.curX;
                }
            }

            //Y-Koordinate
            if (ComandValue.Contains("Y"))
            {
                if (Printing.Printing.RelativeCoordinates == true) //relative Bemaßung
                {
                    Com.coordinates.Y = GetValue('Y', ComandValue);
                }
                else //absolute Bemaßung
                {
                    Com.coordinates.Y = GetValue('Y', ComandValue) - FileHandling.GCodeReader.curY;
                }
            }

            //Z-Koordinate
            if (ComandValue.Contains("Z"))
            {
                if (Printing.Printing.RelativeCoordinates == true) //relative Bemaßung
                {
                    Com.coordinates.Z = GetValue('Z', ComandValue);
                }
                else //absolute Bemaßung
                {
                    Com.coordinates.Z = GetValue('Z', ComandValue) - FileHandling.GCodeReader.curZ;
                }
            }

            //E-Koordinate
            if (ComandValue.Contains("E"))
            {
                if (Printing.Printing.RelativeCoordinates == true) //relative Bemaßung
                {
                    Com.E = GetValue('E', ComandValue);
                }
                else //absolute Bemaßung
                {
                    Com.E = GetValue('E', ComandValue) - FileHandling.GCodeReader.curE;
                }
            }

            //Geschwindigkeit
            if (Printing.Printing.RelativeCoordinates == false & ComandValue.Contains("F")) //absolute Bemaßung
            {
                Printing.Printing.Speed = GetValue('F', ComandValue);         
            }
            else //relative Bemaßung
            {
                // this.coordinates.X = CurX - x;
            }
        }

        public static void GetTemp(string _setValue, Command com)
        {
            //Temperatur in °C
            if (Printing.Printing.RelativeCoordinates == false & _setValue.Contains("S")) //absolute Bemaßung
            {
                Printing.Printing.Temp = GetValue('S', _setValue);
            }
            else //relative Bemaßung oder nicht enthalten
            {
                // this.coordinates.X = CurX - x;
            }
        }

        public static double GetValue(char axis, string _setValue)
        {
            int CurPos = _setValue.IndexOf(axis) + 1;
            int res = 0;
            bool EndOfString = false;
            string Value = "";

            double.TryParse(_setValue, out double test);

            while (EndOfString == false & _setValue[CurPos]!=';' & (int.TryParse(_setValue[CurPos].ToString(), out res) | _setValue[CurPos] == '.' | _setValue[CurPos] == '-'))
            {
                Value += _setValue[CurPos];
                if (CurPos + 1 < _setValue.Length)
                {
                    CurPos += 1;
                }
                else
                {
                    EndOfString = true;
                }

            }
            return Convert.ToDouble(Value.Replace('.', ','));
        }

    }
}
