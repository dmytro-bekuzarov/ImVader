using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImVader;

namespace Test
{
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            var buggyGraph = new DirectedMatrixGraph<int, Edge>(5);
            
            buggyGraph.AddEdge(new Edge(1, 2));
            buggyGraph.AddEdge(new WeightedEdge(1, 2, 3));
            buggyGraph.AddVertex(12);
        }
    }
}
