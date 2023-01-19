namespace TaskrowSharp.Models
{
    public class InsertClientAddressResponse
    {
        public bool Success { get; set; }

        public string? Message { get; set; }
        
        public ClientAddress? Entity { get; set; }
        
        public string? TargetURL { get; set; }
    }
}
