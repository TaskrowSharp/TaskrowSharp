using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp.ApiModels
{
    internal class UserGetApiResponse
    {
        public PermissionsApi Permissions { get; set; }
        public UserApi User { get; set; }
        public List<ProfileListApi> ProfileList { get; set; }
        public List<UserRelationTypeApi> UserRelationType { get; set; }
        public List<UserFunctionGroupApi> UserFunctionGroups { get; set; }
        public List<UserFunctionApi> UserFunctions { get; set; }
        public List<ExternalServiceApi> ExternalServices { get; set; }
        public List<UserScholarityApi> UserScholarity { get; set; }
        public List<GenderApi> Gender { get; set; }
        public List<CivilStatusApi> CivilStatus { get; set; }
        public UserBankingModelApi UserBankingModel { get; set; }
        public List<GroupApprovalListApi> GroupApprovalList { get; set; }
        public List<AccessKeyApi> AccessKeys { get; set; }
        public List<OfficeApi> OfficeList { get; set; }
        public NotificationSettingsApi NotificationSettings { get; set; }
        public int ExternalUserDefaultProfile { get; set; }
        public int ExternalUserDefaultRelationType { get; set; }
        public string AllNotificationTypesBitmask { get; set; }
    }
}
