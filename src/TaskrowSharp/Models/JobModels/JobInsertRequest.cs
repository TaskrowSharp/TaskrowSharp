namespace TaskrowSharp.Models.JobModels;

public class JobInsertRequest
{
    public int ClientID { get; set; }
    public string ClientNickName { get; set; }
    public string JobTitle { get; set; }
    public int OwnerUserID { get; set; }
    public int JobTypeID { get; set; }
    public bool RequiredProduct { get; set; }
    public bool Public { get; set; }
    public bool ExternalUserAccess { get; set; }
    public int HealthReference { get; set; }
    public bool IsPrivate { get; set; }
    public bool EffortRequired { get; set; }
    public bool LooseEntriesAllowed { get; set; }
    public bool DeliverableRequired { get; set; }
    public int RequestDeliveryEnforceabilityID { get; set; }
    public int? ClientAreaID { get; set; }
    public int? JobSubTypeID { get; set; }

    public int? PipelineID { get; set; }
    public int? JobTemplateID { get; set; }

    //Minimum Required
    public JobInsertRequest(
        int clientID,
        string clientNickName,
        string jobTitle,
        int ownerUserID,
        int jobTypeID,
        bool requiredProduct,
        bool isPublic,
        bool externalUserAccess,
        int healthReference,
        bool isPrivate,
        bool effortRequired,
        bool looseEntriesAllowed,
        bool deliverableRequired,
        int requestDeliveryEnforceabilityID,
        int? clientAreaID,
        int? jobSubTypeID)
    {
        ClientID = clientID;
        ClientNickName = clientNickName;
        JobTitle = jobTitle;
        OwnerUserID = ownerUserID;
        JobTypeID = jobTypeID;
        RequiredProduct = requiredProduct;
        Public = isPublic;
        ExternalUserAccess = externalUserAccess;
        HealthReference = healthReference;
        IsPrivate = isPrivate;
        EffortRequired = effortRequired;
        LooseEntriesAllowed = looseEntriesAllowed;
        DeliverableRequired = deliverableRequired;
        RequestDeliveryEnforceabilityID = requestDeliveryEnforceabilityID;
        ClientAreaID = clientAreaID;
        JobSubTypeID = jobSubTypeID;
    }

    public JobInsertRequest(
        int clientID,
        string clientNickName,
        string jobTitle,
        int ownerUserID,
        int jobTypeID,
        bool requiredProduct,
        bool isPublic,
        bool externalUserAccess,
        int healthReference,
        bool isPrivate,
        bool effortRequired,
        bool looseEntriesAllowed,
        bool deliverableRequired,
        int requestDeliveryEnforceabilityID,
        int? clientAreaID,
        int? jobSubTypeID,

        int? pipelineID,
        int? jobTemplateID)
    {
        ClientID = clientID;
        ClientNickName = clientNickName;
        JobTitle = jobTitle;
        OwnerUserID = ownerUserID;
        JobTypeID = jobTypeID;
        RequiredProduct = requiredProduct;
        Public = isPublic;
        ExternalUserAccess = externalUserAccess;
        HealthReference = healthReference;
        IsPrivate = isPrivate;
        EffortRequired = effortRequired;
        LooseEntriesAllowed = looseEntriesAllowed;
        DeliverableRequired = deliverableRequired;
        RequestDeliveryEnforceabilityID = requestDeliveryEnforceabilityID;
        ClientAreaID = clientAreaID;
        JobSubTypeID = jobSubTypeID;

        PipelineID = pipelineID;
        JobTemplateID = jobTemplateID;
    }
}
