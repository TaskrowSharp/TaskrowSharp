namespace TaskrowSharp.Models.InvoiceModels;

public class CancelInvoiceRequest
{
    public int InvoiceID { get; set; }
    public string Memo { get; set; }
    public string GuidModification { get; set; }

    public CancelInvoiceRequest(int invoiceID, string memo, string guidModification)
    {
        InvoiceID = invoiceID;
        Memo = memo;
        GuidModification = guidModification;
    }
}
