using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskrowSharp
{
    public class TaskDetail
    {
        public int TaskID { get; set; }

        public int TaskNumber { get; set; }

        public string TaskTitle { get; set; }

        public int JobID { get; set; }

        public int JobNumber { get; set; }

        public string JobTitle { get; set; }

        public string MemberListString { get; set; }

        public List<TaskItem> TaskItems { get; set; }
        
        public List<TaskTag> Tags { get; set; }

        public string ClientNickName { get; set; }

        public string RowVersion { get; set; }

        public DateTime DueDate { get; set; }
        
        public List<SubTask> SubTasks { get; set; }
        
        public TaskDetail(int taskID, int taskNumber, string taskTitle, int jobID, int jobNumber, string jobTitle, string memberListString,
            List<TaskItem> taskItems, List<TaskTag> tags, string clientNickName, string rowVersion,
            DateTime dueDate, List<SubTask> subTasks)
        {
            this.TaskID = taskID;
            this.TaskNumber = taskNumber;
            this.TaskTitle = taskTitle;
            this.JobID = jobID;
            this.JobNumber = jobNumber;
            this.JobTitle = jobTitle;
            this.MemberListString = memberListString;
            this.TaskItems = taskItems;
            this.Tags = tags;
            this.ClientNickName = clientNickName;
            this.RowVersion = rowVersion;
            this.DueDate = dueDate;
            this.SubTasks = subTasks;
        }

        internal TaskDetail(ApiModels.TaskDataApi taskDataApi, ApiModels.JobDataApi jobDataApi)
        {
            this.TaskID = taskDataApi.TaskID;
            this.TaskNumber = taskDataApi.TaskNumber;
            this.TaskTitle = taskDataApi.TaskTitle;
            this.JobID = taskDataApi.JobID;
            this.JobNumber = jobDataApi.JobNumber;
            this.JobTitle = jobDataApi.JobTitle;
            this.MemberListString = taskDataApi.MemberListString;
            this.ClientNickName = jobDataApi.Client.ClientNickName;
            this.RowVersion = taskDataApi.RowVersion;
            this.DueDate = Utils.Parser.ToDateTimeFromTaskrowDate(taskDataApi.DueDate);

            int ownerUserID = 0;
            int pipelineStepID = 0;
            ApiModels.TaskItemApi taskItemApi;
            this.TaskItems = new List<TaskItem>();
            for (int i=0; i<taskDataApi.NewTaskItems.Count; i++)
            {
                taskItemApi = taskDataApi.NewTaskItems[i];

                if (i == 0)
                {
                    ownerUserID = taskItemApi.NewOwnerUserID.GetValueOrDefault();
                    pipelineStepID = taskItemApi.PipelineStepID.GetValueOrDefault();
                }

                bool ownerChanged = (taskItemApi.NewOwnerUserID != 0 && ownerUserID != taskItemApi.NewOwnerUserID && taskItemApi.NewOwnerUserID.GetValueOrDefault() != 0);
                if (ownerChanged)
                    ownerUserID = taskItemApi.NewOwnerUserID.GetValueOrDefault();

                bool pipelineChanged = (taskItemApi.PipelineStepID != 0 && pipelineStepID != taskItemApi.PipelineStepID);
                if (pipelineChanged)
                    pipelineStepID = taskItemApi.PipelineStepID.GetValueOrDefault();

                this.TaskItems.Add(new TaskItem(taskItemApi, ownerChanged, pipelineChanged));
            }

            if (taskDataApi.Tags != null)
                this.Tags = taskDataApi.Tags.Select(a => new TaskTag(a)).ToList();

            if (taskDataApi.Subtasks != null)
                this.SubTasks = taskDataApi.Subtasks.Select(a => new SubTask(a, jobDataApi)).ToList();
        }
    }
}
