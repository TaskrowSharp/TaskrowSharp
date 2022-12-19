namespace TaskrowSharp.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public string? InvoiceNumber { get; set; }
        public decimal InvoiceDeductionValue { get; set; }
        public string? InvoiceMemo { get; set; }
        public int ClientID { get; set; }
        public string ClientNickName { get; set; }
        public string ClientName { get; set; }
        public string ClientDisplayName { get; set; }
        public bool IsCancelled { get; set; }
        public bool IsDeleted { get; set; }
        public int IntegrationStatusID { get; set; }
        public string IntegrationStatus { get; set; }
    }
}
