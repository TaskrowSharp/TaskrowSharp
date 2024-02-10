namespace TaskrowSharp.Models.InvoiceModels;

public class SaveInvoiceAuthorizationResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public Invoice? Entity { get; set; }
    public string TargetURL { get; set; }
}
