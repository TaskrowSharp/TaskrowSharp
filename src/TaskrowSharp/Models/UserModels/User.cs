namespace TaskrowSharp.Models.UserModels;

public class User
{
    public int UserID { get; set; }
    public string FullName { get; set; }
    public string MainEmail { get; set; }
    public int PhotoVersion { get; set; }
    public string Extension { get; set; }
    public string MainPhoneNumber { get; set; }
    public string MainCellNumber { get; set; }
    public string UserLogin { get; set; }
    public string UserHashCode { get; set; }
    public int AppMainCompanyID { get; set; }
    public int AppMainLanguageID { get; set; }
    public bool Inactive { get; set; }
    public int ProfileID { get; set; }
    public string ExternalCode { get; set; }
    public bool ExternalUser { get; set; }
    public string ApprovalGroup { get; set; }
    public string ProfileTitle { get; set; }
    public int ProfileRate { get; set; }
    public string FunctionGroupName { get; set; }
    public string UserFunctionTitle { get; set; }
    public object NotBillable { get; set; }
    public object NotRequiredTimesheet { get; set; }
    public string RegistrationNumber { get; set; }
    public object Office { get; set; }
    public bool SystemUser { get; set; }
}
