namespace ImVaderUnitTests.AlgorithmsTests
{
    using System;
    using ImVader;
    using ImVader.Algorithms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MinCutsTest
    {
        [TestMethod]
        public void Test1()
        {
            var matrixGraph = new MatrixGraph<int, WeightedEdge>(3);
            matrixGraph.AddEdge(new WeightedEdge(0, 1, 1));
            matrixGraph.AddEdge(new WeightedEdge(1, 2, 4));
            var minCuts = new MinimumCuts<int, WeightedEdge>(matrixGraph);
            Assert.IsTrue(Math.Abs(minCuts.MinimumCost - 1.0) < 0.001);
            Assert.IsTrue(minCuts.MinimumCut.Count == 2);
        }

        [TestMethod]
        public void Test2()
        {
            var matrixGraph = new MatrixGraph<int, WeightedEdge>(4);
            matrixGraph.AddEdge(new WeightedEdge(0, 1, 1));
            matrixGraph.AddEdge(new WeightedEdge(1, 2, 4));
            matrixGraph.AddEdge(new WeightedEdge(2, 3, 1));
            matrixGraph.AddEdge(new WeightedEdge(3, 0, 10));

            var minCuts = new MinimumCuts<int, WeightedEdge>(matrixGraph);
            Assert.IsTrue(Math.Abs(minCuts.MinimumCost - 2) < 0.001);
            Assert.IsTrue(minCuts.MinimumCut.Count == 2);
        }
    }
}
