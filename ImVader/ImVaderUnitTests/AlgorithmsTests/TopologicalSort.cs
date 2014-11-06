namespace ImVaderUnitTests.AlgorithmsTests
{
    using ImVader;
    using ImVader.Algorithms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TopologicalSort
    {
        [TestMethod]
        public void Sort()
        {
            var g = new DirectedListGraph<int, WeightedEdge>(4);
            g.AddEdge(new WeightedEdge(0, 2, 1));
            g.AddEdge(new WeightedEdge(0, 1, 1));
            g.AddEdge(new WeightedEdge(1, 3, 1));
            g.AddEdge(new WeightedEdge(2, 1, 1));
            g.AddEdge(new WeightedEdge(2, 3, 1));
            var topSort = new TopologicalSort<int, WeightedEdge>();
            topSort.SortGraph(g);
            var order = topSort.GetOrder();
            Assert.AreEqual(order.GetLength(0), 4);
        }

        [TestMethod]
        public void Sort1()
        {
            var g = new ListGraph<int, WeightedEdge>(1);
            var topSort = new TopologicalSort<int, WeightedEdge>();
            topSort.SortGraph(g);
            var order = topSort.GetOrder();
            Assert.AreEqual(order.GetLength(0), 1);
        }
    }
}
