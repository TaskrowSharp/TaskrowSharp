namespace TaskrowSharp.Models
{
    public class Subtask
    {
        public int TaskID { get; set; }
        public int TaskNumber { get; set; }
        public string? TaskTitle { get; set; }
        public bool Closed { get; set; }
    }
}
