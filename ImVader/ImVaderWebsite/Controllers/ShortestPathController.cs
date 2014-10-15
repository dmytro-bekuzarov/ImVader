namespace ImVaderWebsite.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using ImVader;
    using ImVader.ShortestPaths;

    public class ShortestPathController : ApiController
    {
        public IEnumerable<int> Get(IEnumerable<int> vertices, IEnumerable<UnweightedEdge> edges, int vertex1, int vertex2)
        {
            var graph = new DirectedListGraph<int, UnweightedEdge>();
            var vList = new List<int>(vertices);
            var eList = new List<UnweightedEdge>(edges);
            graph.Init(eList, vList);
            var d = new Dijkstra<int, UnweightedEdge>(graph, vertex1);
            var allIds = d.PathToAsIds(vertex2);
            return allIds.Select(graph.GetVertexData);
        }
    }
}
