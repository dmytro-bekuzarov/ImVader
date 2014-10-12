// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListGraph.cs" company="Sigma">
//   It's a totally free software
// </copyright>
// <summary>
//   Adjacency-list based graph.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImVader
{
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    /// <summary>
    /// List-based graph.
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
        [JsonProperty]
        protected List<List<TE>> AdjacencyList;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListGraph{TV,TE}"/> class.
        /// </summary>
        /// <param name="capacity">
        /// Initial number of vertices.
        /// </param>
        public ListGraph(int capacity = 0)
        {
            AdjacencyList = new List<List<TE>>();
            for (int i = 0; i < capacity; i++)
            {
                AdjacencyList.Add(new List<TE>());
                Indexes.Add(LastVertexIndex + 1);
                Vertices.Add(i, new Vertex<TV>());
            }

            LastEdgeIndex = -1;
        }

        /// <summary>
        /// Gets a collection of indexes of the vertices that are adjacent for the vertex v
        /// </summary>
        /// <param name="v">
        /// Index of the vertex
        /// </param>
        /// <returns>
        /// <see cref="System.Collections.IEnumerable"/> 
        /// Indexes of the vertices that are adjacent for the vertex v
        /// </returns>
        public override IEnumerable<int> GetAdjacentVertices(int v)
        {
            CheckVerticesIndexes(v);
            return this.AdjacencyList[v].Select(edge => edge.Other(v));
        }

        /// <summary>
        /// Gets a collection of indexes of the edges that are adjacent for the vertex v
        /// </summary>
        /// <param name="v">
        /// The v.
        /// </param>
        /// <returns>
        /// <see cref="System.Collections.IEnumerable"/> 
        /// </returns>
        public override IEnumerable<TE> GetAdjacentEdges(int v)
        {
            CheckVerticesIndexes(v);
            return this.AdjacencyList[v];
        }

        /// <summary>
        /// Adds a new vertex to the graph.
        /// </summary>
        /// <param name="value">
        /// The value of the vertex
        /// </param>
        /// <returns>
        /// Index of the created vertex
        /// </returns>
        public override int AddVertex(TV value)
        {
            int lastVertexIndex = LastVertexIndex;
            Vertices.Add(++lastVertexIndex, new Vertex<TV>(value));
            AdjacencyList.Add(new List<TE>());
            Indexes.Add(lastVertexIndex);
            return lastVertexIndex;
        }

        /// <summary>
        /// Removes the vertex with the specified index
        /// </summary>
        /// <param name="index">
        /// The index of the vertex
        /// </param>
        public override void RemoveVertex(int index)
        {
            AdjacencyList.RemoveAt(Indexes.IndexOf(index));
            Indexes.Remove(index);
            Vertices.Remove(index);
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
            CheckVerticesIndexes(e.V, e.W);
            Edges.Add(++LastEdgeIndex, e);
            AdjacencyList[Indexes.IndexOf(e.V)].Add(e);
            AdjacencyList[Indexes.IndexOf(e.W)].Add(e);
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
            var e = Edges[index];
            var list1 = AdjacencyList[Indexes.IndexOf(e.V)];
            var list2 = AdjacencyList[Indexes.IndexOf(e.W)];
            var index1 = list1.IndexOf(e);
            var index2 = list2.IndexOf(e);
            list1.RemoveAt(index1);
            list2.RemoveAt(index2);
            EdgesCount--;
            if (index == LastEdgeIndex) LastEdgeIndex--;
            Edges.Remove(index);
        }
    }
}
