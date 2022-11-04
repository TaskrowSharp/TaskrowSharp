namespace TaskrowSharp.Models
{
    public class GenderList
    {
        public int GenderID { get; set; }
        public string Name { get; set; }
        public string? ExternalCode { get; set; }
        public int AppMainCompanyID { get; set; }
        public string? ModificationDate { get; set; }
        public int ModificationUserID { get; set; }
        public bool Inactive { get; set; }
    }
}
