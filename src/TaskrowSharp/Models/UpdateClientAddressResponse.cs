namespace TaskrowSharp.Models
{
    public class UpdateClientAddressResponse
    {
        public bool Success { get; set; }

        public string? Message { get; set; }

        public ClientAddress? Entity { get; set; }

        public string? TargetURL { get; set; }
    }
}
