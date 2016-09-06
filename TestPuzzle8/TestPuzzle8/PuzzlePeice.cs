using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TestPuzzle8
{
    public class PuzzlePeice
    {
        public int Value { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Point GetPoint()
        {
            return new Point(X, Y);
        }

        public void SwapLocation(PuzzlePeice peice)
        {
            int x = peice.X;
            int y = peice.Y;

            peice.X = X;
            peice.Y = Y;

            X = x;
            Y = y;
        }
    }
}
