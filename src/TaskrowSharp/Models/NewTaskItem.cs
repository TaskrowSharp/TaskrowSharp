using System;
using System.Text.Json.Serialization;
using static TaskrowSharp.Utils.JsonUtils;

using System.Collections.Generic;

namespace TaskrowSharp.Models
{
    public class NewTaskItem
    {
        public int TaskItemID { get; set; }
        public int PreviousOwnerUserID { get; set; }
        public int OldOwnerUserID { get; set; }
        public string? OldOwnerName { get; set; }
        public string? OldOwnerHashCode { get; set; }
        public int? NewOwnerUserID { get; set; }
        public string? NewOwnerName { get; set; }
        public string? NewOwnerHashCode { get; set; }
        public string? TaskItemComment { get; set; }
        public int? RequestTypeID { get; set; }
        public int? ReferencedTaskItemID { get; set; }
        public RequestType? RequestType { get; set; }
        public bool HasDynForm { get; set; }
        public int TaskItemTypeID { get; set; }
        
		[JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
		public DateTime? Date { get; set; }
		
		[JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
        public DateTime? DueDate { get; set; }

        [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
        public DateTime? PreviousDueDate { get; set; }		

        public bool? Closed { get; set; }
        public bool PreviousClosedState { get; set; }
        public bool DiffDueDate { get; set; }
        public int TaskItemAction { get; set; }
        public bool HasAttachment { get; set; }
        public int? PercentComplete { get; set; }
        public int? PipelineStepID { get; set; }
        public bool ProgressChange { get; set; }
        public List<Attachment>? Attachments { get; set; }
        public int? EffortEstimation { get; set; }
        //public object? RemainingEffortEstimation { get; set; }
        public int? SubtaskID { get; set; }
        public Subtask? Subtask { get; set; }
        //public object? TaskItemTemplateID { get; set; }
        //public object? TaskItemTemplate { get; set; }
        public bool DiffStandByDate { get; set; }
        //public object? StandByDate { get; set; }
        //public object? PreviousStandByDate { get; set; }
    }
}
