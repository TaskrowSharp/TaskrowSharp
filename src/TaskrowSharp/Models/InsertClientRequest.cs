namespace TaskrowSharp.Models
{
    public class InsertClientRequest
    {
        public bool NewClientAddress { get; set; }
        public string ClientName { get; set; }
        public string DisplayName { get; set; }
        public int ClientID { get; set; }
        public string? ExternalCode { get; set; }
        public string? SegmentListString { get; set; }
        public int OwnerID { get; set; }
        public string? MemberListString { get; set; }
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
        public bool NoCNPJ { get; set; }
        public string? CNPJ { get; set; }
        public string? CPF { get; set; }
        public string? SocialContractName { get; set; }
        public string? InscrEstad { get; set; }
        public string? InscrMunic { get; set; }
        public string? Memo { get; set; }

        public InsertClientRequest(string clientName, string displayName, int ownerID)
        {
            this.ClientName = clientName;
            this.DisplayName = displayName;
            this.OwnerID = ownerID;
        }
    }
}
