using TaskrowSharp.Models.ClientModels;
using TaskrowSharp.Models.UserModels;

namespace TaskrowSharp.Models.JobModels;

public class Opportunity
{
    public int OpportunityID { get; set; }
    public int OpportunityNumber { get; set; }
    public string Number { get; set; }
    public int OpportunityStatusID { get; set; }
    public string OpportunityStatus { get; set; }
    public string Name { get; set; }
    public int ClientID { get; set; }
    public Client Client { get; set; }
    public object ProductID { get; set; }
    public object Product { get; set; }
    public string Description { get; set; }
    public string CreationDate { get; set; }
    public int CreationUserID { get; set; }
    public User CreationUser { get; set; }
    public string LastModificationDate { get; set; }
    public int LastModificationUserID { get; set; }
    public User LastModificationUser { get; set; }
    public int AppMainCompanyID { get; set; }
    public string DisplayName { get; set; }
    //public List<object> Estimate { get; set; }
    //public List<object> Proposal { get; set; }
    public bool Canceled { get; set; }
}
