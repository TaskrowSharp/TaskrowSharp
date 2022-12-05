using System;
using System.Text.Json.Serialization;
using TaskrowSharp.JsonConverters;

namespace TaskrowSharp.Models
{
    public class ClientContact
    {
        public int ClientContactID { get; set; }
        public string? ContactName { get; set; }
        public string? ContactMainPhone { get; set; }
        public string? ContactCellPhone { get; set; }
        public string? ContactEmail { get; set; }
        public string? OfficeArea { get; set; }
        //public object? ContactInfo { get; set; }
		
		[JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
        public DateTime? DateModification { get; set; }
		
        public int UserModification { get; set; }
        public string? FunctionName { get; set; }
        public int ClientID { get; set; }
        public int? ClientAddressID { get; set; }
        public int? BirthDay { get; set; }
        public int? BirthMonth { get; set; }
        public bool AddToMailling { get; set; }
        public bool Inactive { get; set; }
        public bool MainContact { get; set; }
        public bool IsMainContact { get; set; }
        public bool ExtranetEnabled { get; set; }
        public bool TermsAccepted { get; set; }
        public int AppMainCompanyID { get; set; }
        public string? ContactHashCode { get; set; }
        public bool FinancialDocument { get; set; }
        public bool IsFinancialDocument { get; set; }
        public bool ExtranetViewAllTasks { get; set; }
    }
}
