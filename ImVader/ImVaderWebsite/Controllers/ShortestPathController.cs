namespace ImVaderWebsite.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using ImVader;

    public class ShortestPathController : ApiController
    {
        public IEnumerable<Edge> Get(ListGraph<int, UnweightedEdge> graph)
        {
            return graph.FindShortestPath();
        }
    }
}
