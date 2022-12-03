using System;
using System.Text.Json.Serialization;
using static TaskrowSharp.Utils.JsonUtils;

namespace TaskrowSharp.Models
{
    public class AccessKey
    {
        public int UserCredentialID { get; set; }
        public string Description { get; set; }
		
		[JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
        public DateTime? MembershipDate { get; set; }
		
        public string? IP { get; set; }
        public string? Identifier { get; set; }
    }
}
