namespace ImVaderUnitTests.AlgorithmsTests
{
    using ImVader;
    using ImVader.Algorithms;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MinCutsTest
    {
        [TestMethod]
        public void Test1()
        {
            var MatrixGraph = new MatrixGraph<int, WeightedEdge>(3);
            MatrixGraph.AddEdge(new WeightedEdge(0, 1, 1));
            MatrixGraph.AddEdge(new WeightedEdge(1, 2, 4));
            var minCuts = new MinimumCuts<int, WeightedEdge>(MatrixGraph);
            Assert.IsTrue(minCuts.BestCost == 1);
            Assert.IsTrue(minCuts.BestCut.Count == 2);
        }

        [TestMethod]
        public void Test2()
        {
            var MatrixGraph = new MatrixGraph<int, WeightedEdge>(4);
            MatrixGraph.AddEdge(new WeightedEdge(0, 1, 1));
            MatrixGraph.AddEdge(new WeightedEdge(1, 2, 4));
            MatrixGraph.AddEdge(new WeightedEdge(2, 3, 1));
            MatrixGraph.AddEdge(new WeightedEdge(3, 0, 10));

            var minCuts = new MinimumCuts<int, WeightedEdge>(MatrixGraph);
            Assert.IsTrue(minCuts.BestCost == 2);
            Assert.IsTrue(minCuts.BestCut.Count == 2);
        }
    }
}
