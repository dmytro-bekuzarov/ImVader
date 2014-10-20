namespace ImVader.Algorithms
{
    using System.Collections.Generic;
    using System.Linq;
    
    /// <summary>
    /// Initialized new instance for dividing vertices into strong components. 
    /// </summary>
    /// <typeparam name="TV">Type of the vertices</typeparam>
    /// <typeparam name="TE">Type of the edges</typeparam>
    public class StrongComponents<TV, TE>
        where TE : Edge
    {
        /// <summary>
        /// List of components. List[i] is a list of vertices the i-th component contains.
        /// </summary>
        public List<List<int>> Components;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="StrongComponents{TV,TE}"/> class.
        /// </summary>
        /// <param name="g">Initial graph to divide for strong components</param>
        public StrongComponents(Graph<TV, TE> g)
        {
            Components = new List<List<int>>();
            int start, deltatimer = 0;
            var order = new List<KeyValuePair<int, int>>(g.VertexCount);
            for (int i = 0; i < g.VertexCount; i++)
                order.Add(new KeyValuePair<int, int>(i, 0));

            while (true)
            {
                for (int i = 0; i < order.Count; i++)
                    if (order[i].Value > deltatimer)
                        deltatimer = order[i].Value;
                
                start = -1;
                for (int i = 0; i < order.Count(); i++)
                    if (order[i].Value == 0)
                    {
                        start = order[i].Key;
                        break;
                    }

                if (start == -1) break;

                var dfp = new DepthFirstPathes<TV, TE>(g, start);
                for (int i = 0; i < dfp.Timeout.Count(); i++)
                    if (dfp.Timeout[i] != -1)
                    {
                        int index = 0;
                        for (int j = 0; j < order.Count; j++)
                        {
                            if (order[j].Key == i)
                            {
                                index = j;
                                break;
                            }
                        }

                        order[index] = new KeyValuePair<int, int>(i, dfp.Timeout[i] + deltatimer);
                    }

                order.Sort(new OrderComparer());
            }

            var gr = new DirectedMatrixGraph<TV, UnweightedEdge>(g.VertexCount);

            for (int i = 0; i < g.EdgesCount; i++)
            {
                gr.AddEdge(new UnweightedEdge(g.Edges[i].To, g.Edges[i].From));
                if (typeof(MatrixGraph<TV, TE>) == g.GetType() || typeof(ListGraph<TV, TE>) == g.GetType())
                    gr.AddEdge(new UnweightedEdge(g.Edges[i].From, g.Edges[i].To));
            }

            var used = new bool[g.VertexCount];

            for (int i = 0; i < gr.VertexCount; i++)
            {
                start = order[gr.VertexCount - 1 - i].Key;
                if (!used[start])
                {
                    Components.Add(new List<int>());
                    var dfp = new DepthFirstPathes<TV, UnweightedEdge>(gr, start);
                    for (int j = 0; j < gr.VertexCount; j++)
                    {
                        if (dfp.Timein[j] != -1)
                        {
                            used[j] = true;
                            Components[Components.Count - 1].Add(j);
                        }
                    }
                }

                int[] keys = new int[gr.EdgesCount];
                gr.Edges.Keys.CopyTo(keys, 0);

                for (int j = 0; j < keys.Count(); j++)
                {
                    if (used[gr.Edges[keys[j]].From] || used[gr.Edges[keys[j]].To])
                        gr.RemoveEdge(keys[j]);
                }
            }
        }

        /// <summary>
        /// Comparator class to sort "order" correctly
        /// </summary>
        private class OrderComparer : Comparer<KeyValuePair<int, int>>
        {
            /// <summary>
            /// Comparator method to sort "order" correctly
            /// </summary>
            /// <param name="x">The first pair to compare</param>
            /// <param name="y">The second pair to compare</param>
            /// <returns>1 or 0 depending on which value is bigger</returns>
            public override int Compare(KeyValuePair<int, int> x, KeyValuePair<int, int> y)
            {
                if (x.Value.CompareTo(y.Value) == 1)
                    return 0;
                return 1;
            }
        }
    }
}
