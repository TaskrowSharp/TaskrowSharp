using System;
using System.Collections.Generic;
using System.Text;

namespace TaskrowSharp.ApiModels
{
    internal class TaskDataApi
    {
        public int TaskID { get; set; }
        public int TaskNumber { get; set; }
        public string TaskTitle { get; set; }
        public int EffortEstimation { get; set; }
        public string DueDate { get; set; }
        public string CreationDate { get; set; }
        public bool Closed { get; set; }
        public int JobID { get; set; }
        public List<TaskItemApi> NewTaskItems { get; set; }
        //public List<object> ExternalTaskItems { get; set; }
        public string RowVersion { get; set; }
        public bool Unread { get; set; }
        public int DueDateOrder { get; set; }
        public int RequestTypeID { get; set; }
        public string RequestTypeAcronym { get; set; }
        public int AbsoluteUserOrder { get; set; }
        public int CreationUserID { get; set; }
        public bool HasAttachment { get; set; }
        public int PercentComplete { get; set; }
        public int PipelineStepID { get; set; }
        public int ExtranetPipelineStepID { get; set; }
        public object ClosingDate { get; set; }
        public string UrlData { get; set; }
        public ActualPermissionsApi ActualPermissions { get; set; }
        public OwnerApi Owner { get; set; }
        public LastForwardUserApi LastForwardUser { get; set; }
        public List<MemberTaskApi> Members { get; set; }
        //public List<object> ExternalMembers { get; set; }
        //public List<object> TaskAttachments { get; set; }
        //public List<object> Tags { get; set; }
        public string TagListString { get; set; }
        public string MemberListString { get; set; }
        public string EffortUnitListString { get; set; }
        //public List<object> EffortUnitTask { get; set; }
        //public object ParentTaskID { get; set; }
        //public object Deliverable { get; set; }
        //public List<object> Subtasks { get; set; }
        //public object RequestContactID { get; set; }
        //public object RequestContact { get; set; }
        //public List<object> ExternalAttachments { get; set; }
        //public object Replica { get; set; }
        //public object ParentTask { get; set; }
        public bool ExternalRequest { get; set; }
    }
}
