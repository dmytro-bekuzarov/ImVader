// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DirectedMatrixGraph.cs" company="Sigma">
//   It's a totally free software
// </copyright>
// <summary>
//   Defines the DirectedMatrixGraph type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImVader
{
    /// <summary>
    /// Defines directed matrix-based graph.
    /// </summary>
    /// <typeparam name="TV">
    /// Type of the data stored in the vertices.
    /// </typeparam>
    /// <typeparam name="TE">
    /// Type of the edges connecting vertices.
    /// </typeparam>
    public class DirectedMatrixGraph<TV, TE> : MatrixGraph<TV, TE>, IDirectedGraph
        where TE : Edge
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectedMatrixGraph{TV,TE}"/> class.
        /// </summary>
        /// <param name="capacity">
        /// Initial number of vertices.
        /// </param>
        public DirectedMatrixGraph(int capacity = 0)
            : base(capacity)
        {
        }

        /// <summary>
        /// Adds a new edge to the graph.
        /// </summary>
        /// <param name="e">
        /// The edge to add.
        /// </param>
        /// <returns>
        /// The index of the created edge.
        /// </returns>
        public override int AddEdge(TE e)
        {
            CheckVerticesIndexes(e.From, e.To);
            Edges.Add(++LastEdgeIndex, e);
            this[e.From, e.To].Add(LastEdgeIndex);
            EdgesCount++;
            return LastEdgeIndex;
        }

        /// <summary>
        /// Removes the edge with the specified index.
        /// </summary>
        /// <param name="index">
        /// The index of the edge.
        /// </param>
        public override void RemoveEdge(int index)
        {
            Edge e = Edges[index];
            this[e.From, e.To].Remove(index);
            EdgesCount--;
            if (index == LastEdgeIndex) LastEdgeIndex--;
            Edges.Remove(index);
        }
    }
}
