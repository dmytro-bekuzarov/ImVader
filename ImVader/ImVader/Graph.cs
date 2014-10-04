// <copyright file="Graph.cs" company="Sigma">
//   I have no idea what should be written here.
// </copyright>
// <summary>
//   The Graph interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImVader
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Base class for all graphs
    /// </summary>
    /// <typeparam name="TV">
    /// Type of information stored in the vertex
    /// </typeparam>
    /// <typeparam name="TE">
    /// Type of edges used to ceonnect vertices
    /// </typeparam>
    public abstract class Graph<TV, TE>
        where TE : Edge
    {
        /// <summary>
        /// Represents vertices in graph
        /// </summary>
        protected Vertex<TV>[] Vertices;

        /// <summary>
        /// Initializes a new instance of the <see cref="Graph{TV,TE}"/> class.
        /// </summary>
        /// <param name="vertexCount">
        /// Count of vertices
        /// </param>
        protected Graph(int vertexCount)
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            this.VertexCount = vertexCount;
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            this.Vertices = new Vertex<TV>[this.VertexCount];
        }

        /// <summary>
        /// Gets count of vertices in the graph
        /// </summary>
        public virtual int VertexCount { get; protected set; }

        /// <summary>
        /// Gets count of edges in the graph
        /// </summary>
        public virtual int EdgesCount { get; protected set; }

        /// <summary>
        /// Gets adjacency list for vertex id v
        /// </summary>
        /// <param name="v">
        /// Id of vertex for which we want to get adjacency list
        /// </param>
        /// <returns>
        /// <see cref="System.Collections.IEnumerable"/> Ids of vertices adjacent to v
        /// </returns>
        public abstract IEnumerable<int> GetAdjacencyList(int v);

        /// <summary>
        /// Sets data to the appropriate vertex
        /// </summary>
        /// <param name="id">
        /// Id of vertex where we want to set data
        /// </param>
        /// <param name="data">
        /// Data we want to store in the vertex
        /// </param>
        /// <returns>
        /// Returns vertex with new data.
        /// </returns>
        public virtual Vertex<TV> SetVertexData(int id, TV data)
        {
            this.CheckArguments(id);
            this.Vertices[id] = new Vertex<TV>(data);
            return this.Vertices[id];
        }

        /// <summary>
        /// Gets vertex data
        /// </summary>
        /// <param name="id">
        /// Id of vertex which data we want to get
        /// </param>
        /// <returns>
        /// Data stored in vertex with id
        /// </returns>
        public virtual TV GetVertexData(int id)
        {
            this.CheckArguments(id);
            return this.Vertices[id].Data;
        }

        /// <summary>
        /// Adds new edge to the graph
        /// </summary>
        /// <param name="e">
        /// Contains info about edge we want to add(two vertices and cost if needed)
        /// </param>
        public abstract void AddEdge(TE e);

        /// <summary>
        /// Validates parameter
        /// </summary>
        /// <param name="id">
        /// Id of vertex we want to validate
        /// </param>
        /// <exception cref="ArgumentException">
        /// Throws an exception if id is out of boundaries(0 and count of vertices in graph)
        /// </exception>
        protected void CheckArguments(int id)
        {
            if (id < 0 || id >= VertexCount)
                throw new ArgumentException("Id must be between 0 and verticesCount");
        }
    }
}
