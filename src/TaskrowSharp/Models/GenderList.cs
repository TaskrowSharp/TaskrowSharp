using System;
using System.Text.Json.Serialization;
using static TaskrowSharp.Utils.JsonUtils;

namespace TaskrowSharp.Models
{
    public class GenderList
    {
        public int GenderID { get; set; }
        public string Name { get; set; }
        public string? ExternalCode { get; set; }
        public int AppMainCompanyID { get; set; }
		
		[JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
        public DateTime? ModificationDate { get; set; }
		
        public int ModificationUserID { get; set; }
        public bool Inactive { get; set; }
    }
}
