// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnweightedEdge.cs" company="Sigma">
//   Sigma
// </copyright>
// <summary>
//   The unweighted edge.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImVader
{
    /// <summary>
    /// Represents unweighted edge of the graph
    /// </summary>
    public class UnweightedEdge : Edge
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnweightedEdge"/> class.
        /// </summary>
        /// <param name="v">
        /// Vertex where edge starts
        /// </param>
        /// <param name="w">
        /// Vertex where edge finished
        /// </param>
        public UnweightedEdge(int v, int w)
            : base(v, w)
        {
        }
    }
}
