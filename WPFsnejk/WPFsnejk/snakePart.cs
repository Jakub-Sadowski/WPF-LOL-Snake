using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPFsnejk
{
    class snakePart
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Ellipse ellipse { get; private set; }

        public snakePart(int x, int y)
        {
            this.X = x;
            this.Y = y;


            ellipse = new Ellipse {
                Fill = Brushes.Black,
                Width = 15,
                Height = 15,
            };

        }
    }
}
