namespace ImVaderUnitTests
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using ImVader;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ListGraph
    {
        [TestMethod]
        public void InitEmpty()
        {
            var m = new ListGraph<int, Edge>();
            Assert.AreEqual(0, m.EdgesCount);
            Assert.AreEqual(0, m.VertexCount);
        }

        [TestMethod]
        public void InitWithCapacity()
        {
            var m = new ListGraph<int, Edge>(10);
            Assert.AreEqual(0, m.EdgesCount);
            Assert.AreEqual(10, m.VertexCount);
            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(0, m.GetVertexData(i));
                m.SetVertexData(i, i);
                Assert.AreEqual(i, m.GetVertexData(i));
            }
        }

        [TestMethod]
        public void AddVertex()
        {
            var m = new ListGraph<int, Edge>();
            for (int i = 0; i < 10; i++)
            {
                m.AddVertex(i);
                Assert.AreEqual(i, m.GetVertexData(i));
                Assert.AreEqual(i + 1, m.VertexCount);
            }
        }

        [TestMethod]
        public void RemoveVertex()
        {
            var m = new ListGraph<int, Edge>(10);
            for (int i = 0; i < 10; i++)
            {
                m.RemoveVertex(i);
                Assert.AreEqual(10 - i - 1, m.VertexCount);
            }

            Assert.AreEqual(0, m.VertexCount);
        }

        [TestMethod]
        public void ManageEdges()
        {
            var m = new ListGraph<int, WeightedEdge>(10);
            var edges = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                int edgeIndex = m.AddEdge(new WeightedEdge(i, (i + 1) % 10, i + 2.2));
                edges.Add(edgeIndex);
                Assert.AreEqual(i + 1, m.EdgesCount);
                var edge = m.GetEdge(edgeIndex);
                Assert.AreEqual(i + 2.2, edge.Weight);
                edge.Weight += 2;
                Assert.AreEqual(i + 4.2, edge.Weight);
            }

            for (int i = 0; i < 10; i++)
            {
                m.RemoveEdge(edges[i]);
                Assert.AreEqual(10 - i - 1, m.EdgesCount);
            }
        }

        [TestMethod]
        public void GetAdjacentVertices()
        {
            var m = new ListGraph<int, WeightedEdge>(10);

            // Creating a ring graph
            for (int i = 0; i < 10; i++)
            {
                m.SetVertexData(i, i);
                m.AddEdge(new WeightedEdge(i, (i + 1) % 10, i + 2.2));
            }

            for (int i = 0; i < 10; i++)
            {
                var v = m.GetAdjacentVertices(i).ToList();
                Assert.AreNotEqual(null, v);
                Assert.AreEqual(2, v.Count);
                int index;
                if (i > 0 && i < 9) index = i + 1;
                else if (i == 0) index = 9;
                else index = 8;

                // Because adjacent vertices are in an unpredictable order (maybe)
                Assert.AreEqual(
                    true,
                    m.GetVertexData(index) == m.GetVertexData(v[0]) || m.GetVertexData(index) == m.GetVertexData(v[1]));
            }
        }

        [TestMethod]
        public void SerializeGraph()
        {
            var graph = new ListGraph<int, Edge>(5);
            graph.AddEdge(
                new WeightedEdge(1, 2, 3));
            graph.AddEdge(
                new WeightedEdge(1, 3, 1.5));

            Stream s = System.IO.File.OpenWrite("test.txt");
            graph.SaveToStream(new StreamWriter(s));
        }

        [TestMethod]
        public void DeserializeGraph()
        {
            Stream s = System.IO.File.OpenRead("test.txt");
            var graph =
                (ListGraph<int, Edge>)Graph<int, Edge>.LoadFromJsonFile(new StreamReader(s));
        }

        [TestMethod]
        public void TestSerialization()
        {
            ListGraph<int, Edge> deserializedGraph;
            var initialGraph = new ListGraph<int, Edge>(5);
            initialGraph.AddEdge(
                new WeightedEdge(1, 2, 3));
            initialGraph.AddEdge(
                new WeightedEdge(1, 3, 1.5));

            using (Stream s = File.OpenWrite("test.txt"))
            {
                initialGraph.SaveToStream(new StreamWriter(s));
            }

            using (Stream s2 = File.OpenRead("test.txt"))
            {
                deserializedGraph = (ListGraph<int, Edge>)Graph<int, Edge>.LoadFromJsonFile(new StreamReader(s2));
            }
        }
    }
}
