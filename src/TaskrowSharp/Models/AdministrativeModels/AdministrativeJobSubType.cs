namespace TaskrowSharp.Models.AdministrativeModels;

public class AdministrativeJobSubType
{
    public int JobSubTypeID { get; set; }
    public string Name { get; set; }
    public int AppMainCompanyID { get; set; }
    public string ExternalCode { get; set; }
    public bool IsDefault { get; set; }
    public bool Inactive { get; set; }
}
