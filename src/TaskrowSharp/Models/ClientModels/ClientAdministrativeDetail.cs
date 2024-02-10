namespace TaskrowSharp.Models.ClientModels;

public class ClientAdministrativeDetail
{
    public int ClientAdministrativeDetailID { get; set; }
    public string Memo { get; set; }
    public double? AdComissionPercentage { get; set; }
    public double? SupplierComissionPercentage { get; set; }
    public double? BVPercentage { get; set; }
    public bool AddComissionTax { get; set; }
}
