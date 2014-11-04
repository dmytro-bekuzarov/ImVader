using System.Collections.Generic;
using System.Web.Http;

namespace ImVaderWebsite.Controllers
{
    using ImVader;
    using ImVader.Algorithms;

    public class MinimumCutsController : ApiController
    {
        public class SGraph
        {
            public IEnumerable<int> Vertices;

            public IEnumerable<WeightedEdge> Edges;
        }

        public List<int> Post([FromBody]SGraph g)
        {
            var graph = new ListGraph<int, WeightedEdge>();
            graph.Init(g.Edges, g.Vertices);
            var sc = new MinimumCuts<int, WeightedEdge>(graph);
            var ids = sc.BestCut;
            return ids;
        }
    }
}