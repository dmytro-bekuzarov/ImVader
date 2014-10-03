using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImVader
{
    interface IGraph
    {
         int TotalVertices { get; }
         int TotalEdges { get; }
         IVertex AddVertex(IVertex v);
         IVertex GetVertex(int id);
         void RemoveVertex(int id);
         Edge AddEdge(Edge e);
         Edge GetEdge(int id);
    }
}
