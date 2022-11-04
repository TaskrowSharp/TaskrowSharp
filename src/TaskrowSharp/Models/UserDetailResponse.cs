using System.Collections.Generic;

namespace TaskrowSharp.Models
{
    public class UserDetailResponse
    {
        public UserPermissions Permissions { get; set; }
        public UserDetailInfo User { get; set; }
        public List<ProfileList>? ProfileList { get; set; }
        public List<UserRelationType>? UserRelationType { get; set; }
        public List<UserFunctionGroup>? UserFunctionGroups { get; set; }
        public List<UserFunction>? UserFunctions { get; set; }
        public List<ExternalService>? ExternalServices { get; set; }
        public List<UserScholarity>? UserScholarity { get; set; }
        public List<CivilStatus>? CivilStatus { get; set; }
        public UserBankingModel? UserBankingModel { get; set; }
        public List<GroupApprovalList>? GroupApprovalList { get; set; }
        public List<AccessKey>? AccessKeys { get; set; }
        public List<OfficeList>? OfficeList { get; set; }
        public List<Language>? Languages { get; set; }
        public NotificationSettings? NotificationSettings { get; set; }
        public int ExternalUserDefaultProfile { get; set; }
        public int ExternalUserDefaultRelationType { get; set; }
        public string? AllNotificationTypesBitmask { get; set; }
        public List<object>? EthnicityList { get; set; }
        public List<GenderList>? GenderList { get; set; }
        public HierarchicalGroupLeader? HierarchicalGroupLeader { get; set; }
    }
}
