using System.Collections.Generic;

namespace TaskrowSharp.Models.InvoiceModels;

public class InvoiceAuthorizationSaveRequest
{
    public int JobNumber { get; set; }
    public int InvoiceID { get; set; }
    public string GuidModification { get; set; }
    public List<int> InvoiceFeeIDs { get; set; }

    public InvoiceAuthorizationSaveRequest(int jobNumber, int invoiceID, string guidModification, List<int> invoiceFeeIDs)
    {
        JobNumber = jobNumber;
        InvoiceID = invoiceID;
        GuidModification = guidModification;
        InvoiceFeeIDs = invoiceFeeIDs;
    }
}
