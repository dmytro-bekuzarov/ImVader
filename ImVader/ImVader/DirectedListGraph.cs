// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DirectedListGraph.cs" company="Sigma">
//   Sigma
// </copyright>
// <summary>
//   Defines the DirectedListGraph type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImVader
{
    /// <summary>
    /// Defines directed list-based graph
    /// </summary>
    /// <typeparam name="TV">
    /// Type of data stored in vertices
    /// </typeparam>
    /// <typeparam name="TE">
    /// Type of edge used to connect vertices
    /// </typeparam>
    public class DirectedListGraph<TV, TE> : ListGraph<TV, TE>
        where TE : Edge
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectedListGraph{TV,TE}"/> class.
        /// </summary>
        /// <param name="vertexCount">
        /// Count of vertices in graph
        /// </param>
        public DirectedListGraph(int vertexCount)
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
            this.CheckArguments(e.W);
            this.CheckArguments(e.V);
            this.AdjacencyList[e.V].Add(e);
        }
    }
}
