using System.Collections.Generic;
using TaskrowSharp.Models.ApproveModels;

namespace TaskrowSharp.Models.UserModels;

public class UserGroupListResponse
{
    public List<UserGroup> Groups { get; set; }
    public List<ApproveTimesheetProfile> ApproveTimesheetProfiles { get; set; }
    public List<ApproveExpensesProfile> ApproveExpensesProfiles { get; set; }
    public List<ApproveJobsProfile> ApproveJobsProfiles { get; set; }
}
