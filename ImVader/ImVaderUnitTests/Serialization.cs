namespace ImVaderUnitTests
{
    using System.IO;

    using ImVader;
    using ImVader.Utils;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Serialization
    {
        [TestMethod]
        public void SerializeToFile()
        {
            const string Path = "graph.txt";
            var graph1 = new ListGraph<int, WeightedEdge>(10);
            graph1.AddEdge(new WeightedEdge(1, 2, 2.3));
            var stream = new FileStream(Path, FileMode.Create);
            var serializer = new JsonSerializer<ListGraph<int, WeightedEdge>>();
            serializer.Serialize(graph1, stream);
            var reader = new FileStream(Path, FileMode.Open);
            ListGraph<int, WeightedEdge> graph2 = serializer.Deserialize(reader);
            Assert.AreEqual(graph1.ToString(), graph2.ToString());
            File.Delete(Path);
        }

        [TestMethod]
        public void SerializeToString()
        {
            var graph1 = new ListGraph<int, WeightedEdge>(10);
            graph1.AddEdge(new WeightedEdge(1, 2, 2.3));
            var serializer = new JsonSerializer<ListGraph<int, WeightedEdge>>();
            string s = serializer.Serialize(graph1);
            ListGraph<int, WeightedEdge> graph2 = serializer.Deserialize(s);
            Assert.AreEqual(graph1.ToString(), graph2.ToString());
        }

       
    }

}
