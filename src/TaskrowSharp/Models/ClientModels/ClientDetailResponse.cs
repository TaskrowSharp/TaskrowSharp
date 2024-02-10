namespace TaskrowSharp.Models.ClientModels;

public class ClientDetailResponse
{
    public bool Success { get; set; }

    public string Message { get; set; }

    public ClientDetailEntity Entity { get; set; }

    public string TargetURL { get; set; }
}
