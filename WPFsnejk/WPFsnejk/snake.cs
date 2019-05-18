using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFsnejk
{
    class snake
    {
        public snakePart head { get; private set; }
        public List<snakePart> snakeParts { get; private set; }

        Random rnd = new Random();

        public snake()
        {
            head = new snakePart(20, 20);
            snakeParts = new List<snakePart>();
            snakeParts.Add(new snakePart(19, 20));
            snakeParts.Add(new snakePart(18, 20));
            snakeParts.Add(new snakePart(17, 20));
        }

        public void setOnGrid()
        {
            Grid.SetColumn(head.ellipse, head.X);
            Grid.SetRow(head.ellipse, head.Y);
            foreach (snakePart snakePart in snakeParts)
            {
                Grid.SetColumn(snakePart.ellipse, snakePart.X);
                Grid.SetRow(snakePart.ellipse, snakePart.Y);
            }
        }
    }
}
