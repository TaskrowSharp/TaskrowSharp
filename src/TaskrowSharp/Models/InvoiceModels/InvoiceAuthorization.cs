using System.Collections.Generic;

namespace TaskrowSharp.Models.InvoiceModels;

public class InvoiceAuthorization
{
    public List<InvoiceProject> InvoiceProject { get; set; }
    public List<InvoiceFee> InvoiceFee { get; set; }
    //public List<object> InvoiceAdIA { get; set; }
}
