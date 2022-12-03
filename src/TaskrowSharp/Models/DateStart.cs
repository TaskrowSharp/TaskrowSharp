using System;
using System.Text.Json.Serialization;
using static TaskrowSharp.Utils.JsonUtils;

namespace TaskrowSharp.Models
{
    public class DateStartEntity
    {
		[JsonConverter(typeof(DateTimeTaskrowFormatJsonConverter))]
        public DateTime DateStart { get; set; }
		
        public int DayID { get; set; }
    }
}
