namespace TaskrowSharp.Models.SupplierModels;

public class Supplier
{
    public int ClientID { get; set; }
    public string DisplayName { get; set; }
    public string ClientNickName { get; set; }
    public string ClientName { get; set; }
    public int ClientNumber { get; set; }
    public bool Inactive { get; set; }
    public int? JobPipelineID { get; set; }
    public int? OwnerUserID { get; set; }
    public string UrlData { get; set; }
}