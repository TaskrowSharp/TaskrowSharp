﻿using System;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace TaskrowSharp.JsonConverters;

public class DateTimeTaskrowFormatJsonConverter : JsonConverter<DateTime>
{
    private const string DATE_WRITE_FORMAT = "yyyy-MM-ddTHH:mm:ss.fff";

    public override DateTime Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) =>
            Utils.ParseToDateTimeFromTaskrowDate(reader.GetString());

    public override void Write(
        Utf8JsonWriter writer,
        DateTime value,
        JsonSerializerOptions options) =>
            writer.WriteStringValue(value.ToString(DATE_WRITE_FORMAT));
}
