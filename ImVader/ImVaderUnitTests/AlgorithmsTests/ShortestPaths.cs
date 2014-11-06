namespace ImVaderUnitTests.AlgorithmsTests
{
    using System;
    using System.Linq;
    using ImVader;
    using ImVader.Algorithms.ShortestPaths;
    using ImVader.Utils;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ShortestPaths
    {
        [TestMethod]
        public void ListDijkstra()
        {
            var m = new DirectedListGraph<int, WeightedEdge>(6);

            m.AddEdge(new WeightedEdge(0, 2, 1));
            m.AddEdge(new WeightedEdge(0, 1, 7));
            m.AddEdge(new WeightedEdge(0, 5, 4));
            m.AddEdge(new WeightedEdge(5, 3, 4));
            m.AddEdge(new WeightedEdge(4, 3, 1));
            m.AddEdge(new WeightedEdge(3, 2, 2));
            m.AddEdge(new WeightedEdge(2, 4, 3));
            m.AddEdge(new WeightedEdge(2, 1, 3));

            var d = new Dijkstra<int, WeightedEdge>(m, 0);

            var all = d.PathTo(3);
            var enumerable = all as WeightedEdge[] ?? all.ToArray();

            Assert.AreEqual(3, enumerable.Length);
            Assert.AreEqual(5, d.GetDistTo(3));
        }

        [TestMethod]
        public void MatrixDijkstra()
        {
            var m = new DirectedListGraph<int, WeightedEdge>(6);

            m.AddEdge(new WeightedEdge(0, 2, 1));
            m.AddEdge(new WeightedEdge(0, 1, 7));
            m.AddEdge(new WeightedEdge(0, 5, 4));
            m.AddEdge(new WeightedEdge(5, 3, 4));
            m.AddEdge(new WeightedEdge(4, 3, 1));
            m.AddEdge(new WeightedEdge(3, 2, 2));
            m.AddEdge(new WeightedEdge(2, 4, 3));
            m.AddEdge(new WeightedEdge(2, 1, 3));

            var d = new Dijkstra<int, WeightedEdge>(m, 0);

            var all = d.PathTo(3);
            var enumerable = all as WeightedEdge[] ?? all.ToArray();

            Assert.AreEqual(3, enumerable.Length);
        }

        [TestMethod]
        public void UnWeightedDijkstra()
        {
            var m = new DirectedListGraph<int, UnweightedEdge>(6);

            m.AddEdge(new UnweightedEdge(0, 2));
            m.AddEdge(new UnweightedEdge(0, 1));
            m.AddEdge(new UnweightedEdge(0, 5));
            m.AddEdge(new UnweightedEdge(5, 3));
            m.AddEdge(new UnweightedEdge(4, 3));
            m.AddEdge(new UnweightedEdge(3, 2));
            m.AddEdge(new UnweightedEdge(2, 4));
            m.AddEdge(new UnweightedEdge(2, 1));

            var d = new Dijkstra<int, UnweightedEdge>(m, 0);

            var all = d.PathTo(1);
            var allIds = d.PathToAsIds(1);
            var enumerable = all as UnweightedEdge[] ?? all.ToArray();
            var enumerableIds = allIds as int[] ?? allIds.ToArray();
            Assert.AreEqual(2, enumerableIds.Length);
            Assert.AreEqual(1, enumerable.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "edge ImVader.WeightedEdge has negative weight")]
        public void NegativeEdgeDijkstra()
        {
            var m = new DirectedListGraph<int, WeightedEdge>(6);

            m.AddEdge(new WeightedEdge(0, 2, 1));
            m.AddEdge(new WeightedEdge(0, 1, 7));
            m.AddEdge(new WeightedEdge(0, 5, 4));
            m.AddEdge(new WeightedEdge(5, 3, -4));
            m.AddEdge(new WeightedEdge(4, 3, 1));
            m.AddEdge(new WeightedEdge(3, 2, 2));
            m.AddEdge(new WeightedEdge(2, 4, 3));
            m.AddEdge(new WeightedEdge(2, 1, 3));

            var d = new Dijkstra<int, WeightedEdge>(m, 0);

            var all = d.PathTo(3);
            var enumerable = all as WeightedEdge[] ?? all.ToArray();

            Assert.AreEqual(3, enumerable.Length);
        }

        [TestMethod]
        public void MinPq1()
        {
            var pq = new MinPq<string>();
            pq.Insert("this");
            pq.Insert("is");
            pq.Insert("a");
            pq.Insert("test");

            Assert.AreEqual("a", pq.DelMin());
            Assert.AreEqual("is", pq.DelMin());
            Assert.AreEqual("test", pq.DelMin());
            Assert.AreEqual("this", pq.DelMin());
        }

        [TestMethod]
        public void MinPq2()
        {
            var pq = new MinPq<int>();
            pq.Insert(3);
            pq.Insert(6);
            pq.Insert(1);
            pq.Insert(7);

            Assert.AreEqual(1, pq.DelMin());
            Assert.AreEqual(3, pq.DelMin());
            Assert.AreEqual(6, pq.DelMin());
            Assert.AreEqual(7, pq.DelMin());
        }

        [TestMethod]
        public void MinPq3()
        {
            var pq = new MinPq<Node>();
            pq.Insert(new Node(0, 3));
            pq.Insert(new Node(1, 6));
            pq.Insert(new Node(2, 1));
            pq.Insert(new Node(3, 7));

            Assert.AreEqual(1, pq.DelMin().Weight);
            Assert.AreEqual(3, pq.DelMin().Weight);
            Assert.AreEqual(6, pq.DelMin().Weight);
            Assert.AreEqual(7, pq.DelMin().Weight);
        }

        [TestMethod]
        public void MinPq4()
        {
            var pq = new MinPq<Node>();
            pq.Insert(new Node(0, 3));
            pq.Insert(new Node(1, 6));
            pq.Insert(new Node(2, 1));
            pq.Insert(new Node(3, 7));

            var all = pq.GetAsEnumerable();
            var enumerable = all as Node[] ?? all.ToArray();

            Assert.AreEqual(4, enumerable.Length);
        }
    }
}
