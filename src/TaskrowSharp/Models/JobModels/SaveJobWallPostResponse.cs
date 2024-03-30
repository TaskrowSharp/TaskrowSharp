namespace TaskrowSharp.Models.JobModels;

public class SaveJobWallPostResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public JobWallPost Entity { get; set; }
    public string TargetURL { get; set; }
}
