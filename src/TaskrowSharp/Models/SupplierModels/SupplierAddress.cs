using TaskrowSharp.Models.BasicDataModels;

namespace TaskrowSharp.Models.SupplierModels;

public class SupplierAddress
{
    public int ClientAddressID { get; set; }
    public int ClientID { get; set; }
    public string SocialContractName { get; set; }
    public bool NoCNPJ { get; set; }
    public string CNPJ { get; set; }
    public string CPF { get; set; }
    public bool FlagMain { get; set; }
    public string FormattedAddress { get; set; }
    public string FormattedLocality { get; set; }
    public string FormattedSocialName { get; set; }
    public City? City { get; set; }
}