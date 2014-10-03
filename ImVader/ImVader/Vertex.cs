// <copyright file="Vertex.cs" company="Sigma">
//   Sigma
// </copyright>
// <summary>
//   Vertex class defines graph vertex
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImVader
{
    /// <summary>
    /// Defines graph vertex
    /// </summary>
    /// <typeparam name="T">
    /// Defines type of data stored in the vertex
    /// </typeparam>
    public class Vertex<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vertex{T}"/> class.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        public Vertex(T data)
        {
            this.Data = data;
        }

        /// <summary>
        /// Gets or sets the data in the vertex
        /// </summary>
        public T Data { get; protected set; }
    }
}
