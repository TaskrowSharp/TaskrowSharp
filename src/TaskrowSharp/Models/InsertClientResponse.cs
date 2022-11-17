namespace TaskrowSharp.Models
{
    public class InsertClientResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public Client Entity { get; set; }
        public string? TargetURL { get; set; }
        public string? PreviousGUID { get; set; }
        public string? UserTaskListGUID { get; set; }
    }
}
