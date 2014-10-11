namespace ImVader.Utils
{
    using System.IO;

    using Newtonsoft.Json;

    /// <summary>
    /// The json serializer.
    /// </summary>
    /// <typeparam name="T">
    /// Type to be selialized
    /// </typeparam>
    public class JsonSerializer<T>
    {
        public string Serialize(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public string Serialize(T obj, Stream stream)
        {
            string s = Serialize(obj);
            using (var streamWriter = new StreamWriter(stream))
            {
                streamWriter.Write(s);
            }

            return s;
        }

        public T Deserialize(string s)
        {
            return JsonConvert.DeserializeObject<T>(s);
        }

        public T Deserialize(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return Deserialize(reader.ReadToEnd());
            }
        }
    }
}
