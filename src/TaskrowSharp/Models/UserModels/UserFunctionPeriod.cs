namespace TaskrowSharp.Models.UserModels;

public class UserFunctionPeriod
{
    public int UserFunctionPeriodID { get; set; }
    public string DateStart { get; set; }
    public string DateEnd { get; set; }
    public int UserFunctionID { get; set; }
    public string UserFunctionTitle { get; set; }
    public int FunctionGroupID { get; set; }
    public string FunctionGroupName { get; set; }
    public string FullFunctionName { get; set; }
    public int OfficeID { get; set; }
    public Office Office { get; set; }
    //public object? RegistrationNumber { get; set; }
    public int UserRelationTypeID { get; set; }
    public UserRelationType UserRelationType { get; set; }
    //public object? FunctionRate { get; set; }
    //public object? FunctionMargin { get; set; }
    //public object? NetValue { get; set; }
    //public object? GrossValue { get; set; }
    //public object? UserTerminationTypeID { get; set; }
    public string UserTerminationType { get; set; }
    //public object? TerminationMemo { get; set; }
}
