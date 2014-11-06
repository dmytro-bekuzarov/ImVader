// <copyright file="Vertex.cs" company="Sigma">
//   It's a totally free software
// </copyright>
// <summary>
//   Vertex class defines graph vertex
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImVader
{
    /// <summary>
    /// Defines graph vertex.
    /// </summary>
    /// <typeparam name="T">
    /// Defines type of data stored in the vertex.
    /// </typeparam>
    public class Vertex<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vertex{T}"/> class.
        /// </summary>
        public Vertex()
        {
            Data = default(T);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vertex{T}"/> class.
        /// </summary>
        /// <param name="data">
        /// Type of the data stored in the vertex.
        /// </param>
        public Vertex(T data)
        {
            Data = data;
        }

        /// <summary>
        /// Gets or sets the data in the vertex.
        /// </summary>
        /// <value>
        /// The data stored in the vertex.
        /// </value>
        public T Data { get; set; }
    }
}
