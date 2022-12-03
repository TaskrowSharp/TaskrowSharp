using System;
using System.Text.Json.Serialization;
using static TaskrowSharp.Utils.JsonUtils;

namespace TaskrowSharp.Models
{
    public class ExternalAttachment
    {
        public int AttachmentID { get; set; }
        public string? Identification { get; set; }
        public string? Name { get; set; }
        public string? MimeType { get; set; }
        public int AttachmentTypeID { get; set; }
        public int CreationUserID { get; set; }
		
		[JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
        public DateTime? CreationDate { get; set; }
		
        //public object? GroupID { get; set; }
        //public object? NoteID { get; set; }
        //public object? ClientID { get; set; }
        //public object? JobID { get; set; }
        //public object? DocumentID { get; set; }
        //public object? ExpenseEntryID { get; set; }
        //public object? WallPostID { get; set; }
        //public object? BudgetID { get; set; }
        //public object? InvoiceID { get; set; }
        public bool CanDelete { get; set; }
        //public object? ApprovalRequestItem { get; set; }
    }
}
