using System.Collections.Generic;

namespace TaskrowSharp.ApiModels
{
    internal class GroupListResponseApi
    {
        public List<GroupApi> Groups { get; set; }
        public List<ApproveTimesheetProfileApi> ApproveTimesheetProfiles { get; set; }
        public List<ApproveExpensesProfileApi> ApproveExpensesProfiles { get; set; }
        public List<ApproveJobsProfileApi> ApproveJobsProfiles { get; set; }
    }
}
