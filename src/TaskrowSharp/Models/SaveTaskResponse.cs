namespace TaskrowSharp.Models
{
    public class SaveTaskResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public TaskEntity Entity { get; set; }
        //public object? TargetURL { get; set; }
        public string? PreviousGUID { get; set; }
        public string? UserTaskListGUID { get; set; }
    }
}
