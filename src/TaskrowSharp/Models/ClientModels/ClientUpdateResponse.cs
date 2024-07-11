namespace TaskrowSharp.Models.ClientModels;

public class ClientUpdateResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public Client Entity { get; set; }
    public string TargetURL { get; set; }
    public string PreviousGUID { get; set; }
    public string UserTaskListGUID { get; set; }
}
