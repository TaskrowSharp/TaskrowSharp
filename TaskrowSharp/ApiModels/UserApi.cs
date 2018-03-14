using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp.ApiModels
{
    internal class UserApi
    {
        public int AppMainCompanyID { get; set; }
        public int AppMainLanguageID { get; set; }
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string MainEmail { get; set; }
        public object Extension { get; set; }
        public object MainPhoneNumber { get; set; }
        public object MainCellNumber { get; set; }
        public string UserLogin { get; set; }
        public string UserHashCode { get; set; }
        public bool Inactive { get; set; }
        public int ProfileID { get; set; }
        public object ExternalCode { get; set; }
        public UserNotificationSettingApi UserNotificationSetting { get; set; }
        public bool ExternalUser { get; set; }
        public UserGroupApi UserGroup { get; set; }
        public string ProfileTitle { get; set; }
        public UserDetailApi UserDetail { get; set; }
        public UserAdministrativeDetailApi UserAdministrativeDetail { get; set; }
        public List<object> UserBankingInfo { get; set; }
        public List<UserPeriodApi> UserPeriods { get; set; }
        public List<UserFunctionPeriodApi> UserFunctionPeriod { get; set; }
        public List<UserPeriodApi> UserPeriod { get; set; }
        public List<object> UserExternalService { get; set; }
        public string ApprovalGroup { get; set; }
        public object AppMainCompanyMediaUser { get; set; }
    }
}
