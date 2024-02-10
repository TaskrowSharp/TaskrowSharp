namespace TaskrowSharp.Models.ClientModels;

public class FromClientAddress
{
    public int ClientAddressID { get; set; }
    public string CNPJ { get; set; }
    public string SocialContractName { get; set; }
    public string Number { get; set; }
    public bool Inactive { get; set; }
    public string FormattedAddress { get; set; }
}
