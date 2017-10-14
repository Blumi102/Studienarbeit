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
        public static Point3D Coordinates;
        public static Point3D getCoordinates(string _setValue)
        {
            //X-Koordinate
            if (Printing.Printing.relativeCoordinates == false) //absolute Bemaßung
            {
                Coordinates.X = GetValue('X', _setValue); ;
            }
            else //relative Bemaßung
            {
                // this.coordinates.X = CurX - x;
            }

            //Y-Koordinate
            if (Printing.Printing.relativeCoordinates == false) //absolute Bemaßung
            {
                Coordinates.Y = GetValue('Y', _setValue); ;
            }
            else //relative Bemaßung
            {
                // this.coordinates.X = CurX - x;
            }

            //Z-Koordinate
            if (Printing.Printing.relativeCoordinates == false) //absolute Bemaßung
            {
                Coordinates.Z = GetValue('Z', _setValue); ;
            }
            else //relative Bemaßung
            {
                // this.coordinates.X = CurX - x;
            }

            return Coordinates;
        }

        public static double GetValue(char axis, string _setValue)
        {
            if (_setValue.IndexOf(axis) != -1)
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
            return 0.0;
        }
    }
}
