using System.Collections.Generic;

namespace TaskrowSharp.Models.SupplierModels;

public class SupplierInvoiceResponse
{
    public SupplierInvoice SupplierInvoice { get; set; }
    public List<SupplierExpense> SupplierExpenses { get; set; } = [];
    public List<SupplierAddress> SupplierAddresses { get; set; } = [];
    public List<SupplierInvoiceType> SupplierInvoiceTypes { get; set; } = [];
    public List<CostCenter> CostCenters { get; set; } = [];
    public List<SupplierAddress> InternalClientAddresses { get; set; } = [];
    //public JobCompanyAddressList? JobCompanyAddressList { get; set; }
}
