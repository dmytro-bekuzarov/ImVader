namespace ImVader
{
    using System;

    /// <summary>
    /// The vertex.
    /// </summary>
    /// <typeparam name="T">Type of the value
    /// </typeparam>
    public class Vertex<T> : IVertex where T : IComparable
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public virtual T Value { get; set; }

        public int Id { get; protected set; }
    }
}
