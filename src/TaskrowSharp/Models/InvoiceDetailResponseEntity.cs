using System.Collections.Generic;

namespace TaskrowSharp.Models
{
    public class InvoiceDetailResponseEntity
    {
        public Invoice InvoiceDetail { get; set; }
        
        public bool AllowEditInvoice { get; set; }
        
        public List<ClientAddress> ClientAddressList { get; set; }
        
        //public List<InternalClientAddressList> InternalClientAddressList { get; set; }
        
        public List<InvoiceBillType> InvoiceBillTypeList { get; set; }
        
        public List<FinancialAccount> FinancialAccountList { get; set; }
        
        public List<InvoiceServiceCode> InvoiceServiceCodeList { get; set; }

        //public List<object> InvoiceTaxValueList { get; set; }

        public InvoicePermissions Permissions { get; set; }
    }
}
