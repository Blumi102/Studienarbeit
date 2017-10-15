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
        public static void GetMoveValues(string _setValue, Command com)
        {
            //X-Koordinate
            if (Printing.Printing.relativeCoordinates == false & _setValue.Contains("X")) //absolute Bemaßung
            {
                com.coordinates.X = GetValue('X', _setValue); ;
            }
            else //relative Bemaßung oder nicht vorhanden
            {
                // this.coordinates.X = CurX - x;
            }

            //Y-Koordinate
            if (Printing.Printing.relativeCoordinates == false & _setValue.Contains("Y")) //absolute Bemaßung
            {
                com.coordinates.Y = GetValue('Y', _setValue); ;
            }
            else //relative Bemaßung oder nicht vorhanden
            {
                // this.coordinates.X = CurX - x;
            }

            //Z-Koordinate
            if (Printing.Printing.relativeCoordinates == false & _setValue.Contains("Z")) //absolute Bemaßung
            {
                com.coordinates.Z = GetValue('Z', _setValue); ;
            }
            else //relative Bemaßung oder nicht vorhanden
            {
                // this.coordinates.X = CurX - x;
            }

            //E-Koordinate
            if (Printing.Printing.relativeCoordinates == false & _setValue.Contains("E")) //absolute Bemaßung
            {
                Printing.Printing.E = GetValue('E', _setValue); ;
            }
            else //relative Bemaßung
            {
                // this.coordinates.X = CurX - x;
            }

            //Geschwindigkeit
            if (Printing.Printing.relativeCoordinates == false & _setValue.Contains("F")) //absolute Bemaßung
            {
                Printing.Printing.Speed = GetValue('F', _setValue); ;           
            }
            else //relative Bemaßung
            {
                // this.coordinates.X = CurX - x;
            }
        }

        public static void GetTemp(string _setValue, Command com)
        {
            //Temperatur in °C
            if (Printing.Printing.relativeCoordinates == false & _setValue.Contains("S")) //absolute Bemaßung
            {
                Printing.Printing.Temp = GetValue('S', _setValue); ;
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

            while (EndOfString == false & (int.TryParse(_setValue[CurPos].ToString(), out res) | _setValue[CurPos] == '.'))
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
