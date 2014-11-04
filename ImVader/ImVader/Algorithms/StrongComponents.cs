// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StrongComponents.cs" company="Sigma">
//   It's a totally free software
// </copyright>
// <summary>
//   Defines the StrongComponents type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImVader.Algorithms
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents an implementation of the algorithm for finding strong components in the graph.
    /// </summary>
    /// <typeparam name="TV">
    /// Type of data stored in vertices of the graph.
    /// </typeparam>
    /// <typeparam name="TE">
    /// Type of edge of the graph.
    /// </typeparam>
    public class StrongComponents<TV, TE>
        where TE : Edge
    {
        /// <summary>
        /// List of components. List[i] is a list of vertices the i-th component contains.
        /// </summary>
        public List<List<int>> Components;

        /// <summary>
        /// Defines whether the vertex used/unused.
        /// </summary>
        private readonly bool[] used;

        /// <summary>
        /// Initial graph.
        /// </summary>
        private readonly Graph<TV, TE> g;

        /// <summary>
        /// Reversed graph.
        /// </summary>
        private readonly DirectedMatrixGraph<TV, UnweightedEdge> gr;

        /// <summary>
        /// Back order of the vertices timeouts.
        /// </summary>
        private readonly List<int> order;

        /// <summary>
        /// Initializes a new instance of the <see cref="StrongComponents{TV,TE}"/> class.
        /// </summary>
        /// <param name="graph">
        /// Initial graph strong components must be found for.
        /// </param>
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
                    this.DepthFirstSearch1(i);

            used = new bool[g.VertexCount];

            for (var i = 0; i < g.VertexCount; ++i)
            {
                var v = order[g.VertexCount - 1 - i];
                if (!used[v])
                {
                    Components.Add(new List<int>());
                    this.DepthFirstSearch2(v);
                }
            }
        }

        /// <summary>
        /// First depth-first search which is used in the algorithm.
        /// </summary>
        /// <param name="v">
        /// The index of the vertex.
        /// </param>
        private void DepthFirstSearch1(int v)
        {
            used[v] = true;
            for (var i = 0; i < g.GetAdjacentVertices(v).Count(); ++i)
                if (!used[g.GetAdjacentVertices(v).ElementAt(i)])
                    this.DepthFirstSearch1(g.GetAdjacentVertices(v).ElementAt(i));
            order.Add(v);
        }

        /// <summary>
        /// Second depth-first search which is used in the algorithm.
        /// </summary>
        /// <param name="v">
        /// The index of the vertex.
        /// </param>
        private void DepthFirstSearch2(int v)
        {
            used[v] = true;
            Components[Components.Count - 1].Add(v);
            for (var i = 0; i < gr.GetAdjacentVertices(v).Count(); ++i)
                if (!used[gr.GetAdjacentVertices(v).ElementAt(i)])
                    this.DepthFirstSearch2(gr.GetAdjacentVertices(v).ElementAt(i));
        }
    }
}
