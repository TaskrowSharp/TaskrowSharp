using System.Collections.Generic;

namespace TaskrowSharp.Models
{
    public class ListGroupResponse
    {
        public List<Group> Groups { get; set; }
        public List<ApproveTimesheetProfile>? ApproveTimesheetProfiles { get; set; }
        public List<ApproveExpensesProfile>? ApproveExpensesProfiles { get; set; }
        public List<ApproveJobsProfile>? ApproveJobsProfiles { get; set; }
    }
}
