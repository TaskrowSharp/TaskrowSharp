namespace TaskrowSharp.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string? Name { get; set; }
        public int ClientID { get; set; }
        public int AppMainCompanyID { get; set; }
        public string? ExternalCode { get; set; }
        public bool Inactive { get; set; }
    }
}
