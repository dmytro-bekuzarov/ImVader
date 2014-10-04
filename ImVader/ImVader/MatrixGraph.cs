// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MatrixGraph.cs" company="Sigma">
//   I have no idea what should be written here.
// </copyright>
// <summary>
//   Defines the MatrixGraph type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImVader
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Matrix-based graph
    /// </summary>
    /// <typeparam name="TV">
    /// Type of data stored in vertices
    /// </typeparam>
    /// <typeparam name="TE">
    /// Type of edge used to connect vertices
    /// </typeparam>
    public class MatrixGraph<TV, TE> : Graph<TV, TE>
        where TE : Edge
    {
        /// <summary>
        /// Represents Adjacency matrix
        /// </summary>
        protected List<List<int>> Matrix;

        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixGraph{TV,TE}"/> class.
        /// </summary>
        /// <param name="vertexCount">
        /// Count of vertices
        /// </param>
        public MatrixGraph(int vertexCount)
            : base(vertexCount)
        {
            this.Matrix = new List<List<int>>(vertexCount);
            for (var i = 0; i < vertexCount; i++)
                this.Matrix[i] = new List<int>(vertexCount);
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            this.VertexCount = vertexCount;
        }

        /// <summary>
        /// Returns Adjacency list for appropriate vertex
        /// </summary>
        /// <param name="v">
        /// Vertex for which we return adjacency list
        /// </param>
        /// <returns>
        /// The <see cref="System.Collections.IEnumerable"/>. Collection of adjacent vertices ids 
        /// </returns>
        public override IEnumerable<int> GetAdjacencyList(int v)
        {
            this.CheckArguments(v);
            return this.Matrix[v].Where(vertex => vertex > 0);
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
            this.Matrix[e.W][e.V]++;
            this.Matrix[e.V][e.W]++;
        }
    }
}
