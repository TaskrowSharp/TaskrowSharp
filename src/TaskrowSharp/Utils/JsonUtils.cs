using System.Text.Json.Serialization;
using System.Text.Json;
using System;

namespace TaskrowSharp.Utils
{
    public static class JsonUtils
    {
        private const string DATE_WRITE_FORMAT = "yyyy-MM-ddTHH:mm:ss.fff";

        public class DateTimeNullableTaskrowFormatJsonConverter : JsonConverter<DateTime?>
        {
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

        public class DateTimeTaskrowFormatJsonConverter : JsonConverter<DateTime>
        {
            public override DateTime Read(
                ref Utf8JsonReader reader,
                Type typeToConvert,
                JsonSerializerOptions options) =>
                    Utils.Parser.ToDateTimeFromTaskrowDate(reader.GetString());

            public override void Write(
                Utf8JsonWriter writer,
                DateTime value,
                JsonSerializerOptions options) =>
                    writer.WriteStringValue(value.ToString(DATE_WRITE_FORMAT));
        }
    }
}
