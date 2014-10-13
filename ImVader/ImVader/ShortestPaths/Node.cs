namespace ImVader.ShortestPaths
{
    using System;

    /// <summary>
    /// The ode interface.
    /// </summary>
    internal class Node : IComparable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Node"/> class.
        /// </summary>
        /// <param name="n">
        /// The n.
        /// </param>
        /// <param name="weight">
        /// The weight.
        /// </param>
        public Node(int n, double weight)
        {
            this.N = n;
            this.Weight = weight;
        }

        /// <summary>
        /// Gets or sets the index of the node.
        /// </summary>
        public int N { get; set; }

        /// <summary>
        /// Gets or sets the weight of the path to the vertex.
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// The compare to method,used for Dijkstra minPQ.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int CompareTo(object obj)
        {
            var anotherNode = (Node)obj;
            return this.Weight.CompareTo(anotherNode.Weight);
        }
    }
}
