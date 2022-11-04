namespace TaskrowSharp.Models
{
    public class JobPipelineStep
    {
        public int JobPipelineID { get; set; }
        public int JobPipelineStepID { get; set; }
        public int Order { get; set; }
        public string? Title { get; set; }
        public string? PluralTitle { get; set; }
        //public object? ColorID { get; set; }
        public int? JobPipelineStepClassID { get; set; }
        public string? LastModificationDate { get; set; }
        public int? LastModificationUserID { get; set; }
    }
}
