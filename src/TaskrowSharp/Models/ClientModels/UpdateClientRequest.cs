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

        //NewClientAddress = client.NewClientAddress;
        //SegmentListstring = client.SegmentListstring;
        //MemberListstring = client.MemberListstring;
        //JobRequiredProduct = client.JobRequiredProduct;

        IsSupplier = client.IsSupplier;
        NotToSearchAsClient = client.NotToSearchAsClient;
        //ContactName = client.ContactName;
        //ContactMainPhone = client.ContactMainPhone;
        //ContactEmail = client.ContactEmail;
        //Location = client.Location;
        //CountryID = client.CountryID;
        //CityID = client.CityID;
        //StateName = client.StateName;
        //CityName = client.CityName;
        //District = client.District;
        //Street = client.Street;
        //Number = client.Number;
        //Complement = client.Complement;
        //ZipCode = client.ZipCode;
        //NoCNPJ = client.NoCNPJ;
        //CNPJ = client.CNPJ;
        //CPF = client.CPF;
        //SocialContractName = client.SocialContractName;
        //InscrEstad = client.InscrEstad;
        //InscrMunic = client.InscrMunic;

        //Memo = client.Memo;
}
}
