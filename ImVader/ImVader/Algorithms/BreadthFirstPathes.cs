// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BreadthFirstPathes.cs" company="Sigma">
//   It'startIndex a totally free software
// </copyright>
// <summary>
//   Defines the BreadthFirstPathes type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImVader.Algorithms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents an implementation of the breadth-first search algorithm.
    /// </summary>
    /// <typeparam name="TV">
    /// Type of data stored in vertices of the graph.
    /// </typeparam>
    /// <typeparam name="TE">
    /// Type of edge of the graph.
    /// </typeparam>
    public class BreadthFirstPathes<TV, TE>
        where TE : Edge
    {
        /// <summary>
        /// Defines if the vertex with an appropriate index is marked or not after breadth-first search.
        /// </summary>
        private readonly bool[] marked;

        /// <summary>
        /// Defines index of the vertex that leads to the vertex with index i. 
        /// </summary>
        private readonly int[] edgeTo;

        /// <summary>
        /// Start vertex index for breath-first search.
        /// </summary>
        private readonly int startIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="BreadthFirstPathes{TV,TE}"/> class.
        /// </summary>
        /// <param name="g">
        /// Graph, on which breath-first search is performed.
        /// </param>
        /// <param name="startIndex">
        /// Start vertex index for breath-first search.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Exception is thrown if startIndex is out of boundaries (0, g.VertexCount).
        /// </exception>
        public BreadthFirstPathes(Graph<TV, TE> g, int startIndex)
        {
            if (startIndex < 0 || startIndex > g.VertexCount)
            {
                throw new ArgumentOutOfRangeException("startIndex", "Vertex index is out of range.");
            }

            marked = new bool[g.VertexCount];
            edgeTo = new int[g.VertexCount];
            for (var i = 0; i < edgeTo.Length; i++)
            {
                edgeTo[i] = -1;
            }

            this.startIndex = startIndex;
            this.BreadthFirstSearch(g);
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
        /// Defines a path between start vertex and vertex with index v as a sequence of vertices from startIndex to v. 
        /// </summary>
        /// <param name="v">
        /// Path from startIndex to v.
        /// </param>
        /// <returns>
        /// Collections of vertices <see cref="System.Collections.IEnumerable"/> if path found, null otherwise.
        /// </returns>
        public IEnumerable<int> PathTo(int v)
        {
            if (!HasPathTo(v)) return null;
            var path = new Stack<int>();
            for (var i = v; i != -1; i = edgeTo[i])
            {
                path.Push(i);
            }

            return path;
        }

        /// <summary>
        /// Encapsulates breadth-first search algorithm on the graph g.
        /// </summary>
        /// <param name="g">
        /// Graph we want to perform breadth-first search on.
        /// </param>
        private void BreadthFirstSearch(Graph<TV, TE> g)
        {
            var vertices = new Queue<int>();
            vertices.Enqueue(this.startIndex);
            while (vertices.Any())
            {
                var curVertex = vertices.Dequeue();
                marked[curVertex] = true;
                foreach (var vertex in g.GetAdjacentVertices(curVertex).Where(x => !this.marked[x]))
                {
                    edgeTo[vertex] = curVertex;
                    vertices.Enqueue(vertex);
                }
            }
        }
    }
}
