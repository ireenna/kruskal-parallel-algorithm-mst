using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCreator.Model
{
    public class Project
    {
        public List<Vertex> Vertexes { get; private set; } = new List<Vertex>();
        public List<Edge> Edges { get; private set; } = new List<Edge>();
    }
}
