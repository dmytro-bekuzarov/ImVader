namespace ImVader.Algorithms
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Initialized new instance for dividing vertices into strong components. 
    /// </summary>
    /// <typeparam name="TV">Type of the vertices</typeparam>
    /// <typeparam name="TE">Type of the edges</typeparam>
    public class StrongComponents<TV, TE>
        where TE : Edge
    {
        /// <summary>
        /// List of components. List[i] is a list of vertices the i-th component contains.
        /// </summary>
        public List<List<int>> Components;

        /// <summary>
        /// Vertex used/unused
        /// </summary>
        private bool[] used;

        /// <summary>
        /// Initial graph
        /// </summary>
        private Graph<TV, TE> g;

        /// <summary>
        /// Reversed graph
        /// </summary>
        private DirectedMatrixGraph<TV, UnweightedEdge> gr;

        /// <summary>
        /// Back order of vertices timeouts
        /// </summary>
        private List<int> order;

        /// <summary>
        /// Initializes a new instance of the <see cref="StrongComponents{TV,TE}"/> class.
        /// </summary>
        /// <param name="graph">Initial graph to divide for strong components</param>
        public StrongComponents(Graph<TV, TE> graph)
        {
            g = graph;
            used = new bool[g.VertexCount];
            order = new List<int>();
            Components = new List<List<int>>();
            gr = new DirectedMatrixGraph<TV, UnweightedEdge>(g.VertexCount);

            for (int i = 0; i < g.EdgesCount; i++)
            {
                gr.AddEdge(new UnweightedEdge(g.Edges[i].To, g.Edges[i].From));
                if (typeof(MatrixGraph<TV, TE>) == g.GetType() || typeof(ListGraph<TV, TE>) == g.GetType())
                    gr.AddEdge(new UnweightedEdge(g.Edges[i].From, g.Edges[i].To));
            }

            for (var i = 0; i < g.VertexCount; ++i)
                if (!used[i])
                    Dfs1(i);

            used = new bool[g.VertexCount];

            for (var i = 0; i < g.VertexCount; ++i)
            {
                var v = order[g.VertexCount - 1 - i];
                if (!used[v])
                {
                    Components.Add(new List<int>());
                    Dfs2(v);
                }
            }
        }

        void Dfs1(int v)
        {
            used[v] = true;
            for (var i = 0; i < g.GetAdjacentVertices(v).Count(); ++i)
                if (!used[g.GetAdjacentVertices(v).ElementAt(i)])
                    Dfs1(g.GetAdjacentVertices(v).ElementAt(i));
            order.Add(v);
        }

        void Dfs2(int v)
        {
            used[v] = true;
            Components[Components.Count - 1].Add(v);
            for (var i = 0; i < gr.GetAdjacentVertices(v).Count(); ++i)
                if (!used[gr.GetAdjacentVertices(v).ElementAt(i)])
                    Dfs2(gr.GetAdjacentVertices(v).ElementAt(i));
        }
    }
}
