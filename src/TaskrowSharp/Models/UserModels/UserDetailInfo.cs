using System.Collections.Generic;

namespace TaskrowSharp.Models.UserModels;

public class UserDetailInfo
{
    public int AppMainCompanyID { get; set; }
    public int AppMainLanguageID { get; set; }
    public int UserID { get; set; }
    public string FullName { get; set; }
    public string MainEmail { get; set; }
    public string Extension { get; set; }
    public string MainPhoneNumber { get; set; }
    public string MainCellNumber { get; set; }
    public string UserLogin { get; set; }
    public string UserHashCode { get; set; }
    public bool Inactive { get; set; }
    public int ProfileID { get; set; }
    public string ExternalCode { get; set; }
    public UserNotificationSetting UserNotificationSetting { get; set; }
    public bool ExternalUser { get; set; }
    public UserGroup UserGroup { get; set; }
    public string ProfileTitle { get; set; }
    public string LanguageTitle { get; set; }
    public UserDetail UserDetail { get; set; }
    public UserAdministrativeDetail UserAdministrativeDetail { get; set; }
    public List<object> UserBankingInfo { get; set; }
    public List<UserPeriod> UserPeriods { get; set; }
    public List<UserFunctionPeriod> UserFunctionPeriod { get; set; }
    public List<UserPeriodDetail> UserPeriod { get; set; }
    public List<object> UserExternalService { get; set; }
    public string ApprovalGroup { get; set; }
    //public object? AppMainCompanyMediaUser { get; set; }
    public bool SystemUser { get; set; }
}
