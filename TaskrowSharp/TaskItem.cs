using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskrowSharp
{
    public class TaskItem
    {
        public int TaskItemID { get; set; }

        public Owner Owner { get; set; }

        public Owner OwnerFrom { get; set; }

        public Owner OwnerTo { get; set; }

        public string TaskItemComment { get; set; }

        public bool OwnerChanged { get; set; }

        public bool PipelineChanged { get; set; }

        /*public TaskItem(int taskItemID, int oldOwnerUserID, string oldOwnerName,
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
        }*/

        internal TaskItem(ApiModels.TaskItemApi taskItemApi, 
            bool ownerChanged, bool pipelineChanged)
        {
            this.TaskItemID = taskItemApi.TaskItemID;

            if (!ownerChanged)
                this.Owner = new Owner(taskItemApi.OldOwnerUserID, taskItemApi.OldOwnerName, taskItemApi.OldOwnerHashCode);
            else
            {
                this.Owner = new Owner(taskItemApi.NewOwnerUserID.Value, taskItemApi.NewOwnerName, taskItemApi.NewOwnerHashCode);

                this.OwnerFrom = new Owner(taskItemApi.OldOwnerUserID, taskItemApi.OldOwnerName, taskItemApi.OldOwnerHashCode);
                this.OwnerTo = new Owner(taskItemApi.NewOwnerUserID.Value, taskItemApi.NewOwnerName, taskItemApi.NewOwnerHashCode);
            }

            this.TaskItemComment = taskItemApi.TaskItemComment;

            this.OwnerChanged = ownerChanged;
            this.PipelineChanged = pipelineChanged;
        }
    }
}
