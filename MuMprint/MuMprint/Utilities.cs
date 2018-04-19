using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;
using CommandHandling;

namespace MuMprint
{
    /// <summary>
    /// This class includes useful little functions which help to read out data from a GCode-file and creat command objects based on this.
    /// </summary>

    public class Utilities
    {
        public static void GetMoveValues(string ComandValue, Command Com)
        {
            //X-Koordinate
            if (ComandValue.Contains("X")) 
            {
                if (Printing.PrintingParameters.RelativeCoordinates == true) //relativ Bemaßung
                {
                    Com.coordinates.X = GetValue('X', ComandValue);
                }
                else //absolute Bemaßung
                {
                    Com.coordinates.X = GetValue('X', ComandValue) - Printing.PrintingParameters.CurPoint.X;
                }

                Printing.PrintingParameters.CurPoint.X = Printing.PrintingParameters.CurPoint.X + Com.coordinates.X;

            }

            //Y-Koordinate
            if (ComandValue.Contains("Y"))
            {
                if (Printing.PrintingParameters.RelativeCoordinates == true) //relativ Bemaßung
                {
                    Com.coordinates.Y = GetValue('Y', ComandValue);
                }
                else //absolute Bemaßung
                {
                    Com.coordinates.Y = GetValue('Y', ComandValue) - Printing.PrintingParameters.CurPoint.Y;
                }

                Printing.PrintingParameters.CurPoint.Y = Printing.PrintingParameters.CurPoint.Y + Com.coordinates.Y;
            }

            //Z-Koordinate
            if (ComandValue.Contains("Z"))
            {
                if (Printing.PrintingParameters.RelativeCoordinates == true) //relativ Bemaßung
                {
                    Com.coordinates.Z = GetValue('Z', ComandValue);

                }
                else //absolute Bemaßung
                {
                    Com.coordinates.Z = GetValue('Z', ComandValue) - Printing.PrintingParameters.CurPoint.Z;
                }

                Printing.PrintingParameters.CurPoint.Z = Printing.PrintingParameters.CurPoint.Z + Com.coordinates.Z;

            }

                //E-Koordinate
                if (ComandValue.Contains("E"))
            {
                if (Printing.PrintingParameters.RelativeCoordinates == true) //relativ Bemaßung
                {
                    Com.E = GetValue('E', ComandValue);
                }
                else //absolute Bemaßung
                {
                    Com.E = GetValue('E', ComandValue) - Printing.PrintingParameters.CurE;
                }

                Printing.PrintingParameters.CurE = Printing.PrintingParameters.CurE + Com.E;

            }

                //Geschwindigkeit
                if (Printing.PrintingParameters.RelativeCoordinates == false & ComandValue.Contains("F")) //relativ Bemaßung
            {
                Printing.PrintingParameters.Speed = GetValue('F', ComandValue);         
            }
            else //absolute Bemaßung
            {
                // this.coordinates.X = CurX - x;
            }
        }

        public static double GetTemp(string _TempValue, Command com)
        {
            //Temperatur in °C
           return GetValue('S', _TempValue);         
           
        }

        public static int SetFan(string _FanValue, Command Com)
        {
            if (GetValue('S', _FanValue) > 0)
            {
               return  1;
            }
            else
            {
               return -1;
            }
            
        }

        public static double GetValue(char axis, string _setValue)
        {
            int CurPos = _setValue.IndexOf(axis) + 1;
            int res = 0;
            bool EndOfString = false;
            string Value = "";

            while (EndOfString == false & _setValue[CurPos]!=';' & (int.TryParse(_setValue[CurPos].ToString(), out res) | _setValue[CurPos] == '.'| _setValue[CurPos] == ',' | _setValue[CurPos] == '-'))
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
