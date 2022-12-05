using System;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace TaskrowSharp.JsonConverters
{
    public class DateTimeNullableTaskrowFormatJsonConverter : JsonConverter<DateTime?>
    {
        private const string DATE_WRITE_FORMAT = "yyyy-MM-ddTHH:mm:ss.fff";

        public override DateTime? Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
                Utils.Parser.ToDateTimeFromTaskrowDate(reader.GetString());

        public override void Write(
            Utf8JsonWriter writer,
            DateTime? value,
            JsonSerializerOptions options) =>
                writer.WriteStringValue((value.HasValue ? value?.ToString(DATE_WRITE_FORMAT) : null));
    }
}
