// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShortestPath.cs" company="Sigma">
//   It's a totally free software
// </copyright>
// <summary>
//   Defines the ShortestPaths type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImVader.Algorithms.ShortestPaths
{
    using System.Collections.Generic;

    /// <summary>
    /// The abstract class for algorithm for finding the shortes path.
    /// </summary>
    /// <typeparam name="TV">
    /// Type of data stored in vertices of the graph.
    /// </typeparam>
    /// <typeparam name="TE">
    /// Type of edge of the graph.
    /// </typeparam>
    public class ShortestPath<TV, TE> where TE : Edge
    {
        /// <summary>
        /// The graph used for finding the shortest path.
        /// </summary>
        protected Graph<TV, TE> G;

        /// <summary>
        /// An array, where distTo[v] = distance  of shortest s->v path.
        /// </summary>
        protected double[] DistTo;

        /// <summary>
        /// An array, edgeTo[v] = last edge on shortest s->v path.
        /// </summary>
        protected TE[] EdgeTo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShortestPath{TV,TE}"/> class.
        /// </summary>
        /// <param name="g">
        /// The graph used for finding the shortest path.
        /// </param>
        protected ShortestPath(Graph<TV, TE> g)
        {
            this.G = g;
        }

        /// <summary>
        /// Gets distance (weight) to ther vertex with index v.
        /// </summary>
        /// <param name="v">
        /// The destination vertex.
        /// </param>
        /// <returns>
        /// Returns the length of a shortest path from the source vertex s to vertex v, inifinity if no such path.
        /// </returns>
        public double GetDistTo(int v)
        {
            v = this.G.IndexOf(v);
            return this.DistTo[v];
        }

        /// <summary>
        /// Defines is there a path from the source vertex s to vertex v.
        /// </summary>
        /// <param name="v">
        /// The vertex with index v.
        /// </param>
        /// <returns>
        /// True, if there is a path from the source vertex s to vertex v, false otherwise.
        /// </returns>
        public bool HasPathTo(int v)
        {
            v = this.G.IndexOf(v);
            return this.DistTo[v] < double.MaxValue;
        }

        /// <summary>
        /// Returns a shortest path from the source vertex s to vertex v.
        /// </summary>
        /// <param name="v">
        /// The destination vertex index.
        /// </param>
        /// <returns>
        /// The shortest path from the source vertex s to vertex v as an Enumerable of edges, null if no such path.
        /// </returns>
        public IEnumerable<TE> PathTo(int v)
        {
            v = this.G.IndexOf(v);
            if (!this.HasPathToVertex(v)) return null;
            var path = new Stack<TE>();
            for (var e = this.EdgeTo[v]; e != null; e = this.EdgeTo[this.G.IndexOf(e.From)])
            {
                path.Push(e);
            }

            return path;
        }

        /// <summary>
        /// Gets the path to the vertex with index v as vertices indexes.
        /// </summary>
        /// <param name="v">
        /// The vetex index.
        /// </param>
        /// <returns>
        /// The path to the vertex with index v as vertices indexes.
        /// </returns>
        public IEnumerable<int> PathToAsIds(int v)
        {
            v = this.G.IndexOf(v);
            if (!this.HasPathToVertex(v)) return null;
            var path = new Stack<int>();
            for (var e = this.EdgeTo[v]; e != null; e = this.EdgeTo[this.G.IndexOf(e.From)])
            {
                if (!path.Contains(e.From))
                {
                    path.Push(e.From);
                }

                if (!path.Contains(e.To))
                {
                    path.Push(e.To);
                }
            }

            return path;
        }

        /// <summary>
        /// Defines is there a path from the source vertex s to vertex v.
        /// </summary>
        /// <param name="v">
        /// The vertex with index v.
        /// </param>
        /// <returns>
        /// True, if there is a path from the source vertex s to vertex v, false otherwise.
        /// </returns>
        protected bool HasPathToVertex(int v)
        {
            return this.DistTo[v] < double.MaxValue;
        }
    }
}
