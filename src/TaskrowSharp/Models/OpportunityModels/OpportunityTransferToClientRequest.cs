namespace TaskrowSharp.Models.OpportunityModels;

public class OpportunityTransferToClientRequest
{
    public string ClientNickName { get; set; }
    public int OpportunityID { get; set; }
    public string NewClientNickname { get; set; }

    public OpportunityTransferToClientRequest(string clientNickName, int opportunityID, string newClientNickname)
    {
        ClientNickName = clientNickName;
        OpportunityID = opportunityID;
        NewClientNickname = newClientNickname;
    }
}
