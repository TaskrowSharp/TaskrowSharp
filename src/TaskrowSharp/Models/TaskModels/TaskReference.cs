namespace TaskrowSharp.Models.TaskModels;

public class TaskReference
{
    public string ClientNickname { get; set; }
    public int JobNumber { get; set; }
    public int TaskNumber { get; set; }

    public TaskReference(string clientNickName, int jobNumber, int taskNumber)
    {
        ClientNickname = clientNickName;
        JobNumber = jobNumber;
        TaskNumber = taskNumber;
    }

    public override string ToString()
    {
        return $"/{ClientNickname}/{JobNumber}/{TaskNumber}";
    }
}
