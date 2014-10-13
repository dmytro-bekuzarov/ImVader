// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BreadthFirstPathes.cs" company="Sigma">
//   It's a totally free software
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
    /// Initialized new instance for doing Breath first search on a graph
    /// </summary>
    /// <typeparam name="TV">
    /// Type of data stored in vertices of the graph
    /// </typeparam>
    /// <typeparam name="TE">
    /// Type of edge used to connect vertices
    /// </typeparam>
    public class BreadthFirstPathes<TV, TE>
        where TE : Edge
    {
        /// <summary>
        /// Represents if the vertex with an appropriate id is marked or not after bfs done
        /// </summary>
        private readonly bool[] marked;

        /// <summary>
        /// edgeTo[i] defines id of vertex that leads to vertice with id i 
        /// </summary>
        private readonly int[] edgeTo;

        /// <summary>
        /// Start vertex for breath-first search
        /// </summary>
        private readonly int s;

        /// <summary>
        /// Initializes a new instance of the <see cref="BreadthFirstPathes{TV,TE}"/> class.
        /// </summary>
        /// <param name="g">
        /// Graph for BFS(Breath-first search)
        /// </param>
        /// <param name="s">
        /// Start vertex for BFS
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Exception is thrown if s is out of boundaries (0, g.VertexCount)
        /// </exception>
        public BreadthFirstPathes(Graph<TV, TE> g, int s)
        {
            if (s < 0 || s > g.VertexCount)
                throw new ArgumentOutOfRangeException("Vertex is out of range!");
            marked = new bool[g.VertexCount];
            edgeTo = new int[g.VertexCount];
            for (var i = 0; i < edgeTo.Length; i++)
                edgeTo[i] = -1;
            this.s = s;
            BFS(g);
        }

        /// <summary>
        /// Defines if there is a path from start vertex to vertex v after bfs done
        /// </summary>
        /// <param name="v">
        /// Vertex for which we want to know if there is a path from start to it 
        /// </param>
        /// <returns>
        /// Bollean value <see cref="bool"/>.
        /// </returns>
        public bool HasPathTo(int v)
        {
            return marked[v];
        }

        /// <summary>
        /// Defines a path between start vertex and v as a sequence of vertices from s to v if path exists else returns null 
        /// </summary>
        /// <param name="v">
        /// Path from s to v
        /// </param>
        /// <returns>
        /// Collections of vertices <see cref="System.Collections.IEnumerable"/>.
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
        /// Private method encapsulating bfs algorithm on a graph
        /// </summary>
        /// <param name="g">
        /// Graph we want to do bfs on
        /// </param>
        private void BFS(Graph<TV, TE> g)
        {
            var vertices = new Queue<int>();
            vertices.Enqueue(s);
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
