using System;
using System.Collections.Generic;

namespace ImVaderWebsite.Controllers
{
    using System.Web.Http;

    using ImVader;
    using ImVader.Algorithms;

    public class TopologicalController : ApiController
    {
        public class SGraph
        {
            public IEnumerable<int> Vertices;

            public IEnumerable<WeightedEdge> Edges;
        }

        public IEnumerable<int> Post([FromBody]SGraph g)
        {
            var graph = new DirectedListGraph<int, WeightedEdge>();
            graph.Init(g.Edges, g.Vertices);
            var topSort = new TopologicalSort<int, WeightedEdge>();
            int[] res;
            try
            {
                topSort.SortGraph(graph);
                var order = topSort.GetOrder();
                res = new int[order.Length];
                for (int i = 0; i < order.Length; i++)
                    res[i] = graph.IndexedValue(order[i]);
            }
            catch (InvalidOperationException)
            {
                res = null;
            }

            return res;
        }
    }
}