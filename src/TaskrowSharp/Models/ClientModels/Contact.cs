namespace TaskrowSharp.Models.ClientModels;

public class Contact
{
    public int ClientContactID { get; set; }
    public string ContactName { get; set; }
    public string ContactHashCode { get; set; }
    public string ContactMainPhone { get; set; }
    public string ContactCellPhone { get; set; }
    public string ContactEmail { get; set; }
    public string OfficeArea { get; set; }
    public bool ExtranetEnabled { get; set; }
    public bool TermsAccepted { get; set; }
}
