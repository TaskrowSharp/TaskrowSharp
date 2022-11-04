namespace TaskrowSharp.Models
{
    public class JobList
    {
        public int JobID { get; set; }
        public int JobNumber { get; set; }
        public string JobTitle { get; set; }
        public string TypeName { get; set; }
        public string? UrlData { get; set; }
        public Client? Client { get; set; }
    }
}
