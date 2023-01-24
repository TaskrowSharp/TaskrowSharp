namespace TaskrowSharp.Models
{
    public class Bank
    {
        public int BankID { get; set; }
        public int BankNumber { get; set; }
        public string Name { get; set; }
        public string? PortfolioCode { get; set; }
        public string? CovenantCode { get; set; }
        public string? FormattedName { get; set; }
    }
}
