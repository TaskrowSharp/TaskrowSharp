using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskrowSharp
{
    public class SubTask
    {
        public int SubTaskID { get; set; }

        public int TaskID { get; set; }

        public int? ChildTaskID { get; set; }

        public string Title { get; set; }

        public TaskDetail ChildTask { get; set; }

        public SubTask(int subTaskID, int taskID, int? childTaskID, string title, TaskDetail childTask)
        {
            this.SubTaskID = subTaskID;
            this.TaskID = taskID;
            this.ChildTaskID = childTaskID;
            this.Title = title;
            this.ChildTask = childTask;
        }

        internal SubTask(ApiModels.SubTaskApi subTaskApi, ApiModels.JobDataApi jobDataApi)
        {
            this.SubTaskID = subTaskApi.SubTaskID;
            this.TaskID = subTaskApi.TaskID;
            this.ChildTaskID = subTaskApi.ChildTaskID;
            this.Title = subTaskApi.Title;

            if (subTaskApi.ChildTask != null)
                this.ChildTask = new TaskDetail(subTaskApi.ChildTask, jobDataApi);
        }
    }
}
