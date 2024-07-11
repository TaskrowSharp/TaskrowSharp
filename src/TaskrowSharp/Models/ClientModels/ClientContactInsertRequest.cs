namespace TaskrowSharp.Models.ClientModels;

public class ClientContactInsertRequest
{
    public int ClientContactID { get; set; }
    public int ClientID { get; set; }
    public string ClientNickName { get; set; }
    public int? ClientAddressID { get; set; }

    public string ContactName { get; set; }
    public string ContactEmail { get; set; }
    public string ContactMainPhone { get; set; }
    public string ContactCellPhone { get; set; }
    public string OfficeArea { get; set; }
    public string FunctionName { get; set; }
    public int? BirthDay { get; set; }
    public int? BirthMonth { get; set; }
    public string ContactInfo { get; set; }

    public bool IsMainContact { get; set; }
    public bool IsFinancialDocument { get; set; }
    public bool Inactive { get; set; }
    public bool AddToMailling { get; set; }

    public ClientContactInsertRequest(int clientID, string clientNickName, string contactName)
    {
        ClientID = clientID;
        ClientNickName = clientNickName;
        ContactName = contactName;
    }
}
