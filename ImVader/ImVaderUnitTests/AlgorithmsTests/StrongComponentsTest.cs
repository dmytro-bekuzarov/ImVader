namespace ImVaderUnitTests.AlgorithmsTests
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ImVader;
    using ImVader.Algorithms;
    /// <summary>
    /// Summary description for StrongComponentsTest
    /// </summary>
    [TestClass]
    public class StrongComponentsTest
    {
        [TestMethod]
        public void UndirectedListGraphTest()
        {
            var g = new ListGraph<int, Edge>(4);
            g.AddEdge(new UnweightedEdge(0, 1));
            g.AddEdge(new UnweightedEdge(2, 3));
            g.AddEdge(new UnweightedEdge(2, 1));
            var strongComponents = new StrongComponents<int, Edge>(g);
            Assert.IsNotNull(strongComponents);
            Assert.IsTrue(strongComponents.Components.Count() == 1);
        }

        [TestMethod]
        public void DirectedListGraphTest()
        {
            var g = new DirectedListGraph<int, Edge>(6);
            g.AddEdge(new UnweightedEdge(0, 1));
            g.AddEdge(new UnweightedEdge(2, 0));
            g.AddEdge(new UnweightedEdge(2, 1));
            g.AddEdge(new UnweightedEdge(3, 4));
            g.AddEdge(new UnweightedEdge(4, 5));
            g.AddEdge(new UnweightedEdge(5, 3));
            var strongComponents = new StrongComponents<int, Edge>(g);
            Assert.IsNotNull(strongComponents);
            Assert.IsTrue(strongComponents.Components.Count() == 4);
        }

        [TestMethod]
        public void UndirectedMaxtrixGraphTest()
        {
            var g = new MatrixGraph<int, Edge>(6);
            g.AddEdge(new UnweightedEdge(0, 1));
            g.AddEdge(new UnweightedEdge(2, 0));
            g.AddEdge(new UnweightedEdge(2, 1));
            g.AddEdge(new UnweightedEdge(3, 4));
            g.AddEdge(new UnweightedEdge(4, 5));
            g.AddEdge(new UnweightedEdge(5, 3));
            var strongComponents = new StrongComponents<int, Edge>(g);
            Assert.IsNotNull(strongComponents);
            Assert.IsTrue(strongComponents.Components.Count() == 2);
        }

        [TestMethod]
        public void DirectedMaxtrixGraphTest()
        {
            var g = new DirectedMatrixGraph<int, Edge>(6);
            g.AddEdge(new UnweightedEdge(0, 1));
            g.AddEdge(new UnweightedEdge(2, 0));
            g.AddEdge(new UnweightedEdge(2, 1));
            g.AddEdge(new UnweightedEdge(3, 4));
            g.AddEdge(new UnweightedEdge(4, 5));
            g.AddEdge(new UnweightedEdge(5, 3));
            g.AddEdge(new UnweightedEdge(0, 4));
            g.AddEdge(new UnweightedEdge(4, 0));
            var strongComponents = new StrongComponents<int, Edge>(g);
            Assert.IsNotNull(strongComponents);
            Assert.IsTrue(strongComponents.Components.Count == 3);
        }
    }
}
