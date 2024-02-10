using System.Collections.Generic;

namespace TaskrowSharp.Models.ClientModels;

public class Client
{
    public int AppMainCompanyID { get; set; }
    public int ClientID { get; set; }
    public int ClientNumber { get; set; }
    public string ClientName { get; set; }
    public string DisplayName { get; set; }
    public string ClientNickName { get; set; }
    public string MemberListString { get; set; }
    public string MemberInfoListString { get; set; }
    public string SegmentListString { get; set; }
    public int OwnerID { get; set; }
    public bool Inactive { get; set; }
    public string ExternalCode { get; set; }
    public bool JobRequiredProduct { get; set; }
    public int ClientColorID { get; set; }
    public string OwnerUserLogin { get; set; }
    public string OwnerUserHashCode { get; set; }
    public string UrlData { get; set; }
    public string EditUrlData { get; set; }
    public List<ClientMember> ClientMember { get; set; }
    public List<object> ClientSegment { get; set; }
    public List<ClientAddress> ClientAddress { get; set; }
    public ClientAdministrativeDetail ClientAdministrativeDetail { get; set; }
    public double? BonusOrderPercentage { get; set; }
    //public object? HasOpenedJobs { get; set; }
    public bool IsSupplier { get; set; }
    public bool NotToSearchAsClient { get; set; }
    public int? ExtranetPipelineID { get; set; }
    public bool ExtranetRestrictedAccessTime { get; set; }
    //public object? ExtranetAccessEndTime { get; set; }
    //public object? ExtranetAccessInitialTime { get; set; }
    public int? JobPipelineID { get; set; }
    //public object? DefaultPipelineID { get; set; }
    //public object? DefaultContractID { get; set; }
}
