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
    /// Represents an implementation of the algorithm for findingW a minimum spanning tree.
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
        private const double infinity = double.MaxValue;

        /// <summary>
        /// Edges included to resulting minimal spanning tree.
        /// </summary>
        private readonly List<TE> mstEdges;

        /// <summary>
        /// Contains indexes of the edges accessible from adjacent vertices.
        /// </summary>
        private readonly TE[][] initialEdges;

        /// <summary>
        /// Contains the weight of the edge with minimal weight, adjacent to vertex with appropriate index.
        /// </summary>
        private readonly double[] minimalWeightEdges;

        /// <summary>
        /// Contains the other vertex adjacent to minimalWeightEdges[i].
        /// </summary>
        private readonly int[] otherAdjacentVertex;

        /// <summary>
        /// Value in used[i] is "true" if vertex i is included into minimal spanning tree.
        /// </summary>
        private readonly bool[] isIncludedToMst;

        /// <summary>
        /// Resulting minimal spanning tree.
        /// </summary>
        private ListGraph<TV, WeightedEdge> resultingTree;

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
            isIncludedToMst = new bool[g.VertexCount];
            mstEdges = new List<TE>();
            otherAdjacentVertex = new int[g.VertexCount];
            minimalWeightEdges = new double[g.VertexCount];
            initialEdges = new TE[g.VertexCount][];

            for (var i = 0; i < minimalWeightEdges.Length; i++)
            {
                initialEdges[i] = new TE[g.VertexCount];
            }

            for (var i = 0; i < minimalWeightEdges.Length; i++)
            {
                minimalWeightEdges[i] = infinity;
                otherAdjacentVertex[i] = -1;
            }

            int from, to;
            for (int i = 0; i < g.EdgesCount; i++)
            {
                from = g.Edges[i].From;
                to = g.Edges[i].To;
                initialEdges[from][to] = g.Edges[i];
                initialEdges[to][from] = g.Edges[i];
            }

            minimalWeightEdges[0] = 0;
            FindMst(g);
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
        /// Returns the minimal spanning tree.
        /// </summary>
        /// <returns>
        /// The minimal spanning tree.
        /// </returns>
        public ListGraph<TV, WeightedEdge> GetMstTree()
        {
            return resultingTree;
        }

        /// <summary>
        /// Returns total weight of the minimal spanning tree.
        /// </summary>
        /// <returns>
        /// Total weight of the minimal spanning tree.
        /// </returns>
        public double GetMstWeight()
        {
            return mstEdges.Cast<WeightedEdge>().Sum(we => we.Weight);
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
        private void FindMst(Graph<TV, TE> g)
        {
            resultingTree = ((ListGraph<TV, TE>)g).CopyWeighted();
            var n = g.VertexCount;
            for (var i = 0; i < n; ++i)
            {
                var v = -1;
                for (var j = 0; j < n; ++j)
                {
                    if (!isIncludedToMst[j] && (v == -1 || minimalWeightEdges[j] < minimalWeightEdges[v]))
                    {
                        v = j;
                    }
                }
                if (Math.Abs(minimalWeightEdges[v] - infinity) < 0.01)
                {
                    throw new InvalidOperationException("Cannot build MST of not fully connected graph!");
                }

                isIncludedToMst[v] = true;
                if (otherAdjacentVertex[v] != -1)
                {
                    var tmp = new WeightedEdge(v, otherAdjacentVertex[v], initialEdges[v][otherAdjacentVertex[v]].Weight);

                    mstEdges.Add(initialEdges[v][otherAdjacentVertex[v]]);
                    resultingTree.AddEdge(tmp);
                }

                for (var to = 0; to < n; ++to)
                {
                    if (initialEdges[v][to] != null && initialEdges[v][to].Weight < minimalWeightEdges[to])
                    {
                        minimalWeightEdges[to] = initialEdges[v][to].Weight;
                        otherAdjacentVertex[to] = v;
                        otherAdjacentVertex[v] = to;
                    }
                }
            }
        }
    }
}
