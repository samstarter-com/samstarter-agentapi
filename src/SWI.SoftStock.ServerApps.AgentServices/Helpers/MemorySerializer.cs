using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace SWI.SoftStock.ServerApps.AgentServices.Helpers
{
    public class MemorySerializer<T>
    {
        public string SerializeObject(T obj)
        {
            using (var memoryStream = new MemoryStream())
            using (var reader = new StreamReader(memoryStream))
            {
                var serializer = new DataContractSerializer(typeof (T));
                serializer.WriteObject(memoryStream, obj);
                memoryStream.Position = 0;
                return reader.ReadToEnd();
            }
        }

        public T Deserialize(string xml)
        {
            using (Stream stream = new MemoryStream())
            {
                byte[] data = Encoding.UTF8.GetBytes(xml);
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                var deserializer = new DataContractSerializer(typeof (T));
                return (T) deserializer.ReadObject(stream);
            }
        }
    }
}