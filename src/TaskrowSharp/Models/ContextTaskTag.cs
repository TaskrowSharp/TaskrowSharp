namespace TaskrowSharp.Models
{
    public class ContextTaskTag
    {
        public int TaskTagID { get; set; }
        public string? TagKey { get; set; }
        public int TaskID { get; set; }
        public int JobID { get; set; }
        public string? TagTitle { get; set; }
        //public object? TagColor { get; set; }
        public int CountTasks { get; set; }
        public string? TagGroup { get; set; }
        public string? DisplayName { get; set; }
        public int ContextID { get; set; }
        public string? Context { get; set; }
        public int ColorID { get; set; }
    }
}
