// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DepthFirstPathes.cs" company="Sigma">
//   It's a totally free software
// </copyright>
// <summary>
//   Defines the DepthFirstPathes type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImVader.Algorithms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents an implementation of the depth-first search algorithm.
    /// </summary>
    /// <typeparam name="TV">
    /// Type of data stored in vertices of the graph.
    /// </typeparam>
    /// <typeparam name="TE">
    /// Type of edge of the graph.
    /// </typeparam>
    public class DepthFirstPathes<TV, TE>
        where TE : Edge
    {
        /// <summary>
        /// Times of the entrance to the vertices.
        /// </summary>
        public readonly int[] Timein;

        /// <summary>
        /// Times of the exit from the vertices.
        /// </summary>
        public readonly int[] Timeout;

        /// <summary>
        /// Defines if the vertex with an appropriate index is marked or not after depth-first search.
        /// </summary>
        private readonly bool[] marked;

        /// <summary>
        /// Defines index of the vertex that leads to the vertex with index i. 
        /// </summary>
        private readonly int[] edgeTo;

        /// <summary>
        /// Start vertex index for depth-first search.
        /// </summary>
        private readonly int s;

        /// <summary>
        /// Initializes a new instance of the <see cref="DepthFirstPathes{TV,TE}"/> class.
        /// </summary>
        /// <param name="g">
        /// Graph, on which depth-first search is performed.
        /// </param>
        /// <param name="s">
        /// Start vertex for breath-first search.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Exception is thrown if s is out of boundaries (0, g.VertexCount).
        /// </exception>
        public DepthFirstPathes(Graph<TV, TE> g, int s)
        {
            if (s < 0 || s > g.VertexCount)
                throw new ArgumentOutOfRangeException("s", "Vertex index is out of range.");
            marked = new bool[g.VertexCount];
            edgeTo = new int[g.VertexCount];
            Timein = new int[g.VertexCount];
            Timeout = new int[g.VertexCount];
            for (var i = 0; i < edgeTo.Length; i++)
            {
                Timein[i] = Timeout[i] = edgeTo[i] = -1;
            }

            this.s = s;
            this.DepthFirstSearch(g);
        }

        /// <summary>
        /// Defines if there is a path from start vertex to vertex v after breath-first search.
        /// </summary>
        /// <param name="v">
        /// Vertex index for which we want to know if there is a path from start to it. 
        /// </param>
        /// <returns>
        /// True, if path exists, false otherwise.
        /// </returns>
        public bool HasPathTo(int v)
        {
            return marked[v];
        }

        /// <summary>
        /// Defines a path between start vertex and vertex with index v as a sequence of vertices from s to v. 
        /// </summary>
        /// <param name="v">
        /// Path from s to v.
        /// </param>
        /// <returns>
        /// Collections of vertices <see cref="System.Collections.IEnumerable"/> if path found, null otherwise.
        /// </returns>
        public IEnumerable<int> PathTo(int v)
        {
            if (!HasPathTo(v)) return null;
            var path = new Stack<int>();
            for (var i = v; i != -1; i = edgeTo[i])
                path.Push(i);

            return path;
        }

        /// <summary>
        /// Encapsulates depth-first search algorithm on the graph g.
        /// </summary>
        /// <param name="g">
        /// Graph we want to perform depth-first search on.
        /// </param>
        private void DepthFirstSearch(Graph<TV, TE> g)
        {
            int dfsTimer = 0;
            var vertices = new Stack<int>();
            vertices.Push(s);
            while (vertices.Any())
            {
                ++dfsTimer;
                var curVertex = vertices.Peek();
                if (Timein[curVertex] == -1)
                {
                    Timein[curVertex] = dfsTimer;
                }
                else
                {
                    Timeout[curVertex] = dfsTimer;
                    vertices.Pop();
                    continue;
                }

                marked[curVertex] = true;
                foreach (var vertex in g.GetAdjacentVertices(curVertex).Where(x => !this.marked[x]))
                {
                    edgeTo[vertex] = curVertex;
                    vertices.Push(vertex);
                }
            }
        }
    }
}
