namespace ImVaderWebsite.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using ImVader.Algorithms;
    using ImVader;

    public class MinimalSpanningTreeController : ApiController
    {

        public class SGraph
        {
            public IEnumerable<int> Vertices;

            public IEnumerable<WeightedEdge> Edges;
        }
        public List<WeightedEdge> Post([FromBody]SGraph g)
        {
            var graph = new DirectedListGraph<int, WeightedEdge>(g.Vertices.Count());
            foreach (var edge in g.Edges)
            {
                graph.AddEdge(edge);
            }
            var mst = new MinimalSpanningTree<int, WeightedEdge>(graph);
            var mstEdges = mst.GetMstEdges();
            return mstEdges;
        }

    }
}
