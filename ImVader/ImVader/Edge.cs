// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Edge.cs" company="Sigma">
//   Copyright
// </copyright>
// <summary>
//   Defines the Edge type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImVader
{
    using System;

    public class Edge
    {
        private readonly IVertex v;
        private readonly IVertex w;

        public Edge(IVertex v, IVertex w)
        {
            this.v = v;
            this.w = w;
        }

        public IVertex Either()
        {
            return v;
        }

        /// <summary>
        /// The other.
        /// </summary>
        /// <param name="vertex">
        /// The vertex.
        /// </param>
        /// <returns>
        /// The <see cref="IVertex"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public IVertex Other(IVertex vertex)
        {
            if (vertex == v) return w;
            if (vertex == w) return v;
            throw new ArgumentException();
        }
    }
}
