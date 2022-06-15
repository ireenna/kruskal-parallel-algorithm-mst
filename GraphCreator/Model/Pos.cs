using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCreator.Model
{
    public class Pos
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Pos() : this(0, 0)
        { }
        public Pos(int x, int y) => Set(x, y);
        public Pos Set(int x, int y)
        {
            X = x;
            Y = y;
            return this;
        }
        public Pos Add(int x, int y)
        {
            X += x;
            Y += y;
            return this;
        }
        public Pos Sub(int x, int y)
        {
            X -= x;
            Y -= y;
            return this;
        }

        public Pos Clone() => new Pos(X, Y);

        public static implicit operator PointF(Pos pos) => new PointF(pos.X, pos.Y);

    }
}
