using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;

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
