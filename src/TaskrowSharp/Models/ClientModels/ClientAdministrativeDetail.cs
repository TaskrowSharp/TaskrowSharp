namespace TaskrowSharp.Models.ClientModels;

public class ClientAdministrativeDetail
{
    public int ClientAdministrativeDetailID { get; set; }
    public string Memo { get; set; }
    public decimal? AdComissionPercentage { get; set; }
    public decimal? SupplierComissionPercentage { get; set; }
    public decimal? BVPercentage { get; set; }
    public bool AddComissionTax { get; set; }
}
