using System;
using System.Text.Json.Serialization;
using TaskrowSharp.JsonConverters;

namespace TaskrowSharp.Models
{
    public class Office
    {
        public int OfficeID { get; set; }
        public string? Name { get; set; }
        public bool MainOffice { get; set; }
        //public object? DailyMinutes { get; set; }
        //public object? ExternalCode { get; set; }
        public int AppMainCompanyID { get; set; }
        
		[JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
		public DateTime? CreationDate { get; set; }
		
        public int CreationUserID { get; set; }
        public string? LastModificationDate { get; set; }
        public int LastModificationUserID { get; set; }
        public bool Inactive { get; set; }
    }
}
