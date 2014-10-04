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
            // TODO: Should be rewritten
            var g = new MatrixGraph<int, UnweightedEdge>(10); 
        }
    }
}
