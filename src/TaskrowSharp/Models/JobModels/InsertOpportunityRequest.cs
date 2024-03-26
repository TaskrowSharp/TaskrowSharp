namespace TaskrowSharp.Models.JobModels;

public class InsertOpportunityRequest
{
    public string ClientNickName { get; set; }
    public int ClientID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public InsertOpportunityRequest(string clientNickName, int clientID, string name, string description)
    {
        ClientNickName = clientNickName;
        ClientID = clientID;
        Name = name;
        Description = description;
    }
}
