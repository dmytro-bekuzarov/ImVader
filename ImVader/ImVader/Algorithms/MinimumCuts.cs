namespace ImVader.Algorithms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MinimumCuts<TV, TE>
        where TE : WeightedEdge
    {
        private double bestCost = Double.MaxValue;

        private readonly Graph<TV, TE> initialGraph;

        private List<int> bestCut;

        public List<int> BestCut
        {
            get
            {
                return this.bestCut.Select(v => this.initialGraph.Indexes[v]).ToList();
            }
        }

        public double BestCost
        {
            get
            {
                return bestCost;
            }
        }

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

        private void MinCut(double[][] graph)
        {
            var n = graph.GetLength(0);
            var v = new List<int>[n];
            for (var i = 0; i < v.Length; i++)
            {
                v[i] = new List<int> { i };
            }

            var exists = new Boolean[n];
            var inA = new Boolean[n];
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
