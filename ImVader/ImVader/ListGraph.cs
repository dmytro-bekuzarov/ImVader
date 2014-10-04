// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListGraph.cs" company="Sigma">
//   Sigma
// </copyright>
// <summary>
//   Adjacency-list based graph.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImVader
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Adjacency-list based graph.
    /// </summary>
    /// <typeparam name="TV">
    /// Type of data stored in vertices
    /// </typeparam>
    /// <typeparam name="TE">
    /// Type of edge used to connect vertices
    /// </typeparam>
    public class ListGraph<TV, TE> : Graph<TV, TE>
        where TE : Edge
    {
        /// <summary>
        /// Represents adjacency list
        /// </summary>
        protected List<List<TE>> AdjacencyList;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListGraph{TV,TE}"/> class.
        /// </summary>
        /// <param name="vertexCount">
        /// The count of vertices.
        /// </param>
        public ListGraph(int vertexCount)
            : base(vertexCount)
        {
            this.AdjacencyList = new List<List<TE>>(vertexCount);
        }

        /// <summary>
        /// The get adjacency list.
        /// </summary>
        /// <param name="v">
        /// The v.
        /// </param>
        /// <returns>
        /// List containing ids of vertices adjacent to v
        /// </returns>
        public override IEnumerable<int> GetAdjacencyList(int v)
        {
            this.CheckArguments(v);
            return this.AdjacencyList[v].Select(edge => edge.Other(v));
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
            this.AdjacencyList[e.W].Add(e);
        }
    }
}
