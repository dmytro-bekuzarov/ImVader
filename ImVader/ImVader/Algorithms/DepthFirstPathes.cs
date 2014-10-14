namespace ImVader.Algorithms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    class DepthFirstPathes<TV,TE> 
        where TE :Edge
    {
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
        /// Times of the entrance to the vertices
        /// </summary>
        private List<int> timein;

        /// <summary>
        /// Times of the exit from the vertices
        /// </summary>
        private List<int> timeout; 

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
            for (var i = 0; i < edgeTo.Length; i++)
                timein[i] = timeout[i] = edgeTo[i] = -1;
            
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

        private void DFS(Graph<TV, TE> g)
        {
            int dfsTimer = 0;
            var vertices = new Stack<int>();
            vertices.Push(s);
            while (vertices.Any())
            {
                ++dfsTimer;
                var curVertex = vertices.Peek();
                if (timein[curVertex] == -1)
                {
                    timein[curVertex] = dfsTimer;
                }
                else
                {
                    timeout[curVertex] = dfsTimer;
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
