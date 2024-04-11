using TaskrowSharp.Models.JobModels;

namespace TaskrowSharp.Models.TaskModels;

public class UpdateJobResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public Job Entity { get; set; }
    public string TargetURL { get; set; }
}
