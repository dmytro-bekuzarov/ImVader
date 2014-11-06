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
    /// Represents an implementation of the algorithm for finding a minimum spanning tree.
    /// </summary>
    /// <typeparam name="TV">
    /// Type of data stored in vertices of the graph.
    /// </typeparam>
    /// <typeparam name="TE">
    /// Type of edge of the graph.
    /// </typeparam>
    public class MinimalSpanningTree<TV, TE>
        where TE : WeightedEdge
    {
        /// <summary>
        /// Infinite weight of edge.
        /// </summary>
        private const double Infinity = double.MaxValue;

        /// <summary>
        /// Edges included to resulting minimal spanning tree.
        /// </summary>
        private readonly List<TE> mstEdges;

        /// <summary>
        /// Contains indexes of the edges accessible from adjacent vertices.
        /// </summary>
        private readonly TE[][] edges;

        /// <summary>
        /// Contains the weight of the edge with minimal weight, adjacent to vertex with appropriate index.
        /// </summary>
        private readonly double[] minE;

        /// <summary>
        /// Contains the other vertex adjacent to min_e[i].
        /// </summary>
        private readonly int[] selE;

        /// <summary>
        /// Value in used[i] is "true" if vertex i is included into minimal spanning tree.
        /// </summary>
        private readonly bool[] used;

        /// <summary>
        /// Resulting minimal spanning tree.
        /// </summary>
        private ListGraph<TV, WeightedEdge> mst;

        /// <summary>
        /// Initializes a new instance of the <see cref="MinimalSpanningTree{TV,TE}"/> class.
        /// </summary>
        /// <param name="g">
        /// Graph, for which minimal spanning tree is built.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Exception is thrown if MST cannot be built.
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
                this.minE[i] = Infinity;
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
        /// Returns edges of the minimal spanning tree.
        /// </summary>
        /// <returns>
        /// Edges of the minimal spanning tree.
        /// </returns>
        public List<TE> GetMstEdges()
        {
            return mstEdges;
        }

        /// <summary>
        /// Returns the vertices of the minimal spanning tree.
        /// </summary>
        /// <returns>
        /// Vertices of the minimal spanning tree.
        /// </returns>
        public int[][] GetMstVertices()
        {
            var mstVertices = new int[mstEdges.Count][];
            for (int i = 0; i < mstEdges.Count; i++)
            {
                mstVertices[i] = new int[2];
                mstVertices[i][0] = mstEdges[i].From;
                mstVertices[i][1] = mstEdges[i].To;
            }

            return mstVertices;
        }

        /// <summary>
        /// Returns the minimal spanning tree.
        /// </summary>
        /// <returns>
        /// The minimal spanning tree.
        /// </returns>
        public ListGraph<TV, WeightedEdge> GetMstTree()
        {
            return mst;
        }

        /// <summary>
        /// Returns total weight of the minimal spanning tree.
        /// </summary>
        /// <returns>
        /// Total weight of the minimal spanning tree.
        /// </returns>
        public double GetMstWeight()
        {
            return this.mstEdges.Cast<WeightedEdge>().Sum(we => we.Weight);
        }

        /// <summary>
        /// Encapsulates algorithm for finding a minimum spanning tree of the graph g.
        /// </summary>
        /// <param name="g">
        /// Graph, for which minimal spanning tree is built.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Exception is thrown if minimal spanning tree cannot be built.
        /// </exception>
        private void GetMst(Graph<TV, TE> g)
        {
            mst = ((ListGraph<TV, TE>)g).CopyWeighted();
            var n = g.VertexCount;
            for (var i = 0; i < n; ++i)
            {
                var v = -1;
                for (var j = 0; j < n; ++j) if (!used[j] && (v == -1 || this.minE[j] < this.minE[v])) v = j;
                if (Math.Abs(this.minE[v] - Infinity) < 0.01)
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
                {
                    if (edges[v][to] != null && edges[v][to].Weight < this.minE[to])
                    {
                        this.minE[to] = edges[v][to].Weight;
                        this.selE[to] = v;
                        this.selE[v] = to;
                    }
                }
            }
        }
    }
}
