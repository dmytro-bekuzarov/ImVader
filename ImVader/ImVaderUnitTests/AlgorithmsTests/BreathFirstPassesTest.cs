using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImVaderUnitTests.AlgorithmsTests
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Runtime.InteropServices;

    using ImVader;
    using ImVader.Algorithms;

    /// <summary>
    /// Summary description for BreathFirstPassesTest
    /// </summary>
    [TestClass]
    public class BreadthFirstPassesTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), AllowDerivedTypes = false)]
        public void InitTest()
        {
            var g = new MatrixGraph<int, Edge>(4);
            g.AddEdge(new UnweightedEdge(0, 1));
            g.AddEdge(new UnweightedEdge(2, 3));
            var bfs = new BreadthFirstPathes<int, Edge>(g, -1);
        }

        [TestMethod]
        public void BfsTest()
        {
            var g = new MatrixGraph<int, Edge>(4);
            g.AddEdge(new UnweightedEdge(0, 1));
            g.AddEdge(new UnweightedEdge(2, 3));
            var bfs = new BreadthFirstPathes<int, Edge>(g, 0);
            Assert.IsFalse(bfs.HasPathTo(2));
            Assert.IsFalse(bfs.HasPathTo(2));
            Assert.IsNull(bfs.PathTo(2));
            //Assert.AreEqual(bfs.PathTo(0), null);
            Assert.IsTrue(bfs.HasPathTo(1));
            Assert.IsTrue(bfs.PathTo(1).ToArray().Length == 1);
        }

        [TestMethod]
        public void DirectedGraphTest()
        {
            var dirListGraph = new DirectedListGraph<int, Edge>(4);
            dirListGraph.AddEdge(new UnweightedEdge(0, 1));
            dirListGraph.AddEdge(new UnweightedEdge(1, 3));
            dirListGraph.AddEdge(new UnweightedEdge(0, 2));
            dirListGraph.AddEdge(new UnweightedEdge(0, 3));
            var bfs = new BreadthFirstPathes<int, Edge>(dirListGraph, 1);
            Assert.IsFalse(bfs.HasPathTo(0));
        }

        [TestMethod]
        public void PathToTest()
        {
            var dirListGraph = new DirectedListGraph<int, Edge>(5);
            dirListGraph.AddEdge(new UnweightedEdge(0, 1));
            dirListGraph.AddEdge(new UnweightedEdge(1, 3));
            dirListGraph.AddEdge(new UnweightedEdge(0, 2));
            dirListGraph.AddEdge(new UnweightedEdge(0, 3));
            dirListGraph.AddEdge(new UnweightedEdge(3, 4));
            var bfs = new BreadthFirstPathes<int, Edge>(dirListGraph, 0);
            var path = bfs.PathTo(4).ToArray();
            int[] path1 = { 0, 1, 3, 4 };
            int[] path2 = { 0, 3, 4 };
            Assert.IsTrue(path.Length == path1.Length || path.Length == path2.Length);
        }
    }
}
