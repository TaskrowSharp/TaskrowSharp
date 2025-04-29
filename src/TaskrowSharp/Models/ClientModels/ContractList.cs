namespace TaskrowSharp.Models.ClientModels;

public class ContractList
{
    public int ContractID { get; set; }
    public string ContractTitle { get; set; }
    public int? ContractNumber { get; set; }
    public string ContractDisplayTitle { get; set; }
    public bool IsStandard { get; set; }
    public bool Inactive { get; set; }
    public int ClientID { get; set; }
    public int? ClientContractTypeID { get; set; }
    public string ContractType { get; set; }
    //public List<object>? ContractPeriod { get; set; } = [];
    public double? ContractValueTotal { get; set; }
    //public object? Job { get; set; }
    //public object? Client { get; set; }
    //public object? AlertStatusReference { get; set; }
    //public object? AlertStatusRequired { get; set; }
    //public object? EndDay { get; set; }
    //public object? OwnerUserID { get; set; }
    public bool NotImpactDemand { get; set; }
    //public object? OwnerUser { get; set; }
}
