namespace TaskrowSharp.Models.UserModels;

public class UserFunction
{
    public int UserFunctionID { get; set; }
    public string UserFunctionTitle { get; set; }
    //public object? DailyMinutes { get; set; }
    public bool NotBillable { get; set; }
    public bool NotRequiredTimesheet { get; set; }
    public int FunctionGroupID { get; set; }
    public string GroupName { get; set; }
    public string FullFunctionName { get; set; }
    public int Order { get; set; }
    public int UtilizationRateTarget { get; set; }
    public bool RequiredPitstop { get; set; }
}
