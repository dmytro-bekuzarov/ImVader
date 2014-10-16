namespace ImVader.ShortestPaths
{
    using System;
    using System.Linq;

    /// <summary>
    /// The dijkstra algorithm
    /// </summary>
    /// <typeparam name="TV">
    /// Vertex type
    /// </typeparam>
    /// <typeparam name="TE">
    /// Edge type
    /// </typeparam>
    public class Dijkstra<TV, TE> : ShortestPath<TV, TE>
        where TE : Edge
    {
        /// <summary>
        /// The priority queue of vertices
        /// </summary>
        private readonly MinPq<Node> pq;  

        /// <summary>
        /// Initializes a new instance of the <see cref="Dijkstra{TV,TE}"/> class. 
        /// Processes graph via Dijkstra algo
        /// </summary>
        /// <param name="gr">
        /// directed graph to be precessed+
        /// </param>
        /// <param name="s">
        /// Entry vertex
        /// </param>
        public Dijkstra(IDirectedGraph gr, int s)
            : base((Graph<TV, TE>)gr)
        {
            
            s = g.IndexOf(s);
            for (var i = 0; i < g.EdgesCount; i++)
            {
                Edge e = g.GetEdge(i);
                var edge = e as WeightedEdge;
                if (edge != null)
                {
                    if (edge.Weight < 0)
                    {
                        throw new ArgumentException("edge " + e + " has negative weight");
                    }
                }
            }

            this.EdgeTo = new TE[g.VertexCount];
            this.DistTo = new double[g.VertexCount];
            for (var v = 0; v < g.VertexCount; v++)
            {
                this.DistTo[v] = double.MaxValue;
            }

            this.DistTo[s] = 0.0;

            // relax vertices in order of distance from s
            pq = new MinPq<Node>(g.VertexCount);
            pq.Insert(new Node(s, this.DistTo[s]));
            while (!pq.IsEmpty())
            {
                var v = pq.DelMin();
                var adj = g.GetAdjacentEdges(v.N);
                var enumerable = adj as TE[] ?? adj.ToArray();
                for (var i = 0; i < enumerable.Count(); i++)
                {
                    this.Relax(enumerable[i]);
                }
            }
        }

        /// <summary>
        /// relax edge e and update pq if changed
        /// </summary>
        /// <param name="edge">
        /// The edge.
        /// </param>
        private void Relax(TE edge)
        {
            var e = edge as Edge;
            int v = g.IndexOf(e.From), w = g.IndexOf(e.To);
            var weight = 1.0;
            var weightedEdge = e as WeightedEdge;
            if (weightedEdge != null)
            {
                weight = weightedEdge.Weight;
            }

            if (this.DistTo[w] > this.DistTo[v] + weight)
            {
                this.DistTo[w] = this.DistTo[v] + weight;
                this.EdgeTo[w] = edge;
                ChangeOrAdd(w, this.DistTo[w]);
            }
        }

        /// <summary>
        /// The change or add while relaxing is done
        /// </summary>
        /// <param name="w">
        /// Vertex to add or update
        /// </param>
        /// <param name="weight">
        /// New weight
        /// </param>
        private void ChangeOrAdd(int w, double weight)
        {
            bool b = false;

            var all = this.pq.GetAsEnumerable();
            var enumerable = all as Node[] ?? all.ToArray();

            foreach (var t in enumerable.Where(t => t.N == w))
            {
                t.Weight = weight;
                b = true;
            }

            if (!b)
            {
                pq.Insert(new Node(w, weight));
            }
        }
    }
}
