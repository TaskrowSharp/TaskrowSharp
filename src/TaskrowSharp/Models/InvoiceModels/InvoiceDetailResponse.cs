namespace TaskrowSharp.Models.InvoiceModels;

public class InvoiceDetailResponse
{
    public bool Success { get; set; }

    public string Message { get; set; }

    public InvoiceDetailResponseEntity Entity { get; set; }

    public string TargetURL { get; set; }
}
