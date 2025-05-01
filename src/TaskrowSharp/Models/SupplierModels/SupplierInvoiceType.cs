namespace TaskrowSharp.Models.SupplierModels;

public class SupplierInvoiceType
{
    public int SupplierInvoiceTypeID { get; set; } = 0;
    public string Name { get; set; }
    public bool Inactive { get; set; }
    public bool EnforceNumberUniqueness { get; set; }
    public int ModificationUserID { get; set; } = 0;
    public string ModificationDate { get; set; }
}
