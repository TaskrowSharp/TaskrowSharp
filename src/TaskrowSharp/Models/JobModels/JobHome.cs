namespace TaskrowSharp.Models.JobModels;

public class JobHome
{
    public Job Job { get; set; }
    public int JobStatus { get; set; }
    public JobPermissions Permissions { get; set; }
    public JobWall JobWall { get; set; }
    //public DeliverableModel DeliverableModel { get; set; }
    //public List<DeliverableTypeList> DeliverableTypeList { get; set; }
    //public List<DeliverableStatusList> DeliverableStatusList { get; set; }
    public JobPipeline JobPipeline { get; set; }
    public bool JobHasOpenTasks { get; set; }
}
