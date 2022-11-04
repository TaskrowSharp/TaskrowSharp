namespace TaskrowSharp.Models
{
    public class ExternalService
    {
        public int ExternalServiceID { get; set; }
        public string Title { get; set; }
        public string? Provider { get; set; }
        public string? Scope { get; set; }
        public string? Description { get; set; }
        public bool Inactive { get; set; }
    }
}
