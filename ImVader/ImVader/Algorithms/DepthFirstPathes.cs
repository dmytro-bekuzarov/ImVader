namespace ImVader.Algorithms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Initialized new instance for doing Breath first search on a graph
    /// </summary>
    /// <typeparam name="TV"></typeparam>
    /// <typeparam name="TE"></typeparam>
    public class DepthFirstPathes<TV,TE> 
        where TE :Edge
    {
        /// <summary>
        /// Times of the entrance to the vertices
        /// </summary>
        public readonly int[] Timein;

        /// <summary>
        /// Times of the exit from the vertices
        /// </summary>
        public readonly int[] Timeout;

        /// <summary>
        /// Represents if the vertex with an appropriate id is marked or not after dfs done
        /// </summary>
        private readonly bool[] marked;

        /// <summary>
        /// edgeTo[i] defines id of vertex that leads to vertice with id i 
        /// </summary>
        private readonly int[] edgeTo;
        
        /// <summary>
        /// Start vertex for depth-first search
        /// </summary>
        private readonly int s;

        /// <summary>
        /// Initializes a new instance of the <see cref="DepthFirstPathes{TV,TE}"/> class.
        /// </summary>
        /// <param name="g">
        /// Graph for DFS(Depth-first search)
        /// </param>
        /// <param name="s">
        /// Start vertex for BFS
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Exception is thrown if s is out of boundaries (0, g.VertexCount)
        /// </exception>
        public DepthFirstPathes(Graph<TV, TE> g, int s)
        {
            if (s < 0 || s > g.VertexCount)
                throw new ArgumentOutOfRangeException("Vertex is out of range!");
            marked = new bool[g.VertexCount];
            edgeTo = new int[g.VertexCount];
            Timein = new int[g.VertexCount];
            Timeout = new int[g.VertexCount];
            for (var i = 0; i < edgeTo.Length; i++)
                Timein[i] = Timeout[i] = edgeTo[i] = -1;
            
            this.s = s;
            DFS(g);
        }

        /// <summary>
        /// The has path to.
        /// </summary>
        /// <param name="v">
        /// The v.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool HasPathTo(int v)
        {
            return marked[v];
        }

        /// <summary>
        /// The path to.
        /// </summary>
        /// <param name="v">
        /// The v.
        /// </param>
        /// <returns>
        /// The <see cref="System.Collections.IEnumerable"/>.
        /// </returns>
        public IEnumerable<int> PathTo(int v)
        {
            if (!HasPathTo(v)) return null;
            var path = new Stack<int>();
            for (var i = v; i != -1; i = edgeTo[i])
                path.Push(i);

            return path;
        }

        private void DFS(Graph<TV, TE> g)
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
                    break;
                }
            }
        }
    }
}
