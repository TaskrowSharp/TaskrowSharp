using System.Collections.Generic;

namespace TaskrowSharp.Models
{
    public class TaskDetailResponse
    {
        public TaskEntity TaskData { get; set; }
        public JobData JobData { get; set; }
        public bool AllowedJob { get; set; }
        public bool JobStatusDisabled { get; set; }
        public string? JobStatusMessage { get; set; }
        public bool JobInactive { get; set; }
        public string? JobInactiveMessage { get; set; }
        public bool CreateTaskPermission { get; set; }
        public List<ContextTaskTag>? ContextTaskTags { get; set; }
        //public List<object> ExternalServices { get; set; }
        public Pipeline? Pipeline { get; set; }
        public ExtranetPipeline? ExtranetPipeline { get; set; }
        public List<TaskStatus>? TaskStatus { get; set; }
        public bool NotifyMe { get; set; }
        public bool Favorite { get; set; }
        public bool Follow { get; set; }
        public bool ExtranetPending { get; set; }
        public bool RequiredTimesheet { get; set; }
        public bool ExtranetEnabled { get; set; }
        public bool CanChangeRequestType { get; set; }
        public List<RequestTypeList>? RequestTypeList { get; set; }
        public bool RequestTypeChangeRequiredParam { get; set; }
        public bool CreateTasksForAllGroupsPermission { get; set; }
        public bool ForwardTasksForAllGroupsPermission { get; set; }
        public bool ViewAllGroupsPermission { get; set; }
        public List<HierarchyGroup>? HierarchyGroups { get; set; }
        public List<SiblingTask>? SiblingTasks { get; set; }
    }
}
