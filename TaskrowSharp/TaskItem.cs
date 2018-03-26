using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskrowSharp
{
    public class TaskItem
    {
        public int TaskItemID { get; set; }

        public int OldOwnerUserID { get; set; }

        public string OldOwnerName { get; set; }

        public int? NewOwnerUserID { get; set; }

        public string NewOwnerName { get; set; }

        public string TaskItemComment { get; set; }

        public bool OwnerChanged { get; set; }

        public bool PipelineChanged { get; set; }

        public TaskItem(int taskItemID, int oldOwnerUserID, string oldOwnerName,
            int? newOwnerUserID, string newOwnerName, string taskItemComment, 
            bool ownerChanged, bool pipelineChanged)
        {
            this.TaskItemID = taskItemID;
            this.OldOwnerUserID = oldOwnerUserID;
            this.OldOwnerName = oldOwnerName;
            this.NewOwnerUserID = newOwnerUserID;
            this.NewOwnerName = newOwnerName;
            this.TaskItemComment = taskItemComment;

            this.OwnerChanged = ownerChanged;
            this.PipelineChanged = pipelineChanged;
        }

        internal TaskItem(ApiModels.TaskItemApi taskItemApi, 
            bool ownerChanged, bool pipelineChanged)
        {
            this.TaskItemID = taskItemApi.TaskItemID;
            this.OldOwnerUserID = taskItemApi.OldOwnerUserID;
            this.OldOwnerName = taskItemApi.OldOwnerName;
            this.NewOwnerUserID = taskItemApi.NewOwnerUserID;
            this.NewOwnerName = taskItemApi.NewOwnerName;
            this.TaskItemComment = taskItemApi.TaskItemComment;

            this.OwnerChanged = ownerChanged;
            this.PipelineChanged = pipelineChanged;
        }
    }
}
