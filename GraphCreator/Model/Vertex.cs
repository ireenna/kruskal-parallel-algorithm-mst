using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCreator.Model
{
    public class Vertex
    {
        public const int RADIUS = 15;
        public int Index { get; set; }
        public Pos Pos { get; private set; }
        public Style Style { get; private set; }
        public bool IsLocked { get; set; }
        public bool IsVisited { get; set; }
        public double Mark { get; set; }
        public Vertex(int index, Pos pos)
        {
            Index = index;
            Pos = pos;
            Style = new Style();
            IsLocked = false;
            IsVisited = false;
        }

        public override string ToString()
        {
            return Index.ToString();
        }
    }
}
