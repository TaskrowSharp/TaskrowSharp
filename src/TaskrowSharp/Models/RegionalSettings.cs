namespace TaskrowSharp.Models
{
    public class RegionalSettings
    {
        public int LanguageID { get; set; }
        public string? UICulture { get; set; }
        public string? ShortDateFormat { get; set; }
        public string? MomentShortDateFormat { get; set; }
        public string? ShortestDateFormat { get; set; }
        public string? MomentShortestDateFormat { get; set; }
        public string? WeekMonthDayFormat { get; set; }
        public string? MomentWeekMonthDayFormat { get; set; }
        public string? DecimalSeparator { get; set; }
        public string? ThousandSeparator { get; set; }
        public string? ShortTimePattern { get; set; }
        public string? MomentShortTimePattern { get; set; }
        public string? DateTimeFormat { get; set; }
        public string? MomentDateTimeFormat { get; set; }
        public string? CurrencySymbol { get; set; }
        public string? LongDatePattern { get; set; }
        public string? MomentLongDatePattern { get; set; }
    }
}
