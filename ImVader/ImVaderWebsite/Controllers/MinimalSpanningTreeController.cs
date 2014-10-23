namespace ImVaderWebsite.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using ImVader;
    using ImVader.Algorithms;

    public class MinimalSpanningTreeController : ApiController
    {

        public class SGraph
        {
            public IEnumerable<int> Vertices;

            public IEnumerable<WeightedEdge> Edges;
        }
        public int[][] Post([FromBody]SGraph g)
        {
            var graph = new ListGraph<int, WeightedEdge>();
            graph.Init(g.Edges, g.Vertices);
            var mst = new MinimalSpanningTree<int, WeightedEdge>(graph);
            var mstvert = mst.GetMstVertices();
            return mstvert;
        }

    }
}
