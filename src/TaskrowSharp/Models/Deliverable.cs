namespace TaskrowSharp.Models
{
    public class Deliverable
    {
        public int DeliverableID { get; set; }
        public string Name { get; set; }
		
		//[JsonConverter(typeof(DatetimeNullableTaskrowFormatJsonConverter))]
        //public DateTime? DueDate { get; set; }
    }
}
