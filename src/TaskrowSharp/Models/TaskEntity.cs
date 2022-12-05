using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TaskrowSharp.JsonConverters;

namespace TaskrowSharp.Models
{
    public class TaskEntity
    {
        public int TaskID { get; set; }
        public int TaskNumber { get; set; }
        public string TaskTitle { get; set; }
        public int EffortEstimation { get; set; }
        public int? RemainingEffortEstimation { get; set; }

        [JsonConverter(typeof(DateTimeTaskrowFormatJsonConverter))]
        public DateTime DueDate { get; set; }
        
        [JsonConverter(typeof(DateTimeTaskrowFormatJsonConverter))]
        public DateTime CreationDate { get; set; }
        
        public bool Closed { get; set; }
        public int JobID { get; set; }
        public List<NewTaskItem>? NewTaskItems { get; set; }
        public List<ExternalTaskItem>? ExternalTaskItems { get; set; }
        public string? RowVersion { get; set; }
        public bool Unread { get; set; }
        public int DueDateOrder { get; set; }
        public int RequestTypeID { get; set; }
        public string? RequestTypeAcronym { get; set; }
        public int AbsoluteUserOrder { get; set; }
        public int CreationUserID { get; set; }
        public bool HasAttachment { get; set; }
        public int PercentComplete { get; set; }
        public int PipelineStepID { get; set; }
        public int ExtranetPipelineStepID { get; set; }
        //public object? ClosingDate { get; set; }
        public string? UrlData { get; set; }
        public ActualPermissions? ActualPermissions { get; set; }
        public Owner? Owner { get; set; }
        public User? LastForwardUser { get; set; }
        public List<Member>? Members { get; set; }
        public List<ExternalMember>? ExternalMembers { get; set; }
        public List<TaskAttachment>? TaskAttachments { get; set; }
        public List<Tag>? Tags { get; set; }
        public string TagListString { get; set; }
        public string MemberListString { get; set; }
        public string EffortUnitListString { get; set; }
        //public List<object> EffortUnitTask { get; set; }
        public int? ParentTaskID { get; set; }
        public Deliverable? Deliverable { get; set; }
        //public object? RecurringAllocation { get; set; }
        public List<Subtask>? Subtasks { get; set; }
        public int? RequestContactID { get; set; }
        public RequestContact? RequestContact { get; set; }
        public List<ExternalAttachment>? ExternalAttachments { get; set; }
        //public object? Replica { get; set; }
        public bool HasRequestDelivery { get; set; }
        public ParentTask? ParentTask { get; set; }
        public bool ExternalRequest { get; set; }
        public bool NotifyMe { get; set; }
        public bool Favorite { get; set; }
        public bool Follow { get; set; }
        public bool ExtranetPending { get; set; }
        public bool ExtranetClosed { get; set; }
        //public object? ExtranetCloseDate { get; set; }
        //public object? ExtranetCloseUserID { get; set; }
        //public object? ExtranetCloseUser { get; set; }
        public bool ExtranetCloseRequested { get; set; }
        //public object? ExtranetCloseRequestDate { get; set; }
        //public object? ExtranetCloseRequestClientContactID { get; set; }
        //public object? ExtranetCloseRequestClientContact { get; set; }
        //public TaskItemReplicasDict? TaskItemReplicasDict { get; set; }
        public bool StandBy { get; set; }
        //public object? StandByDate { get; set; }
        public OwnerSequence? OwnerSequence { get; set; }
    }
}
