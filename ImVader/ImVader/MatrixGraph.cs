// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MatrixGraph.cs" company="Sigma">
//   It's a totally free software
// </copyright>
// <summary>
//   Defines the MatrixGraph type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImVader
{
    using System.Collections.Generic;
    using System.Linq;

    using ImVader.Utils;

    using Newtonsoft.Json;

    /// <summary>
    /// Matrix-based graph.
    /// </summary>
    /// <typeparam name="TV">
    /// Type of data stored in vertices.
    /// </typeparam>
    /// <typeparam name="TE">
    /// Type of edge used to connect vertices.
    /// </typeparam>
    public class MatrixGraph<TV, TE> : Graph<TV, TE>
        where TE : Edge
    {
        /// <summary>
        /// Represents an adjacency matrix of the graph.
        /// </summary>
        [JsonProperty]
        protected SquareMatrix<List<int>> Matrix;

        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixGraph{TV,TE}"/> class.
        /// </summary>
        /// <param name="capacity">
        /// Initial number of vertices.
        /// </param>
        public MatrixGraph(int capacity = 0)
        {
            Matrix = new SquareMatrix<List<int>>(capacity);
            for (int i = 0; i < capacity; i++)
            {
                Indexes.Add(LastVertexIndex + 1);
                Vertices.Add(i, new Vertex<TV>());
            }

            LastEdgeIndex = -1;
        }

        /// <summary>
        /// Represents the indexer for the matrix.
        /// </summary>
        /// <param name="i">
        /// The row index. 
        /// </param>
        /// <param name="j">
        /// The column index.
        /// </param>
        /// <returns>
        /// The indexes of edges that connect the vertex with index i and the vertex with index j.
        /// </returns>
        protected List<int> this[int i, int j]
        {
            get
            {
                return Matrix[Indexes.IndexOf(i), Indexes.IndexOf(j)];
            }
        }

        /// <summary>
        /// Gets a collection of indexes of the vertices that are adjacent for the vertex v.
        /// </summary>
        /// <param name="v">
        /// Index of the vertex.
        /// </param>
        /// <returns>
        /// <see cref="System.Collections.IEnumerable"/> 
        /// Indexes of the vertices that are adjacent for the vertex v.
        /// </returns>
        public override IEnumerable<int> GetAdjacentVertices(int v)
        {
            CheckVerticesIndexes(v);
            int id = Indexes.IndexOf(v);
            for (int j = 0; j < VertexCount; j++)
            {
                if (Matrix[id, j] != null && Matrix[id, j].Count > 0) yield return Indexes[j];
            }
        }

        /// <summary>
        /// Gets a collection of indexes of the edges that are adjacent for the vertex v.
        /// </summary>
        /// <param name="v">
        /// The vertex index.
        /// </param>
        /// <returns>
        /// <see cref="System.Collections.IEnumerable"/> 
        /// </returns>
        public override IEnumerable<TE> GetAdjacentEdges(int v)
        {
            CheckVerticesIndexes(v);
            int id = Indexes.IndexOf(v);
            var edgesIndexes = new List<int>();
            for (var j = 0; j < VertexCount; j++)
            {
                if (Matrix[id, j] != null && Matrix[id, j].Count > 0) edgesIndexes.AddRange(Matrix[id, j]);
            }

            return edgesIndexes.Select(t => this.Edges[t]).ToList();
        }

        /// <summary>
        /// Adds a new vertex to the graph.
        /// </summary>
        /// <param name="value">
        /// The value of the vertex.
        /// </param>
        /// <returns>
        /// Index of the created vertex.
        /// </returns>
        public override int AddVertex(TV value)
        {
            int lastVertexIndex = LastVertexIndex;
            Vertices.Add(++lastVertexIndex, new Vertex<TV>(value));
            Matrix.Add();
            Indexes.Add(lastVertexIndex);
            return lastVertexIndex;
        }

        /// <summary>
        /// Removes the vertex with the specified index.
        /// </summary>
        /// <param name="index">
        /// The index of the vertex.
        /// </param>
        public override void RemoveVertex(int index)
        {
            var edges = this[index, index];
            foreach (var i in edges)
            {
                Edges.Remove(i);
            }

            Matrix.Remove(Indexes.IndexOf(index));
            this.Indexes.Remove(index);
            Vertices.Remove(index);
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
            this[e.To, e.From].Add(LastEdgeIndex);
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
            this[e.To, e.From].Remove(index);
            this[e.From, e.To].Remove(index);
            EdgesCount--;
            if (index == LastEdgeIndex)
            {
                LastEdgeIndex--;
            }

            Edges.Remove(index);
        }
    }
}
