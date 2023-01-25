using System.Collections.Generic;

namespace TaskrowSharp.Models
{
    public class InvoiceServiceCode
    {
        public int InvoiceServiceCodeID { get; set; }

        public string Code { get; set; }

        public decimal Tax { get; set; }

        public string Description { get; set; }

        public bool Inactive { get; set; }

        public string? ClientAddressListString { get; set; }

        public List<ClientAddress>? ClientAddress { get; set; }
    }
}
