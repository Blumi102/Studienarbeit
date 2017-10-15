using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FileHandling;
using System.Windows.Media.Media3D;

namespace MuMprint
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            #region Set starting conditions
            //Voraussichtliche Zeit kann erst nach einlesen des gCodes angezeigt werden
            LabelVoraussZeit.Visibility = Visibility.Hidden; 

            //Programm status aus der Progressbar in Prozentanzeige übernehmen
            if (ProzentLabel != null) 
            {
                ProzentLabel.Content = ProgressBar1.Value.ToString() + "%";
            }
            #endregion

            
        }

        #region ButtonHandling
        private void StartDruck_Click(object sender, RoutedEventArgs e)
        {
            ProgressBar1.Value = ProgressBar1.Value + 1;

            Model3DGroup group = new Model3DGroup();

            MeshGeometry3D m = new MeshGeometry3D();

            m.Positions.Add(new Point3D(1, 0, 0));
            m.Positions.Add(new Point3D(0, 1, 0));
            m.Positions.Add(new Point3D(0, 0, 1));

            m.TriangleIndices.Add(0);
            m.TriangleIndices.Add(1);
            m.TriangleIndices.Add(2);

            group.Children.Add(new GeometryModel3D(m, new DiffuseMaterial(Brushes.Cyan)));

            group.Children.Add(new DirectionalLight(Colors.White, new Vector3D(0, 0, -1)));

            model.Content = group;
        }

        private void Durchsuchen_Click(object sender, RoutedEventArgs e)
        {     
            PfadGcode.Text = GCode.HandleGCode();
        }
        #endregion

        #region Printing
        private void ProgressBar1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //Programm status aus der Progressbar in Prozentanzeige übernehmen
            if (ProzentLabel != null)
            {
                ProzentLabel.Content = ProgressBar1.Value.ToString() + "%";
            }
        }
        #endregion
    }
}
