// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Dijkstra.cs" company="Sigma">
//   It'index a totally free software
// </copyright>
// <summary>
//   Defines the Dijkstra type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImVader.Algorithms.ShortestPaths
{
    using System;
    using System.Linq;

    using ImVader.Utils;

    /// <summary>
    /// Represents an implementation of the Dijkstra algorithm.
    /// </summary>
    /// <typeparam name="TV">
    /// Type of data stored in vertices of the graph.
    /// </typeparam>
    /// <typeparam name="TE">
    /// Type of edge of the graph.
    /// </typeparam>
    public class Dijkstra<TV, TE> : ShortestPath<TV, TE>
        where TE : Edge
    {
        /// <summary>
        /// The priority queue of vertices.
        /// </summary>
        private readonly MinPq<Node> queue;  

        /// <summary>
        /// Initializes a new instance of the <see cref="Dijkstra{TV,TE}"/> class. 
        /// Processes graph via Dijkstra algo.
        /// </summary>
        /// <param name="gr">
        /// Directed graph to be precessed.
        /// </param>
        /// <param name="index">
        /// Entry vertex index.
        /// </param>
        public Dijkstra(IDirectedGraph gr, int index)
            : base((Graph<TV, TE>)gr)
        {
            index = this.G.IndexOf(index);
            for (var i = 0; i < this.G.EdgesCount; i++)
            {
                Edge e = this.G.GetEdge(i);
                var edge = e as WeightedEdge;
                if (edge != null)
                {
                    if (edge.Weight < 0)
                    {
                        throw new ArgumentException("edge " + e + " has negative weight");
                    }
                }
            }

            this.EdgeTo = new TE[this.G.VertexCount];
            this.DistTo = new double[this.G.VertexCount];
            for (var v = 0; v < this.G.VertexCount; v++)
            {
                this.DistTo[v] = double.MaxValue;
            }

            this.DistTo[index] = 0.0;
            this.queue = new MinPq<Node>(this.G.VertexCount);
            this.queue.Insert(new Node(index, this.DistTo[index]));
            while (!this.queue.IsEmpty())
            {
                var v = this.queue.DelMin();
                var adj = this.G.GetAdjacentEdges(v.NodeIndex);
                var enumerable = adj as TE[] ?? adj.ToArray();
                for (var i = 0; i < enumerable.Count(); i++)
                {
                    this.Relax(enumerable[i]);
                }
            }
        }

        /// <summary>
        /// Relaxes edge with index e and update queue if changed.
        /// </summary>
        /// <param name="edge">
        /// The edge to relax.
        /// </param>
        private void Relax(TE edge)
        {
            var e = edge as Edge;
            int v = this.G.IndexOf(e.From), w = this.G.IndexOf(e.To);
            var weight = 1.0;
            var weightedEdge = e as WeightedEdge;
            if (weightedEdge != null)
            {
                weight = weightedEdge.Weight;
            }

            if (this.DistTo[w] > this.DistTo[v] + weight)
            {
                this.DistTo[w] = this.DistTo[v] + weight;
                this.EdgeTo[w] = edge;
                this.ChangeOrAdd(w, this.DistTo[w]);
            }
        }

        /// <summary>
        /// Changes or adds the vertex while relaxing.
        /// </summary>
        /// <param name="w">
        /// Vertex to add or update.
        /// </param>
        /// <param name="weight">
        /// New weight of the edege.
        /// </param>
        private void ChangeOrAdd(int w, double weight)
        {
            bool changed = false;
            var all = this.queue.GetAsEnumerable();
            var enumerable = all as Node[] ?? all.ToArray();
            foreach (var t in enumerable.Where(t => t.NodeIndex == w))
            {
                t.Weight = weight;
                changed = true;
            }

            if (!changed)
            {
                this.queue.Insert(new Node(w, weight));
            }
        }
    }
}
