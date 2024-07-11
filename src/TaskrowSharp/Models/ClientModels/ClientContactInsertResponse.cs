namespace TaskrowSharp.Models.ClientModels;

public class ClientContactInsertResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public ClientContact Entity { get; set; }
    public string TargetURL { get; set; }
}
