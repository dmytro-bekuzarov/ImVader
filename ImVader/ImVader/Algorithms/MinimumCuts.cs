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
        private double bestCost = double.MaxValue;

        /// <summary>
        /// The list of indexes that represents a minimum cut.
        /// </summary>
        private List<int> bestCut;

        /// <summary>
        /// Initializes a new instance of the <see cref="MinimumCuts{TV,TE}"/> class.
        /// </summary>
        /// <param name="graph">
        ///  Graph, for which minimun cut must be found.
        /// </param>
        public MinimumCuts(Graph<TV, TE> graph)
        {
            initialGraph = graph;
            bestCut = new List<int>();
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
                    gmatrix[i][initialGraph.IndexOf(edge.Other(initialGraph.Indexes[i]))] = edge.Weight;
                    gmatrix[initialGraph.IndexOf(edge.Other(initialGraph.Indexes[i]))][i] = edge.Weight;
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
        public List<int> BestCut
        {
            get
            {
                return this.bestCut.Select(v => this.initialGraph.Indexes[v]).ToList();
            }
        }

        /// <summary>
        /// Gets the weight of the minimum cut.
        /// </summary>
        /// <value>
        /// The weight of the minimum cut.
        /// </value>
        public double BestCost
        {
            get
            {
                return bestCost;
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
            var n = graph.GetLength(0);
            var v = new List<int>[n];
            for (var i = 0; i < v.Length; i++)
            {
                v[i] = new List<int> { i };
            }

            var exists = new bool[n];
            var inA = new bool[n];
            var w = new double[n];

            for (var i = 0; i < n; i++)
            {
                exists[i] = true;
            }

            for (var ph = 0; ph < n - 1; ph++)
            {
                for (var i = 0; i < n; i++)
                {
                    inA[i] = false;
                    w[i] = 0;
                }

                for (int it = 0, prev = -1; it < n - ph; it++)
                {
                    var sel = -1;

                    for (var i = 0; i < n; ++i)
                        if (exists[i] && !inA[i] && (sel == -1 || w[i] > w[sel]))
                            sel = i;

                    if (it == n - ph - 1)
                    {
                        if (w[sel] < bestCost)
                        {
                            bestCost = w[sel];
                            bestCut = v[sel];
                        }

                        v[prev].AddRange(v[sel]);
                        for (var i = 0; i < n; ++i)
                            graph[prev][i] = graph[i][prev] += graph[sel][i];
                        exists[sel] = false;
                    }
                    else
                    {
                        inA[sel] = true;
                        for (var i = 0; i < n; i++)
                            w[i] += graph[sel][i];

                        prev = sel;
                    }
                }
            }
        }
    }
}