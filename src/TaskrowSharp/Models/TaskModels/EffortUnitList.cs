namespace TaskrowSharp.Models.TaskModels;

public class EffortUnitList
{
    public int EffortUnitID { get; set; }
    public string UnitName { get; set; }
    public int Effort { get; set; }
    public int AppMainCompanyID { get; set; }
    public bool Inactive { get; set; }
    public string FormatName { get; set; }
    public string UserFunctionListString { get; set; }
    //public List<object>? EffortUnitUserFunction { get; set; }
    //public object? ExternalCode { get; set; }
    public int? EffortUnitGroupID { get; set; }
    public EffortUnitGroup EffortUnitGroup { get; set; }
}
