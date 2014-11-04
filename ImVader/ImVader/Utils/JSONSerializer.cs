// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonSerializer.cs"  company="Sigma">
//   It's a totally free software
// </copyright>
// <summary>
//   Defines the JsonSerializer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImVader.Utils
{
    using System.IO;

    using Newtonsoft.Json;

    /// <summary>
    /// Needed for seliazing objects to stream as JSON objects and deserialization from JSON string.
    /// </summary>
    /// <typeparam name="T">
    /// Type to be selialized.
    /// </typeparam>
    public class JsonSerializer<T>
    {
        /// <summary>
        /// Serializes the object to the JSON string.
        /// </summary>
        /// <param name="obj">
        /// The object to serialize.
        /// </param>
        /// <returns>
        /// The object serialized to the JSON string.
        /// </returns>
        public string Serialize(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// Serializes the object to the JSON string.
        /// </summary>
        /// <param name="obj">
        /// The object to serialize.
        /// </param>
        /// <param name="stream">
        /// The stream in which object will be serialized to.
        /// </param>
        /// <returns>
        /// The object serialized to the JSON string.
        /// </returns>
        public string Serialize(T obj, Stream stream)
        {
            string s = Serialize(obj);
            using (var streamWriter = new StreamWriter(stream))
            {
                streamWriter.Write(s);
            }

            return s;
        }

        /// <summary>
        /// Deserializes the object from JSON string.
        /// </summary>
        /// <param name="s">
        /// The JSON string.
        /// </param>
        /// <returns>
        /// The deserialized object.
        /// </returns>
        public T Deserialize(string s)
        {
            return JsonConvert.DeserializeObject<T>(s);
        }

        /// <summary>
        /// Deserializes the object from the stream.
        /// </summary>
        /// <param name="stream">
        /// The stream to read.
        /// </param>
        /// <returns>
        /// The deserialized object.
        /// </returns>
        public T Deserialize(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return Deserialize(reader.ReadToEnd());
            }
        }
    }
}
