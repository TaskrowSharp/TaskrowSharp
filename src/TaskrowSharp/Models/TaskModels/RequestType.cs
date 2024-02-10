namespace TaskrowSharp.Models.TaskModels;

public class RequestType
{
    public int RequestTypeID { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public string Acronym { get; set; }
    public bool Extranet { get; set; }
    public bool IsDefault { get; set; }
    public string ExternalCode { get; set; }
    public int AppMainCompanyID { get; set; }
    //public object? ColorID { get; set; }
    //public object? Content { get; set; }
    public bool Inactive { get; set; }
    public int? JobID { get; set; }
    public int? ClientID { get; set; }
    public string FunctionGroupListString { get; set; }
    public int Context { get; set; }
    //public object? FunctionGroupName { get; set; }
    //public object? DynFormHint { get; set; }
    //public object? DynFormMetadata { get; set; }
    //public List<object> RequestTypeFunctionGroup { get; set; }
    public bool SingleUse { get; set; }
    public bool InitialType { get; set; }
    public int RequestTypeClassificationID { get; set; }
    public string RequestTypeClassification { get; set; }
}
