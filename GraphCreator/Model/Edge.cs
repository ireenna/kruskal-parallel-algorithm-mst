namespace GraphCreator.Model
{
    public class Edge
    {
        public Vertex StartVertex { get; set; }
        public Vertex EndVertex { get; set; }
        public int Weight { get; set; }
        public Style Style { get; private set; }
        public bool IsDenied { get; set; }
        public bool IsDeniedByMain { get; set; }
        public bool IsSpanning { get; set; }
        public Edge(Vertex start, Vertex end)
        {
            StartVertex = start;
            EndVertex = end;
            Weight = 0;
            Style = new Style();
            IsSpanning = false;
        }

        public bool ContainVertex(Vertex vertex) => StartVertex == vertex || EndVertex == vertex;
    }
}
