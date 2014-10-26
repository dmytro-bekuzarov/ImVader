using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImVaderWebsite.Controllers
{
    using System.Web.Http;

    using ImVader;
    using ImVader.Algorithms;
    using ImVader.ShortestPaths;

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
            int[] res = null;
            try
            {
                topSort.SortGraph(graph);
                var order = topSort.GetOrder();
                res = new int[order.Length];
                for (int i = 0; i < order.Length; i++)
                    res[i] = graph.IndexedValue(order[i]);
            }
            catch (InvalidOperationException ex)
            {
                res = null;
            }

            return res;
        }
    }
}