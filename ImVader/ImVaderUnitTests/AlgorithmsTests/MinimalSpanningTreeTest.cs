using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImVaderUnitTests.AlgorithmsTests
{
    using ImVader;
    using ImVader.Algorithms;

    [TestClass]
    public class MinimalSpanningTreeTest
    {
        [TestMethod]
        public void MstTest()
        {
            var g = new ListGraph<int, WeightedEdge>(6);
            g.AddEdge(new WeightedEdge(0, 1, 2));
            g.AddEdge(new WeightedEdge(1, 3, 1));
            g.AddEdge(new WeightedEdge(2, 4, 3));
            g.AddEdge(new WeightedEdge(2, 3, 10));
            g.AddEdge(new WeightedEdge(0, 1, 2));
            g.AddEdge(new WeightedEdge(3, 5, 2));
            g.AddEdge(new WeightedEdge(4, 5, 5));
            var mst = new MinimalSpanningTree<int, WeightedEdge>(g);
            Assert.AreEqual(13.0, mst.GetMstWeight());
        }

    }
}
