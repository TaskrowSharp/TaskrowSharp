namespace TaskrowSharp.Models.OpportunityModels;

public class OpportunityInsertRequest
{
    public string ClientNickName { get; set; }
    public int ClientID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public OpportunityInsertRequest(string clientNickName, int clientID, string name, string description)
    {
        ClientNickName = clientNickName;
        ClientID = clientID;
        Name = name;
        Description = description;
    }
}
