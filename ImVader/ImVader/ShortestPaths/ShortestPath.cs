namespace ImVader.ShortestPaths
{
    using System.Collections.Generic;

    /// <summary>
    /// The ShortestPath interface.
    /// </summary>
    /// <typeparam name="TE">
    /// edge type
    /// </typeparam>
    public class ShortestPath<TE>
        where TE : Edge
    {
        /// <summary>
        /// distTo[v] = distance  of shortest s->v path
        /// </summary>
        protected double[] DistTo;

        /// <summary>
        /// edgeTo[v] = last edge on shortest s->v path
        /// </summary>
        protected TE[] EdgeTo;

        /// <summary>
        /// The get dist to.
        /// </summary>
        /// <param name="v">
        /// the destination vertex
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// Returns the length of a shortest path from the source vertex s to vertex v.
        /// Double.POSITIVE_INFINITY if no such path
        /// </returns>
        public double GetDistTo(int v)
        {
            return this.DistTo[v];
        }

        /**
          * Is there a path from the source vertex <tt>s</tt> to vertex <tt>v</tt>?
          * @param v the destination vertex
          * @return <tt>true</tt> if there is a path from the source vertex
          *    <tt>s</tt> to vertex <tt>v</tt>, and <tt>false</tt> otherwise
          */

        /// <summary>
        /// Is there a path from the source vertex s to vertex v?
        /// </summary>
        /// <param name="v">
        /// The v.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// trueif there is a path from the source vertex s to vertex v,
        /// and false otherwise
        /// </returns>
        public bool HasPathTo(int v)
        {
            return this.DistTo[v] < double.MaxValue;
        }

        /**
         * Returns a shortest path from the source vertex <tt>s</tt> to vertex <tt>v</tt>.
         * @param v the destination vertex
         * @return a shortest path from the source vertex <tt>s</tt> to vertex <tt>v</tt>
         *    as an iterable of edges, and <tt>null</tt> if no such path
         */

        /// <summary>
        /// Returns a shortest path from the source vertex s to vertex v.
        /// </summary>
        /// <param name="v">
        /// the destination vertex
        /// </param>
        /// <returns>
        /// a shortest path from the source vertex s to vertex v
        /// as an iterable of edges, and null if no such path
        /// The <see>
        ///         <cref>IEnumerable</cref>
        ///     </see>
        ///     .
        /// </returns>
        public IEnumerable<TE> PathTo(int v)
        {
            if (!HasPathTo(v)) return null;
            var path = new Stack<TE>();
            for (var e = this.EdgeTo[v]; e != null; e = this.EdgeTo[e.V])
            {
                path.Push(e);
            }

            return path;
        }


        public IEnumerable<int> PathToAsIds(int v)
        {
            if (!HasPathTo(v)) return null;
            var path = new Stack<int>();
            for (var e = this.EdgeTo[v]; e != null; e = this.EdgeTo[e.V])
            {
                path.Push(e.V);
                if (this.EdgeTo[e.V] == null)
                {
                    path.Push(e.W);
                }
            }
            return path;
        }
    }
}
