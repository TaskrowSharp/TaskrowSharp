using System;
using System.Text.Json.Serialization;
using static TaskrowSharp.Utils.JsonUtils;

namespace TaskrowSharp.Models
{
    public class DateEndEntity
    {
		[JsonConverter(typeof(DateTimeTaskrowFormatJsonConverter))]
        public DateTime DateEnd { get; set; }
		
        public int DayID { get; set; }
    }
}
