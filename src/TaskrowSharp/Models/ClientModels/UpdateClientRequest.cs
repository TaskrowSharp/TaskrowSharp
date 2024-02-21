namespace TaskrowSharp.Models.ClientModels;

public class UpdateClientRequest
{
    public int ClientID { get; set; }
    public string ClientName { get; set; }
    public string DisplayName { get; set; }
    public string ExternalCode { get; set; }
    public int OwnerID { get; set; }

    //public bool NewClientAddress { get; set; }
    public string? SegmentListstring { get; set; }
    public string? MemberListstring { get; set; }
    public bool? JobRequiredProduct { get; set; }
    public bool? IsSupplier { get; set; }
    public bool? NotToSearchAsClient { get; set; }
    public string? ContactName { get; set; }
    public string? ContactMainPhone { get; set; }
    public string? ContactEmail { get; set; }
    public string? Location { get; set; }
    public int? CountryID { get; set; }
    public int? CityID { get; set; }
    public string? StateName { get; set; }
    public string? CityName { get; set; }
    public string? District { get; set; }
    public string? Street { get; set; }
    public string? Number { get; set; }
    public string? Complement { get; set; }
    public string? ZipCode { get; set; }
    public bool? NoCNPJ { get; set; }
    public string? CNPJ { get; set; }
    public string? CPF { get; set; }
    public string? SocialContractName { get; set; }
    public string? InscrEstad { get; set; }
    public string? InscrMunic { get; set; }
    public string? Memo { get; set; }

    public UpdateClientRequest(int clientID, string clientName, string displayName, int ownerID)
    {
        ClientID = clientID;
        ClientName = clientName;
        DisplayName = displayName;
        OwnerID = ownerID;
    }

    public UpdateClientRequest(Client client)
    {
        ClientID = client.ClientID;
        ClientName = client.ClientName;
        DisplayName = client.DisplayName;
        
        ExternalCode = client.ExternalCode;
        OwnerID = client.OwnerID;
        /*MemberListString = client.MemberListString;

        client.ClientAdministrativeDetail.SupplierComissionPercentage;
        client.ClientAdministrativeDetail.Memo;
        client.ExtranetRestrictedAccessTime*/

        //"SegmentListString": "",
        //"OwnerID": "5380",
        //"MemberListString": "5380,6429,6430,9986,9987,10232,10233,11785,13918,23461,24170,24886,28845",
        //"JobRequiredProduct": "false",
        //"IsSupplier": "false",
        //"NotToSearchAsClient": "false",
        //"ExtranetPipelineID": "1116",
        //"JobPipelineID": "173",
        //"DefaultPipelineID": "",
        //"DefaultContractID": "",
        //"SupplierComissionPercentage": "0.00",
        //"AdComissionPercentage": "0.00",
        //"AddComissionTax": "false",
        //"Memo": "",
        //"ExtranetRestrictedAccessTime": "false",
        //"ExtranetAccessInitialTime": "",
        //"ExtranetAccessEndTime": ""
    }
}
