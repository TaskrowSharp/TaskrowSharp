using System;
using System.Text.Json.Serialization;
using TaskrowSharp.JsonConverters;

namespace TaskrowSharp.Models.UserModels;

public class DateStartEntity
{
    [JsonConverter(typeof(DateTimeTaskrowFormatJsonConverter))]
    public DateTime DateStart { get; set; }

    public int DayID { get; set; }
}
