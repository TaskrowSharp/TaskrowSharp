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
