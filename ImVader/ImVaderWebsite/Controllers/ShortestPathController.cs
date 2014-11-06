namespace ImVaderWebsite.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using ImVader;
    using ImVader.Algorithms.ShortestPaths;

    public class ShortestPathController : ApiController
    {

        public class SGraph
        {
            public IEnumerable<int> Vertices;

            public IEnumerable<WeightedEdge> Edges;

            public int Vertex1;

            public int Vertex2;
        }

        public IEnumerable<int> Post([FromBody]SGraph g)
        {
            var graph = new DirectedListGraph<int, WeightedEdge>();
            graph.Init(g.Edges, g.Vertices);
            var d = new Dijkstra<int, WeightedEdge>(graph, g.Vertex1);
            IEnumerable<int> ids = d.PathToAsIds(g.Vertex2);
            return ids;
        }

    }
}
