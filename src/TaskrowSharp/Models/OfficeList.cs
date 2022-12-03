using System;
using System.Text.Json.Serialization;
using static TaskrowSharp.Utils.JsonUtils;

namespace TaskrowSharp.Models
{
    public class OfficeList
    {
        public int OfficeID { get; set; }
        public string Name { get; set; }
        public bool MainOffice { get; set; }
        //public object? DailyMinutes { get; set; }
        public string? ExternalCode { get; set; }
        public int AppMainCompanyID { get; set; }
        
		[JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
		public DateTime? CreationDate { get; set; }
		
        public int CreationUserID { get; set; }
		
		[JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
        public DateTime? LastModificationDate { get; set; }
		
        public int LastModificationUserID { get; set; }
        public bool Inactive { get; set; }
    }
}
