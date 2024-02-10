using System;
using System.Text.Json.Serialization;
using TaskrowSharp.JsonConverters;

namespace TaskrowSharp.Models.UserModels;

public class DateEndEntity
{
    [JsonConverter(typeof(DateTimeTaskrowFormatJsonConverter))]
    public DateTime DateEnd { get; set; }

    public int DayID { get; set; }
}
