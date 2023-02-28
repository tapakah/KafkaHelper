using Confluent.Kafka;
using System;
using System.Buffers.Binary;
using System.IO;
using System.Text;

namespace KafkaHelpers
{
    public class KeyDeserializer : IDeserializer<string>
    {
        private readonly Encoding _encoding;

        public KeyDeserializer(Encoding encoding)
        {
            _encoding = encoding;
        }

        public string Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            if (isNull)
            {
                return null;
            }

            var array = data.ToArray();

            var stream = new MemoryStream(array);

            var sr = new StreamReader(stream, _encoding);

            if (BinaryPrimitives.TryReadInt64BigEndian(data, out long x))
                return x.ToString();
            else
                return sr.ReadToEnd();
        }
    }
}