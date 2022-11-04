namespace TaskrowSharp.Models
{
    public class Tag
    {
        public int TaskTagID { get; set; }
        public string TagKey { get; set; }
        public int TaskID { get; set; }
        public int JobID { get; set; }
        public string? TagTitle { get; set; }
        //public object? TagColor { get; set; }
        public int CountTasks { get; set; }
        //public object? TagGroup { get; set; }
        public string? DisplayName { get; set; }
        //public object? ContextID { get; set; }
        public string? Context { get; set; }
        public int ColorID { get; set; }
    }
}
