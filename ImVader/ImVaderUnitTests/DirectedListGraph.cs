namespace ImVaderUnitTests
{
    using System.Collections.Generic;
    using System.Linq;

    using ImVader;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DirectedListGraph
    {
        [TestMethod]
        public void ManageEdges()
        {
            var m = new DirectedListGraph<int, WeightedEdge>(10);
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
            var m = new DirectedListGraph<int, WeightedEdge>(10);

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
                Assert.AreEqual(1, v.Count);
                Assert.AreEqual(m.GetVertexData((i + 1) % 10), m.GetVertexData(v[0]));
            }
        }
    }
}
