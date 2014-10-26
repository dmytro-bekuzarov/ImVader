// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TopologicalSort.cs" company="Sigma">
//   It's getting boring to write copyrights
// </copyright>
// <summary>
//   The topological sort test.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImVaderUnitTests
{
    using ImVader;
    using ImVader.Algorithms;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The topological sort test.
    /// </summary>
    [TestClass]
    public class TopologicalSort
    {
        /// <summary>
        /// The sort.
        /// </summary>
        [TestMethod]
        public void Sort()
        {
            var g = new ListGraph<int, WeightedEdge>(4);
            g.AddEdge(new WeightedEdge(0, 2, 1));
            g.AddEdge(new WeightedEdge(0, 1, 1));
            g.AddEdge(new WeightedEdge(1, 3, 1));
            g.AddEdge(new WeightedEdge(2, 1, 1));
            g.AddEdge(new WeightedEdge(2, 3, 1));
           
            var topSort = new TopologicalSort<int, WeightedEdge>();
            topSort.SortGraph(g);
            var order = topSort.GetOrder();
        }

        /// <summary>
        /// The sort.
        /// </summary>
        [TestMethod]
        public void Sort1()
        {
            var g = new ListGraph<int, WeightedEdge>(1);

            var topSort = new TopologicalSort<int, WeightedEdge>();
            topSort.SortGraph(g);
            var order = topSort.GetOrder();
        }
    }
}
