namespace TaskrowSharp.Models
{
    public class SiblingTask
    {
        public int TaskID { get; set; }
        public int TaskNumber { get; set; }
        public string? RowVersion { get; set; }
        public string? TaskTitle { get; set; }
        public int OwnerUserID { get; set; }
    }
}
