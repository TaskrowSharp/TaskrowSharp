namespace TaskrowSharp.Models.InvoiceModels;

public class SaveInvoiceBillPaymentResponse
{
    public bool? Success { get; set; }
    public string Message { get; set; }
    public Invoice? InvoiceDetail { get; set; }
    public InvoiceBill2 InvoiceBill { get; set; }
    public bool AllowEditInvoice { get; set; }
}
