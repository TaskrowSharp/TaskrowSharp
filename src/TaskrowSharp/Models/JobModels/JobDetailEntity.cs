using TaskrowSharp.Models.ClientModels;

namespace TaskrowSharp.Models.JobModels;

public class JobDetailEntity
{
    //public List<object> JobApprovalList { get; set; }
    //public List<object> PendingApprovalList { get; set; }
    //public List<object> ClientJobTags { get; set; }
    public Job Job { get; set; }
    public Client Client { get; set; }
    //public List<UsersWithExtranetPermission> UsersWithExtranetPermission { get; set; }
    //public List<object> ProductList { get; set; }
    public bool JobRequiredApproval { get; set; }
    //public List<ClientAreaList> ClientAreaList { get; set; }
    //public List<JobSubTypeList> JobSubTypeList { get; set; }
    //public List<CompanyAddressList> CompanyAddressList { get; set; }
}
