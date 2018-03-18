using System;
using System.Collections.Generic;
using System.Text;

namespace TaskrowSharp.ApiModels
{
    internal class TaskItemApi
    {
        public int TaskItemID { get; set; }
        public int OldOwnerUserID { get; set; }
        public string OldOwnerName { get; set; }
        public string OldOwnerHashCode { get; set; }
        public int NewOwnerUserID { get; set; }
        public string NewOwnerName { get; set; }
        public string NewOwnerHashCode { get; set; }
        public string TaskItemComment { get; set; }
        public int? RequestTypeID { get; set; }
        public RequestTypeApi RequestType { get; set; }
        public int TaskItemTypeID { get; set; }
        public string Date { get; set; }
        public string DueDate { get; set; }
        public string PreviousDueDate { get; set; }
        //public object Closed { get; set; }
        public bool PreviousClosedState { get; set; }
        public bool DiffDueDate { get; set; }
        public int TaskItemAction { get; set; }
        public bool HasAttachment { get; set; }
        public int? PercentComplete { get; set; }
        public int? PipelineStepID { get; set; }
        public bool ProgressChange { get; set; }
        //public List<object> Attachments { get; set; }
        //public object EffortEstimation { get; set; }
        //public object SubtaskID { get; set; }
        //public object Subtask { get; set; }
        //public object TaskItemTemplateID { get; set; }
        //public object TaskItemTemplate { get; set; }
    }
}
