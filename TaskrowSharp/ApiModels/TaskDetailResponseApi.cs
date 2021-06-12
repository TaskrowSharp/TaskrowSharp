using System.Collections.Generic;

namespace TaskrowSharp.ApiModels
{
    internal class TaskDetailResponseApi
    {
        public TaskDataApi TaskData { get; set; }
        public JobDataApi JobData { get; set; }
        public bool AllowedJob { get; set; }
        public bool JobStatusDisabled { get; set; }
        public string JobStatusMessage { get; set; }
        public bool JobInactive { get; set; }
        public string JobInactiveMessage { get; set; }
        public bool CreateTaskPermission { get; set; }
        public List<ContextTaskTagApi> ContextTaskTags { get; set; }
        //public List<object> ExternalServices { get; set; }
        public PipelineApi Pipeline { get; set; }
        public ExtranetPipelineApi ExtranetPipeline { get; set; }
        public List<TaskStatusApi> TaskStatus { get; set; }
        public bool NotifyMe { get; set; }
        public bool Favorite { get; set; }
        public bool Follow { get; set; }
        public bool RequiredTimesheet { get; set; }
        public bool ExtranetEnabled { get; set; }
        public bool ExtranetPending { get; set; }
        public bool CanChangeRequestType { get; set; }
        public List<RequestTypeListApi> RequestTypeList { get; set; }
        public bool RequestTypeChangeRequiredParam { get; set; }
    }
}
