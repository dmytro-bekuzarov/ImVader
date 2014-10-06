// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Edge.cs" company="Sigma">
//  It's a totally free software
// </copyright>
// <summary>
//   Represents edge in undirected graph
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImVader
{
    using System;

    /// <summary>
    /// Represents edge in an undirected graph
    /// </summary>
    public abstract class Edge
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Edge"/> class according to the order. 
        /// Order does not matter as it's not a directed edge
        /// </summary>
        /// <param name="v">
        /// First vertex id of the edge 
        /// </param>
        /// <param name="w">
        /// Second vertex of the edge
        /// </param>
        protected Edge(int v, int w)
        {
            this.V = v;
            this.W = w;
        }

        /// <summary>
        /// Gets or sets first vertex id.
        /// </summary>
        public int V { get; protected set; }

        /// <summary>
        /// Gets or sets second vertex id.
        /// </summary>
        public int W { get; protected set; }

        /// <summary>
        /// Returns the first vertex of the edge
        /// </summary>
        /// <returns>
        /// The first vertex of the edge
        ///  <see cref="int"/>.
        /// </returns>
        public int Either()
        {
            return this.V;
        }

        /// <summary>
        /// Returns the other vertex id of the edge
        /// </summary>
        /// <param name="vertex">
        /// One of the vertices of the edge
        /// </param>
        /// <returns>
        /// Other vertex id in the edge
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Exception is thrown if vertex was not found
        /// </exception>
        public int Other(int vertex)
        {
            if (vertex == this.V) { return W; }
            if (vertex == this.W) { return V; }
            throw new ArgumentException();
        }
    }
}
