using System.Collections.Generic;

namespace TaskrowSharp.Models
{
    public class ParentTask
    {
        public int TaskID { get; set; }
        public string? RowVersion { get; set; }
        public int TaskNumber { get; set; }
        public string? TaskTitle { get; set; }
        public string? RequestTypeAcronym { get; set; }
        public string? DueDate { get; set; }
        public int PipelineStepID { get; set; }
        public string? LastModificationDate { get; set; }
        //public List<object> Tags { get; set; }
        public List<Subtask>? Subtasks { get; set; }
        public bool ExternalRequest { get; set; }
        public bool ExtranetCloseRequested { get; set; }
        public bool ExtranetClosed { get; set; }
        //public object? Deliverable { get; set; }
        //public object? RecurringAllocation { get; set; }
    }
}
