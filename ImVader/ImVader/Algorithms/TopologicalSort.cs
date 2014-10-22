// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TopologicalSort.cs" company="NURE">
//   TopologicalSort
// </copyright>
// <summary>
//   Defines the TopologicalSort type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImVader.Algorithms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;


    /// <summary>
    /// The color, vertix can have.
    /// </summary>
    internal enum Color
    {
        /// <summary>
        /// The white.
        /// </summary>
        White = 0,

        /// <summary>
        /// The grey.
        /// </summary>
        Grey = 1,

        /// <summary>
        /// The black.
        /// </summary>
        Black = 2
    }

    /// <summary>
    /// The topological sort.
    /// </summary>
    /// <typeparam name="TV">
    /// Vertex type
    /// </typeparam>
    /// <typeparam name="TE">
    /// Edge type
    /// </typeparam>
    public class TopologicalSort<TV, TE>
          where TE : Edge
    {
        /// <summary>
        /// The graph we operate on.
        /// </summary>
        private Graph<TV, TE> graph;

        /// <summary>
        /// The colors of graph`s vertices.
        /// </summary>
        private Color[] colors;

        /// <summary>
        /// This array maps old Vertices indexes to new
        /// </summary>
        private int[] numbers;

        /// <summary>
        /// Stack to recover new vertices order.
        /// </summary>
        private Stack<int> visitedVertices;

        /// <summary>
        /// Perfoms topological sort on graph and returns modified graph
        /// </summary>
        /// <param name="g">Graph to operate on</param>
        /// <returns>Returns modofied suurce graph</returns>
        public Graph<TV, TE> SortGraph(Graph<TV, TE> g)
        {
            this.graph = g;
            colors = new Color[g.VertexCount];
            numbers = new int[g.VertexCount];
            visitedVertices = new Stack<int>(g.VertexCount);
            for (var i = 0; i < g.VertexCount; i++)
            {
                var cycle = this.Dfs(i);
                if (cycle)
                    throw new InvalidOperationException("Graph has cycles");
            }

            for (var i = 0; i < g.VertexCount; i++)
            {
                numbers[visitedVertices.Pop()] = i;
            }

            foreach (var edge in g.Edges.Values)
            {
                edge.From = numbers[edge.From];
                edge.To = numbers[edge.To];
            }

            var tempVertices = new Dictionary<int, Vertex<TV>>(g.VertexCount);

            for (var i = 0; i < numbers.Length; i++)
            {
                tempVertices[i] = g.Vertices[numbers[i]];
            }

            return g;
        }

        /// <summary>
        /// The dfs. Fills vertices stack to complete topological sort.
        /// </summary>
        /// <param name="v">
        /// The vertix index.
        /// </param>
        /// <returns>
        /// Returns treu if graph has cycles.
        /// </returns>
        private bool Dfs(int v)
        {
            if (colors[v] == Color.Grey) return true;
            if (colors[v] == Color.Black) return false;
            colors[v] = Color.Black;
            var edgesToEnumerate = graph.GetAdjacentEdges(v).ToArray();
            if (edgesToEnumerate.Any(t => this.Dfs(t.To)))
            {
                return true;
            }

            visitedVertices.Push(v);
            colors[v] = Color.Black;
            return false;
        }
    }   
}
