namespace TaskrowSharp.Models.OpportunityModels;

public class OpportunityTransferToClientRequest
{
    public string ClientNickName { get; set; }
    public int OpportunityID { get; set; }
    public string NewClientNickName { get; set; }

    public OpportunityTransferToClientRequest(string clientNickName, int opportunityID, string newClientNickName)
    {
        ClientNickName = clientNickName;
        OpportunityID = opportunityID;
        NewClientNickName = newClientNickName;
    }
}
