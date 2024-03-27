namespace TaskrowSharp.Models.JobModels;

public class ListJobClientDependeciesResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public JobClientDependeciesEntity Entity { get; set; }
    public string TargetURL { get; set; }
}
