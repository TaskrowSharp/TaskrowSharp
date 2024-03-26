namespace TaskrowSharp.Models.JobModels;

public class JobReference
{
    public int JobID { get; private set; }
    public int JobNumber { get; private set; }
    public string JobTitle { get; private set; }

    public JobReference(int jobID, int jobNumber, string jobTitle)
    {
        JobID = jobID;
        JobNumber = jobNumber;
        JobTitle = jobTitle;
    }
}
