// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MinimumCuts.cs" company="Sigma">
//   It's a totally free software.
// </copyright>
// <summary>
//   Defines the MinimumCuts type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImVader.Algorithms
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents an implementation of the algorithm for finding a minimum cut of the graph.
    /// </summary>
    /// <typeparam name="TV">
    /// Type of data stored in vertices of the graph.
    /// </typeparam>
    /// <typeparam name="TE">
    /// Type of edge of the graph.
    /// </typeparam>
    public class MinimumCuts<TV, TE>
        where TE : WeightedEdge
    {
        /// <summary>
        /// The initial graph.
        /// </summary>
        private readonly Graph<TV, TE> initialGraph;

        /// <summary>
        /// The weight of the minimum cut.
        /// </summary>
        private double minimumCost = double.MaxValue;

        /// <summary>
        /// The list of indexes that represents a minimum cut.
        /// </summary>
        private List<int> minimumCut;

        /// <summary>
        /// Initializes a new instance of the <see cref="MinimumCuts{TV,TE}"/> class.
        /// </summary>
        /// <param name="graph">
        ///  Graph, for which minimun cut must be found.
        /// </param>
        public MinimumCuts(Graph<TV, TE> graph)
        {
            initialGraph = graph;
            this.minimumCut = new List<int>();
            var gmatrix = new double[graph.VertexCount][];
            for (var i = 0; i < gmatrix.GetLength(0); i++)
            {
                gmatrix[i] = new double[graph.VertexCount];
            }

            for (var i = 0; i < gmatrix.GetLength(0); i++)
            {
                var adjacentEdges = graph.GetAdjacentEdges(i);
                foreach (var edge in adjacentEdges)
                {
                    gmatrix[i][initialGraph.IndexOf(edge.Other(initialGraph.IndexedValue(i)))] = edge.Weight;
                    gmatrix[initialGraph.IndexOf(edge.Other(initialGraph.IndexedValue(i)))][i] = edge.Weight;
                }
            }

            MinCut(gmatrix);
        }

        /// <summary>
        /// Gets the list of vertices indexes that represents a minimum cut.
        /// </summary>
        /// <value>
        /// The list of vertices indexes that represents a minimum cut.
        /// </value>
        public List<int> MinimumCut
        {
            get
            {
                return this.minimumCut.Select(v => this.initialGraph.IndexedValue(v)).ToList();
            }
        }

        /// <summary>
        /// Gets the weight of the minimum cut.
        /// </summary>
        /// <value>
        /// The weight of the minimum cut.
        /// </value>
        public double MinimumCost
        {
            get
            {
                return this.minimumCost;
            }
        }

        /// <summary>
        /// Encapsulates algorithm for finding a minimum cut of the graph.
        /// </summary>
        /// <param name="graph">
        /// Graph, for which minimal cut must be found.
        /// </param>
        private void MinCut(double[][] graph)
        {
            var length = graph.GetLength(0);
            var minCutPath = new List<int>[length];
            for (var i = 0; i < minCutPath.Length; i++)
            {
                minCutPath[i] = new List<int> { i };
            }

            var exists = new bool[length];
            var inSet = new bool[length];
            var coverDistance = new double[length];

            for (var i = 0; i < length; i++)
            {
                exists[i] = true;
            }

            for (var k = 0; k < length - 1; k++)
            {
                for (var i = 0; i < length; i++)
                {
                    inSet[i] = false;
                    coverDistance[i] = 0;
                }

                for (int j = 0, prev = -1; j < length - k; j++)
                {
                    var selected = -1;

                    for (var i = 0; i < length; ++i)
                        if (exists[i] && !inSet[i] && (selected == -1 || coverDistance[i] > coverDistance[selected]))
                            selected = i;

                    if (j == length - k - 1)
                    {
                        if (coverDistance[selected] < this.minimumCost)
                        {
                            this.minimumCost = coverDistance[selected];
                            this.minimumCut = minCutPath[selected];
                        }

                        minCutPath[prev].AddRange(minCutPath[selected]);
                        for (var i = 0; i < length; ++i)
                            graph[prev][i] = graph[i][prev] += graph[selected][i];
                        exists[selected] = false;
                    }
                    else
                    {
                        inSet[selected] = true;
                        for (var i = 0; i < length; i++)
                            coverDistance[i] += graph[selected][i];

                        prev = selected;
                    }
                }
            }
        }
    }
}