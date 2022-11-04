namespace TaskrowSharp.Models
{
    public class PipelineStep
    {
        public int PipelineID { get; set; }
        public int PipelineStepID { get; set; }
        public int Order { get; set; }
        public string Title { get; set; }
        public string PluralTitle { get; set; }
        public int PipelineStepClassID { get; set; }
        //public object? ColorID { get; set; }
    }
}
