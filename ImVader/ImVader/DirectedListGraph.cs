// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DirectedListGraph.cs" company="Sigma">
//   It's a totally free software
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
    public class DirectedListGraph<TV, TE> : ListGraph<TV, TE>,IDirectedGraph
        where TE : Edge
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectedListGraph{TV,TE}"/> class.
        /// </summary>
        /// <param name="capacity">
        /// Initial number of vertices.
        /// </param>
        public DirectedListGraph(int capacity = 0)
            : base(capacity)
        {
        }

        /// <summary>
        /// Adds a new edge to the graph
        /// </summary>
        /// <param name="e">
        /// The edge to add
        /// </param>
        /// <returns>
        /// The index of the created edge
        /// </returns>
        public override int AddEdge(TE e)
        {
            //CheckVerticesIndexes(e.From, e.To);
            Edges.Add(++LastEdgeIndex, e);
            AdjacencyList[Indexes.IndexOf(e.From)].Add(e);
            EdgesCount++;
            return LastEdgeIndex;
        }

        /// <summary>
        /// Removes the edge with the specified index
        /// </summary>
        /// <param name="index">
        /// The index of the edge
        /// </param>
        public override void RemoveEdge(int index)
        {
            TE e = Edges[index];
            var list1 = AdjacencyList[Indexes.IndexOf(e.From)];
            int index1 = list1.IndexOf(e);
            list1.RemoveAt(index1);
            EdgesCount--;
            if (index == LastEdgeIndex) LastEdgeIndex--;
            Edges.Remove(index);
        }
    }
}
