namespace TaskrowSharp.Models.BasicDataModels
{
    public class City
    {
        public int CityID { get; set; }
        public int CityCode { get; set; }
        public string Name { get; set; }
        public string FormattedName { get; set; }
        public string UF { get; set; }
        public bool Inactive { get; set; }
    }
}
