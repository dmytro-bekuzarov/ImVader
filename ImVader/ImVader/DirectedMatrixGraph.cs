// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DirectedMatrixGraph.cs" company="Sigma">
//   Sigma
// </copyright>
// <summary>
//   Defines the DirectedMatrixGraph type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImVader
{
    /// <summary>
    /// Defines directed matrix-based graph
    /// </summary>
    /// <typeparam name="TV">
    /// Type of information stored in the vertex
    /// </typeparam>
    /// <typeparam name="TE">
    /// Type of edges used to connect vertices
    /// </typeparam>
    public class DirectedMatrixGraph<TV, TE> : MatrixGraph<TV, TE>
        where TV : Vertex<TV>
        where TE : Edge
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectedMatrixGraph{TV,TE}"/> class.
        /// </summary>
        /// <param name="vertexCount">
        /// Count of vertices
        /// </param>
        public DirectedMatrixGraph(int vertexCount)
            : base(vertexCount)
        {
        }

        /// <summary>
        /// Adds new edge between two vertices of the graph
        /// </summary>
        /// <param name="e">
        /// Edge to be added
        /// </param>
        public override void AddEdge(TE e)
        {
            this.CheckArguments(e.V);
            this.CheckArguments(e.W);
            this.Matrix[e.V][e.W]++;
        }
    }
}
