namespace TaskrowSharp.Models.TaskModels;

public class EffortUnitGroup
{
    public int EffortUnitGroupID { get; set; }
    public string Name { get; set; }
    public string ExternalCode { get; set; }
    public int AppMainCompanyID { get; set; }
    public bool Inactive { get; set; }
    public bool IsDefault { get; set; }
    public int EffortUnitGroupTypeID { get; set; }
    //public object? EffortUnitGroupType { get; set; }
}
