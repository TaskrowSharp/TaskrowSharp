namespace TaskrowSharp.Models.JobModels;

public class JobStatusUpdateResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public UpdateJobStatusEntity Entity { get; set; }
    public string TargetURL { get; set; }

    public class UpdateJobStatusEntity 
    {
        public int JobID { get; set; }
        public int JobStatusID { get; set; }
        public string JobStatus { get; set; }
    }
}
