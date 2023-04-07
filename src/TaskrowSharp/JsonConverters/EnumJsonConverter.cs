using System.Text.Json.Serialization;
using System.Text.Json;
using System;

namespace TaskrowSharp.JsonConverters
{
    public class EnumJsonConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct, Enum
    {
        public override TEnum Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            var value = reader.GetString();
            Enum.TryParse(value, true, out TEnum result);
            return result;
        }

        public override void Write(
            Utf8JsonWriter writer,
            TEnum value,
            JsonSerializerOptions options)
        {
            writer.WriteStringValue((Convert.ToInt32(value)).ToString());
        }
    }
}
