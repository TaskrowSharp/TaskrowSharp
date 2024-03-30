namespace TaskrowSharp.Models.JobModels;

public class SaveJobWallPostRequest
{
    public int WallID { get; set; }
    public int JobNumber { get; set; }
    public string Text { get; set; }

    public SaveJobWallPostRequest(int wallID, int jobNumber, string text)
    {
        WallID = wallID;
        JobNumber = jobNumber;
        Text = text;
    }
}
