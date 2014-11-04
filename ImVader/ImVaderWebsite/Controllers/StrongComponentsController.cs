namespace ImVaderWebsite.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using ImVader.Algorithms;
    using ImVader;
    public class StrongComponentsController : ApiController
    {
        public class SGraph
        {
            public IEnumerable<int> Vertices;

            public IEnumerable<UnweightedEdge> Edges;
        }

        public List<List<int>> Post([FromBody]SGraph g)
        {
            var graph = new DirectedListGraph<int, UnweightedEdge>(g.Vertices.Count());
            foreach(var edge in g.Edges)
            {
                graph.AddEdge(edge);
            }          
            var sc  = new StrongComponents<int, UnweightedEdge>(graph);
            var ids = sc.Components;
            return ids;
        }
    }
}