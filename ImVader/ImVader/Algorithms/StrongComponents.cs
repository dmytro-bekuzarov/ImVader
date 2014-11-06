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
        private readonly Graph<TV, TE> graph;

        /// <summary>
        /// Reversed graph.
        /// </summary>
        private readonly DirectedMatrixGraph<TV, UnweightedEdge> graphReversed;

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
            this.graph = graph;
            used = new bool[graph.VertexCount];
            order = new List<int>();
            Components = new List<List<int>>();
            graphReversed = new DirectedMatrixGraph<TV, UnweightedEdge>(graph.VertexCount);

            for (int i = 0; i < graph.EdgesCount; i++)
            {
                graphReversed.AddEdge(new UnweightedEdge(graph.GetEdge(i).To, graph.GetEdge(i).From));
                if (typeof(MatrixGraph<TV, TE>) == graph.GetType() || typeof(ListGraph<TV, TE>) == graph.GetType())
                {
                    graphReversed.AddEdge(new UnweightedEdge(graph.GetEdge(i).From, graph.GetEdge(i).To));
                }
            }

            for (var i = 0; i < graph.VertexCount; ++i)
            {
                if (!used[i])
                {
                    DepthFirstSearch1(i);
                }
            }

            used = new bool[graph.VertexCount];

            for (var i = 0; i < graph.VertexCount; ++i)
            {
                var vertex = order[graph.VertexCount - 1 - i];
                if (!used[vertex])
                {
                    Components.Add(new List<int>());
                    DepthFirstSearch2(vertex);
                }
            }
        }

        /// <summary>
        /// First depth-first search which is used in the algorithm.
        /// </summary>
        /// <param name="currentVertex">
        /// The index of the vertex.
        /// </param>
        private void DepthFirstSearch1(int currentVertex)
        {
            used[currentVertex] = true;
            for (var i = 0; i < graph.GetAdjacentVertices(currentVertex).Count(); ++i)
            {
                if (!used[graph.GetAdjacentVertices(currentVertex).ElementAt(i)])
                {
                    DepthFirstSearch1(graph.GetAdjacentVertices(currentVertex).ElementAt(i));
                }
            }

            order.Add(currentVertex);
        }

        /// <summary>
        /// Second depth-first search which is used in the algorithm.
        /// </summary>
        /// <param name="currentVertex">
        /// The index of the vertex.
        /// </param>
        private void DepthFirstSearch2(int currentVertex)
        {
            used[currentVertex] = true;
            Components[Components.Count - 1].Add(currentVertex);
            for (var i = 0; i < graphReversed.GetAdjacentVertices(currentVertex).Count(); ++i)
            {
                if (!used[graphReversed.GetAdjacentVertices(currentVertex).ElementAt(i)])
                {
                    DepthFirstSearch2(graphReversed.GetAdjacentVertices(currentVertex).ElementAt(i));
                }
            }
        }
    }
}
