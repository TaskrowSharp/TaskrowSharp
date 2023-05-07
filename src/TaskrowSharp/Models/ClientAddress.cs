namespace TaskrowSharp.Models
{
    public class ClientAddress
    {
        public int ClientAddressID { get; set; }
        public int ClientID { get; set; }
        public Client Client { get; set; }
        public bool NoCNPJ { get; set; }
        public Country? Country { get; set; }
        public string? CNPJ { get; set; }
        public string? CPF { get; set; }
        public string? SocialContractName { get; set; }
        public string? InscrEstad { get; set; }
        public string? InscrMunic { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }
        public string? District { get; set; }
        public string? Complement { get; set; }
        public int? CityID { get; set; }
        public City? City { get; set; }
        public string? CityName { get; set; }
        public string? StateName { get; set; }
        public string? ZipCode { get; set; }
        public bool CanDelete { get; set; }
        public string? Location { get; set; }
        public bool FlagMain { get; set; }
        public bool Inactive { get; set; }
        public string? ExternalCode { get; set; }
        public string? FormattedAddress { get; set; }
        public string? FormattedLocality { get; set; }
        public string? FormattedSocialName { get; set; }
        //public List<object> Product { get; set; }
        public bool Complete { get; set; }
        public string? ProductListString { get; set; }
        public string? ProductListNames { get; set; }
    }
}
