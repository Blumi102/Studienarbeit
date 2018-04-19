using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandHandling;
using System.Windows.Media.Media3D;

namespace Printing
{
    /// <summary>
    /// This class coordinates the actions which are necessary for printing.
    /// </summary>

    public class PrintingParameters
    {
        public static bool RelativeCoordinates = false;
        public static Point3D CurPoint;
        public static double CurE;
        public static List<Command> Commands = new List<Command>();
        public static double Speed = 0.0;
      

    }
}
