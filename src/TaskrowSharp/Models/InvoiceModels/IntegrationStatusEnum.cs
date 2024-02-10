namespace TaskrowSharp.Models.InvoiceModels;

public enum IntegrationStatusEnum
{
    None = 0,
    Unknown = 1,
    SentToIntegration = 2,
    Integrated = 3,
    Error = 4
}
