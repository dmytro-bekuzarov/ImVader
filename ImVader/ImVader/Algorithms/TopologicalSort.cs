// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TopologicalSort.cs" company="Sigma">
//   It's a totally free software
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
    /// Enum of the colors vertices can have.
    /// </summary>
    internal enum Color
    {
        /// <summary>
        /// Defines the integer value of the white color.
        /// </summary>
        White = 0,

        /// <summary>
        /// Defines the integer value of the grey color.
        /// </summary>
        Grey = 1,

        /// <summary>
        /// Defines the integer value of the black color.
        /// </summary>
        Black = 2
    }

    /// <summary>
    /// Represents an implementation of the toplogical sort algorithm.
    /// </summary>
    /// <typeparam name="TV">
    /// Type of data stored in vertices of the graph.
    /// </typeparam>
    /// <typeparam name="TE">
    /// Type of edge of the graph.
    /// </typeparam>
    public class TopologicalSort<TV, TE>
          where TE : Edge
    {
        /// <summary>
        /// The graph we operate on.
        /// </summary>
        private Graph<TV, TE> graph;

        /// <summary>
        /// The colors of graph vertices.
        /// </summary>
        private Color[] colors;

        /// <summary>
        /// This array maps old vertices indexes to new.
        /// </summary>
        private int[] numbers;

        /// <summary>
        /// Stack to recover new vertices order.
        /// </summary>
        private Stack<int> visitedVertices;

        /// <summary>
        /// Perfoms topological sort on graph. 
        /// </summary>
        /// <param name="graphObject">
        /// Graph to operate on.
        /// </param>
        /// <returns>
        /// Returns modified source graph.
        /// </returns>
        public Graph<TV, TE> SortGraph(Graph<TV, TE> graphObject)
        {
            this.graph = graphObject;
            colors = new Color[graphObject.VertexCount];
            numbers = new int[graphObject.VertexCount];
            visitedVertices = new Stack<int>(graphObject.VertexCount);
            for (var i = 0; i < graphObject.VertexCount; i++)
            {
                var cycle = this.DepthFirstSearch(i);
                if (cycle)
                {
                    throw new InvalidOperationException("Graph has cycles");
                }
            }

            for (var i = 0; i < graphObject.VertexCount; i++)
            {
                numbers[visitedVertices.Pop()] = i;
            }

            return graphObject;
        }

        /// <summary>
        /// Gets the order of sorted graph vertices.
        /// </summary>
        /// <returns>
        /// Order of sorted graph vertices.
        /// </returns>
        public int[] GetOrder()
        {
            return numbers;
        }

        /// <summary>
        /// Fills vertices stack to complete topological sort.
        /// </summary>
        /// <param name="vertexIndex">
        /// The vertex index.
        /// </param>
        /// <returns>
        /// Returns true if graph has cycles, false otherwise.
        /// </returns>
        private bool DepthFirstSearch(int vertexIndex)
        {
            if (colors[vertexIndex] == Color.Grey)
            {
                return true;
            }

            if (colors[vertexIndex] == Color.Black)
            {
                return false;
            }

            colors[vertexIndex] = Color.Grey;
            var edgesToEnumerate = graph.GetAdjacentEdges(vertexIndex).ToArray();
            if (edgesToEnumerate.Select(edj => this.graph.IndexOf(edj.To)).Any(this.DepthFirstSearch))
            {
                return true;
            }

            visitedVertices.Push(vertexIndex);
            colors[vertexIndex] = Color.Black;
            return false;
        }
    }
}
