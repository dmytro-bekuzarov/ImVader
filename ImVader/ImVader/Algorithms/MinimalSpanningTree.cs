// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MinimalSpanningTree.cs" company="Sigma">
//   It's a totally free software
// </copyright>
// <summary>
//   Defines the MinimalSpanningTree type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImVader.Algorithms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Initialized new instance for getting minimal spanning tree of a graph
    /// </summary>
    /// <typeparam name="TV">
    /// Type of data stored in vertices of the graph
    /// </typeparam>
    /// <typeparam name="TE">
    /// Type of edge used to connect vertices
    /// </typeparam>
    public class MinimalSpanningTree<TV, TE>
        where TE : WeightedEdge
    {
        /// <summary>
        /// Infinite weight of edge
        /// </summary>
        private const double Inf = Double.MaxValue;

        /// <summary>
        /// Edges included to resulting MST
        /// </summary>
        private ListGraph<TV, WeightedEdge> mst;

        /// <summary>
        /// Edges included to resulting MST
        /// </summary>
        private readonly List<TE> mstEdges;

        /// <summary>
        /// contains indexes of edges accessible by adjacent vertices
        /// </summary>
        private readonly TE[][] edges;

        /// <summary>
        /// min_e[i] contains the weight of edge with minimal weight, adjacent to vertex i
        /// </summary>
        private readonly double[] minE;

        /// <summary>
        /// sel_e[i] contains the other vertex adjacent to min_e[i]
        /// </summary>
        private readonly int[] selE;

        /// <summary>
        /// used[i] is "true" if vertex i is included into MST
        /// </summary>
        private readonly bool[] used;

        /// <summary>
        /// Initializes a new instance of the <see cref="MinimalSpanningTree{TV,TE}"/> class.
        /// </summary>
        /// <param name="g">
        /// Graph for MST
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Exception is thrown if MST cannot be built
        /// </exception>
        public MinimalSpanningTree(Graph<TV, TE> g)
        {
            used = new bool[g.VertexCount];
            mstEdges = new List<TE>();
            this.selE = new int[g.VertexCount];
            this.minE = new double[g.VertexCount];
            edges = new TE[g.VertexCount][];
            for (var i = 0; i < this.minE.Length; i++)
            {
                edges[i] = new TE[g.VertexCount];
            }
            for (var i = 0; i < this.minE.Length; i++)
            {
                this.minE[i] = Inf;
                this.selE[i] = -1;
            }
            for (int i = 0; i < g.EdgesCount; i++)
            {
                int a1 = g.Edges[i].From;
                int a2 = g.Edges[i].To;
                edges[a1][a2] = g.Edges[i];
                edges[a2][a1] = g.Edges[i];
            }
            this.minE[0] = 0;
            this.GetMst(g);
        }

        /// <summary>
        /// The getMST.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Exception is thrown if MST cannot be built
        /// </exception>
        private void GetMst(Graph<TV, TE> g)
        {
            mst = ((ListGraph<TV, TE>)g).CopyWeighted();
            var n = g.VertexCount;
            for (var i = 0; i < n; ++i)
            {
                var v = -1;
                for (var j = 0; j < n; ++j) if (!used[j] && (v == -1 || this.minE[j] < this.minE[v])) v = j;
                if (Math.Abs(this.minE[v] - Inf) < 0.01)
                {
                    throw new InvalidOperationException("Cannot build MST of not fully connected graph!");
                }
                used[v] = true;
                if (this.selE[v] != -1)
                {
                    var tmp = new WeightedEdge(v, this.selE[v], edges[v][this.selE[v]].Weight);

                    mstEdges.Add(edges[v][this.selE[v]]);
                    mst.AddEdge(tmp);
                }
                for (var to = 0; to < n; ++to)
                    if (edges[v][to] != null && edges[v][to].Weight < this.minE[to])
                    {
                        this.minE[to] = edges[v][to].Weight;
                        this.selE[to] = v;
                        this.selE[v] = to;
                    }
            }
        }

        /// <summary>
        /// The getMSTEdges.
        /// </summary>
        public List<TE> GetMstEdges()
        {
            return mstEdges;
        }

        /// <summary>
        /// The getMSTEdges.
        /// </summary>
        public int[][] GetMstVertices()
        {
            int[][] mstVertices = new int[mstEdges.Count][];
            for (int i = 0; i < mstEdges.Count; i++)
            {
                mstVertices[i] = new int[2];
                mstVertices[i][0] = mstEdges[i].From;
                mstVertices[i][1] = mstEdges[i].To;
            }
            return mstVertices;
        }

        /// <summary>
        /// The GetMstTree.
        /// </summary>
        public ListGraph<TV, WeightedEdge> GetMstTree()
        {
            return mst;
        }

        /// <summary>
        /// The getMSTEdges.
        /// </summary>
        public double GetMstWeight()
        {
            return this.mstEdges.Cast<WeightedEdge>().Sum(we => we.Weight);
        }
    }
}
