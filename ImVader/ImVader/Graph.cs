// <copyright file="Graph.cs" company="Sigma">
//   It's a totally free software
// </copyright>
// <summary>
//   The base class for all graphs.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImVader
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Newtonsoft.Json;

    /// <summary>
    /// Base class for all graphs
    /// </summary>
    /// <typeparam name="TV">
    /// Type of data stored in the vertex
    /// </typeparam>
    /// <typeparam name="TE">
    /// Type of the edges connecting vertices
    /// </typeparam>
    public abstract class Graph<TV, TE>
        where TE : Edge
    {
        /// <summary>
        /// A collection of vertices in the graph
        /// </summary>
        [JsonProperty]
        protected Dictionary<int, Vertex<TV>> Vertices;

        /// <summary>
        /// Contains all edges in the graph
        /// </summary>
        [JsonProperty]
        protected Dictionary<int, TE> Edges;

        /// <summary>
        /// Contains indexes of the vertices stored in the matrix
        /// </summary>
        [JsonProperty]
        protected List<int> Indexes = new List<int>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Graph{TV,TE}"/> class. 
        /// </summary>
        protected Graph()
        {
            Vertices = new Dictionary<int, Vertex<TV>>();
            Edges = new Dictionary<int, TE>();
        }

        /// <summary>
        /// Gets number of vertices in the graph
        /// </summary>
        [JsonIgnore]
        public int VertexCount
        {
            get { return Vertices.Count; }
        }

        /// <summary>
        /// Gets or sets number of edges in the graph
        /// </summary>
        [JsonProperty]
        public int EdgesCount { get; protected set; }

        /// <summary>
        /// Gets or sets the last edge index.
        /// </summary>
        [JsonProperty]
        protected int LastEdgeIndex { get; set; }

        /// <summary>
        /// Gets the last vertex index or -1 if there is no vertices in the graph.
        /// </summary>
        protected int LastVertexIndex
        {
            get
            {
                return Indexes.Count > 0 ? Indexes[Indexes.Count - 1] : -1;
            }
        }

        /// <summary>
        /// Loads graph object from specified StreamReader.
        /// </summary>
        /// <param name="inputStreamReader">
        /// The input StreamReader.
        /// </param>
        /// <returns>
        /// Deserialized graph object.
        /// </returns>
        public static Graph<TV, TE> LoadFromJsonFile(StreamReader inputStreamReader)
        {
            Graph<TV, TE> result;
            var serializer = new JsonSerializer();
            using (JsonReader reader = new JsonTextReader(inputStreamReader))
            {
                serializer.TypeNameHandling = TypeNameHandling.All;
                result = serializer.Deserialize<Graph<TV, TE>>(reader);
            }

            return result;
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
        public abstract IEnumerable<int> GetAdjacentVertices(int v);

        /// <summary>
        /// Adds a new vertex to the graph.
        /// </summary>
        /// <param name="value">
        /// The value of the vertex
        /// </param>
        /// <returns>
        /// Index of the created vertex
        /// </returns>
        public abstract int AddVertex(TV value);

        /// <summary>
        /// Gets vertex data
        /// </summary>
        /// <param name="index">
        /// Index of the vertex
        /// </param>
        /// <returns>
        /// Data stored in the vertex
        /// </returns>
        public virtual TV GetVertexData(int index)
        {
            this.CheckVerticesIndexes(index);
            return Vertices[index].Data;
        }

        /// <summary>
        /// Sets data to the the vertex
        /// </summary>
        /// <param name="index">
        /// Index of vertex where we want to set data
        /// </param>
        /// <param name="data">
        /// Data to store in the vertex
        /// </param>
        public virtual void SetVertexData(int index, TV data)
        {
            this.CheckVerticesIndexes(index);
            Vertices[index].Data = data;
        }

        /// <summary>
        /// Removes the vertex with the specified index
        /// </summary>
        /// <param name="index">
        /// The index of the vertex
        /// </param>
        public abstract void RemoveVertex(int index);

        /// <summary>
        /// Adds a new edge to the graph
        /// </summary>
        /// <param name="e">
        /// The edge to add
        /// </param>
        /// <returns>
        /// The index of the created edge
        /// </returns>
        public abstract int AddEdge(TE e);

        /// <summary>
        /// Gets the edge with specified index.
        /// </summary>
        /// <param name="edgeIndex">
        /// The index of the edge.
        /// </param>
        /// <returns>
        /// The edge found
        /// </returns>
        public virtual TE GetEdge(int edgeIndex)
        {
            return Edges[edgeIndex];
        }

        /// <summary>
        /// Removes the edge with the specified index
        /// </summary>
        /// <param name="index">
        /// The index of the edge
        /// </param>
        public abstract void RemoveEdge(int index);

        /// <summary>
        /// Saves graph Json to specified StreamWriter.
        /// </summary>
        /// <param name="outputStreamWriter">
        /// Output StreamWriter to serialize graph object.
        /// </param>
        public virtual void SaveToStream(StreamWriter outputStreamWriter)
        {
            var serializer = new JsonSerializer();
            using (JsonWriter writer = new JsonTextWriter(outputStreamWriter))
            {
                serializer.TypeNameHandling = TypeNameHandling.All;
                serializer.Serialize(writer, this);
            }
        }

        /// <summary>
        /// Checks if indexes are greater or equlas zero and less than a number of vertices in the graph)
        /// </summary>
        /// <param name="indexes">
        /// The indexes to check.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Throws an exception if at least one of indexes is out of boundaries (0 and number of vertices in the graph)
        /// </exception>
        protected void CheckVerticesIndexes(params int[] indexes)
        {
            if (indexes.Any(t => t < 0 || t >= this.VertexCount))
            {
                throw new ArgumentException("Index must be greater or equlas zero and less than a number of vertices in the graph");
            }
        }
    }
}
