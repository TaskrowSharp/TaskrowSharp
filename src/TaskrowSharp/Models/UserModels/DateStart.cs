using System;
using System.Text.Json.Serialization;
using TaskrowSharp.JsonConverters;

namespace TaskrowSharp.Models.UserModels;

public class DateStartEntity
{
    [JsonConverter(typeof(DateTimeTaskrowFormatJsonConverter))]
    public DateTimeOffset DateStart { get; set; }

    public int DayID { get; set; }
}
