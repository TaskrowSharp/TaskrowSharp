using System;
using static TaskrowSharp.Utils.JsonUtils;
using System.Text.Json.Serialization;

namespace TaskrowSharp.Models
{
    public class SaveTaskRequest
    {
        public string ClientNickName { get; set; }
        public int JobNumber { get; set; }
        public int TaskNumber { get; set; }
        public int TaskID { get; set; }
        public int LastTaskItemID { get; set; }
        public string? MemberListString { get; set; }
        public string? RowVersion { get; set; }
        public string? TaskTitle { get; set; }
        public string? TaskItemComment { get; set; }
        public int? OwnerUserID { get; set; }
        public int? SpentTime { get; set; }

        [JsonConverter(typeof(DateTimeTaskrowFormatJsonConverter))]
        public DateTime? DueDate { get; set; }

        public int? PercentComplete { get; set; }
        public int? EffortEstimation { get; set; }
        public string? EffortUnitListString { get; set; }
        public string? TagListString { get; set; }
        public int? PipelineStepID { get; set; }
        public bool? Closed { get; set; }
        public int? ParentTaskNumber { get; set; }

        public SaveTaskRequest(
            string clientNickname,
            int jobNumber,
            int taskNumber,
            int taskID)
        {
            this.ClientNickName = clientNickname;
            this.TaskID = taskID;
            this.JobNumber = jobNumber;
            this.TaskNumber = taskNumber;
        }
    }
}
