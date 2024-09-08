using System;
using System.Linq;
using Newtonsoft.Json;

namespace AAEmu.Commons.Utils
{
    public static class JsonHelper
    {
        public static T DeserializeObject<T>(string json, params JsonConverter[] converters) => JsonConvert.DeserializeObject<T>(json, converters);

        public static bool TryDeserializeObject<T>(string json, out T result, out Exception error)
        {
            result = default(T);

            if (string.IsNullOrWhiteSpace(json))
            {
                error = new ArgumentException("NullOrWhiteSpace", "json");
                return false;
            }

            try
            {
                result = JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception e)
            {
                result = default(T);
                error = e;
                return false;
            }

            error = null;
            return result != null;
        }
    }

    public class ByteArrayHexConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(byte[]);

        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                var hex = serializer.Deserialize<string>(reader);
                if (!string.IsNullOrEmpty(hex))
                {
                    return Enumerable.Range(0, hex.Length)
                        .Where(x => x % 2 == 0)
                        .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                        .ToArray();
                }
            }
            return Enumerable.Empty<byte>();
        }

        private readonly string _separator;

        public ByteArrayHexConverter(string separator = ",") => _separator = separator;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var bytes = value as byte[];
            var @string = BitConverter.ToString(bytes).Replace("-", string.Empty);
            serializer.Serialize(writer, @string);
        }
    }
}
