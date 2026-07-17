using System;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace TaskrowSharp.JsonConverters;

public class DateTimeNullableTaskrowFormatJsonConverter : JsonConverter<DateTimeOffset?>
{
    private const string DATE_WRITE_FORMAT = "yyyy-MM-ddTHH:mm:ss.fff";

    public override DateTimeOffset? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) =>
            Utils.ParseToDateTimeFromTaskrowDate(reader.GetString());

    public override void Write(
        Utf8JsonWriter writer,
        DateTimeOffset? value,
        JsonSerializerOptions options) =>
            writer.WriteStringValue((value.HasValue ? value?.ToString(DATE_WRITE_FORMAT) : null));
}
