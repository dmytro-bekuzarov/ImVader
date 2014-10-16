namespace ImVaderWebsite.Controllers
{
    using System.Collections.Generic;
    using System.Security.Cryptography.X509Certificates;
    using System.Web.Http;

    using ImVader;
    using ImVader.ShortestPaths;

    using Newtonsoft.Json;

    public class ShortestPathController : ApiController
    {
        //public IEnumerable<int> Get(IEnumerable<int> vertices, IEnumerable<UnweightedEdge> edges, int vertex1, int vertex2)
        //{
        //    var graph = new DirectedListGraph<int, UnweightedEdge>();
        //    var vList = new List<int>(vertices);
        //    var eList = new List<UnweightedEdge>(edges);
        //    graph.Init(eList, vList);
        //    var d = new Dijkstra<int, UnweightedEdge>(graph, vertex1);
        //    return d.PathToAsIds(vertex2);

        //}
        public IEnumerable<int> Post([FromBody]SGraph g)
        {
            var graph = new DirectedListGraph<int, UnweightedEdge>();
            graph.Init(g.Edges, g.Vertices);
            var d = new Dijkstra<int, UnweightedEdge>(graph, g.Vertex1);
            IEnumerable<int> ids =  d.PathToAsIds(g.Vertex2);
            return ids;
        }

        public class SGraph
        {
            public IEnumerable<int> Vertices;

            public IEnumerable<UnweightedEdge> Edges;

            public int Vertex1;

            public int Vertex2;
        }

    }
}
