namespace ImVader
{
    /// <summary>
    /// Represents unweighted edge in the graph
    /// </summary>
    public class UnweightedEdge : Edge
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnweightedEdge"/> class.
        /// </summary>
        /// <param name="v">
        /// First vertex id of the edge 
        /// </param>
        /// <param name="w">
        /// Second vertex of the edge
        /// </param>
        protected UnweightedEdge(int v, int w)
            : base(v, w)
        {
        }
    }
}
