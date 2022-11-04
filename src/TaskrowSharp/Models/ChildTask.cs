namespace TaskrowSharp.Models
{
    public class ChildTask
    {
        public int TaskID { get; set; }
        public string RowVersion { get; set; }
        public int TaskNumber { get; set; }
        public int AbsoluteUserOrder { get; set; }
        public string TaskTitle { get; set; }
        public bool Closed { get; set; }
        public string UrlData { get; set; }
        public string DueDate { get; set; }
        public Owner Owner { get; set; }
        public int EffortEstimation { get; set; }
        public int RemainingEffortEstimation { get; set; }
    }
}
