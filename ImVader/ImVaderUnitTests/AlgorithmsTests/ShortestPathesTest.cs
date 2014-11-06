namespace ImVaderUnitTests.AlgorithmsTests
{
    using ImVader;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ShortestPathesTest
    {
        [TestMethod]
        public void TestShortestPathesUndirected()
        {
            var g = new ListGraph<int, WeightedEdge>(5);
            g.AddEdge(new WeightedEdge(0, 1, 1));
            g.AddEdge(new WeightedEdge(0, 2, 1));
            g.AddEdge(new WeightedEdge(1, 2, 1));
            g.AddEdge(new WeightedEdge(3, 4, 1));
            g.AddEdge(new WeightedEdge(1, 4, 1));
            var pathes = g.GetShortestPathesList();
            Assert.IsNotNull(pathes);
            Assert.AreEqual(pathes[0, 1], 1);
            Assert.AreEqual(pathes[0, 2], 1);
            Assert.AreEqual(pathes[1, 2], 1);
            Assert.AreEqual(pathes[3, 4], 1);
            Assert.AreEqual(pathes[1, 4], 1);
        }

        [TestMethod]
        public void TestShortestPathesDirected()
        {
            var g = new DirectedListGraph<int, WeightedEdge>(5);
            g.AddEdge(new WeightedEdge(0, 1, 1));
            g.AddEdge(new WeightedEdge(0, 2, 1));
            g.AddEdge(new WeightedEdge(1, 2, 1));
            g.AddEdge(new WeightedEdge(3, 4, 1));
            g.AddEdge(new WeightedEdge(1, 4, 1));
            var pathes = g.GetShortestPathesList();
            Assert.IsNotNull(pathes);
            Assert.AreEqual(pathes[0, 1], 1);
            Assert.AreEqual(pathes[0, 2], 1);
            Assert.AreEqual(pathes[1, 2], 1);
            Assert.AreEqual(pathes[3, 4], 1);
            Assert.AreEqual(pathes[1, 4], 1);
        }

        [TestMethod]
        public void TestShortestPathesWithUnweightedEdge()
        {
            var g = new DirectedListGraph<int, UnweightedEdge>(5);
            g.AddEdge(new UnweightedEdge(0, 1));
            g.AddEdge(new UnweightedEdge(0, 2));
            g.AddEdge(new UnweightedEdge(1, 2));
            g.AddEdge(new UnweightedEdge(3, 4));
            g.AddEdge(new UnweightedEdge(1, 4));
            var pathes = g.GetShortestPathesList();
            Assert.IsNotNull(pathes);
            Assert.AreEqual(pathes[0, 1], 1);
            Assert.AreEqual(pathes[0, 2], 1);
            Assert.AreEqual(pathes[1, 2], 1);
            Assert.AreEqual(pathes[3, 4], 1);
            Assert.AreEqual(pathes[1, 4], 1);
        }
    }
}
