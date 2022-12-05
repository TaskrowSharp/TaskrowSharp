using System;
using System.Text.Json.Serialization;
using TaskrowSharp.JsonConverters;
using System.Collections.Generic;

namespace TaskrowSharp.Models
{
    public class ExternalTaskItem
    {
        public int ExternalTaskItemID { get; set; }
        public string TaskItemComment { get; set; }
        public int? FromUserID { get; set; }
        public int? ToUserID { get; set; }
        
		[JsonConverter(typeof(DateTimeTaskrowFormatJsonConverter))]
		public DateTime Date { get; set; }
		
        public int ExternalTaskItemTypeID { get; set; }
        //public object? Approved { get; set; }
        public User? FromUser { get; set; }
        public User? ToUser { get; set; }
        public Contact? FromContact { get; set; }
        public Contact? ToContact { get; set; }
        public List<Attachment>? Attachments { get; set; }
        public bool CanDeleteAttachments { get; set; }
        //public object? ApprovalRequest { get; set; }
        public int? ExtranetPipelineStepID { get; set; }
    }
}
