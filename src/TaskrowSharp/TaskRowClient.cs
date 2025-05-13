using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web;
using TaskrowSharp.EventHandlers;
using TaskrowSharp.Exceptions;
using TaskrowSharp.Interfaces;
using TaskrowSharp.Models;
using TaskrowSharp.Models.AdministrativeModels;
using TaskrowSharp.Models.BasicDataModels;
using TaskrowSharp.Models.ClientModels;
using TaskrowSharp.Models.IndexDataModels;
using TaskrowSharp.Models.Integration;
using TaskrowSharp.Models.InvoiceModels;
using TaskrowSharp.Models.JobModels;
using TaskrowSharp.Models.OpportunityModels;
using TaskrowSharp.Models.SupplierModels;
using TaskrowSharp.Models.TaskModels;
using TaskrowSharp.Models.UserModels;

namespace TaskrowSharp;

public class TaskrowClient
{
    public string AccessKey { get; private set; }
    public Uri ServiceUrl { get; private set; }
    public string UserAgent { get; set; }
    public HttpClient HttpClient { get; private set; }

    #region Events

    public event ApiCallExecutedEventHandler? OnApiCallExecuted;

    #endregion

    #region Constructors

    public TaskrowClient(Uri serviceUrl, string accessKey, HttpClient httpClient)
    {
        ValidateServiceUrl(serviceUrl);
        ValidateAccessKey(accessKey);

        this.ServiceUrl = serviceUrl;
        this.AccessKey = accessKey;
        this.HttpClient = httpClient;

        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        this.UserAgent = $"TaskrowSharp v{Utils.GetAppVersion()}";
    }

    #endregion

    private readonly JsonSerializerOptions jsonSerializerOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    private static void ValidateAccessKey(string accessKey)
    {
        ArgumentNullException.ThrowIfNull(accessKey);

        if (accessKey.Length < 20)
            throw new ArgumentException("Invalid AccessKey");
    }

    private static void ValidateServiceUrl(Uri serviceUrl)
    {
        ArgumentNullException.ThrowIfNull(serviceUrl);

        if (serviceUrl.ToString().StartsWith("http://", StringComparison.CurrentCultureIgnoreCase))
            throw new TaskrowSharpInvalidServiceUrlException("https:// is required");
        
        if (!serviceUrl.ToString().StartsWith("https://", StringComparison.CurrentCultureIgnoreCase) || !serviceUrl.ToString().EndsWith(".taskrow.com/", StringComparison.CurrentCultureIgnoreCase))
            throw new TaskrowSharpInvalidServiceUrlException("Invalid service url, use the format: https://yourdomain.taskrow.com");
    }

    private async Task<T2> ExecuteApiCall<T1, T2>(HttpMethod httpMethod, Uri fullUrl, T1? request)
    {
        var jsonRequest = (request != null ? JsonSerializer.Serialize(request, jsonSerializerOptions) : null);

        try
        {
            var httpRequest = new HttpRequestMessage(httpMethod, fullUrl);
            httpRequest.Headers.Add("__identifier", this.AccessKey);

            if (jsonRequest != null)
            {
                var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpRequest.Content = requestContent;
            }

            var stopwatch = Stopwatch.StartNew();
            var httpResponse = await this.HttpClient.SendAsync(httpRequest);
            stopwatch.Stop();

            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();

            RegisterApiCallExecuted(httpMethod, fullUrl, httpResponse.StatusCode, httpResponse.IsSuccessStatusCode, jsonRequest, jsonResponse, stopwatch.ElapsedMilliseconds);

            if (httpMethod == HttpMethod.Get && httpResponse.StatusCode == HttpStatusCode.NotFound)
                return default;

            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowSharpWebException(httpResponse.StatusCode, $"Error statusCode: {(int)httpResponse.StatusCode}");

            var response = JsonSerializer.Deserialize<T2>(jsonResponse);

            if (response is IBaseApiResponse baseResponseParsed)
            {
                if (baseResponseParsed != null && !baseResponseParsed.Success)
                    throw new TaskrowSharpException(baseResponseParsed.Message ?? "Response.success=false");
            }

            return response;
        }
        catch (Exception ex)
        {
            throw new TaskrowSharpException($"Error in Taskrow API Call {fullUrl} -- {ex.Message}", ex);
        }
    }

    private async Task ExecuteApiCallWithNoReturn<T1>(HttpMethod httpMethod, Uri fullUrl, T1? request)
    {
        var jsonRequest = (request != null ? JsonSerializer.Serialize(request, jsonSerializerOptions) : null);

        try
        {
            var httpRequest = new HttpRequestMessage(httpMethod, fullUrl);
            httpRequest.Headers.Add("__identifier", this.AccessKey);

            if (jsonRequest != null)
            {
                var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpRequest.Content = requestContent;
            }

            var stopwatch = Stopwatch.StartNew();
            var httpResponse = await this.HttpClient.SendAsync(httpRequest);
            stopwatch.Stop();

            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();

            RegisterApiCallExecuted(httpMethod, fullUrl, httpResponse.StatusCode, httpResponse.IsSuccessStatusCode, jsonRequest, jsonResponse, stopwatch.ElapsedMilliseconds);

            if (httpMethod == HttpMethod.Get && httpResponse.StatusCode == HttpStatusCode.NotFound)
                return;

            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowSharpWebException(httpResponse.StatusCode, $"Error statusCode: {(int)httpResponse.StatusCode}");
        }
        catch (Exception ex)
        {
            throw new TaskrowSharpException($"Error in Taskrow API Call {fullUrl} -- {ex.Message}", ex);
        }
    }

    private void RegisterApiCallExecuted(HttpMethod httpMethod, Uri fullUrl, HttpStatusCode httpStatusCode, bool isSuccess, string? jsonRequest, string? jsonResponse, long elapsedMilliseconds)
    {
        if (isSuccess)
            Debug.WriteLine($"API Call - {httpMethod} {fullUrl} -- HttpStatus: {(int)httpStatusCode}");
        else
            Debug.WriteLine($"API Call ERROR - {httpMethod} {fullUrl} -- HttpStatus: {(int)httpStatusCode} -- Response: {jsonResponse}");

        OnApiCallExecuted?.Invoke(httpMethod, fullUrl, httpStatusCode, isSuccess, jsonRequest, jsonResponse, elapsedMilliseconds);
    }

    #region IndexData

    public async Task<IndexData> IndexDataGetAsync()
    {
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/Main/IndexData");
        var response = await ExecuteApiCall<object, IndexData>(HttpMethod.Get, fullUrl, null);
        return response;

    }

    #endregion

    #region Client

    public async Task<List<ClientItemSearch>> ClientSearchAsync(string term, bool showInactives = true)
    {        
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/Search/SearchClients?&q={term}&showInactives={showInactives}");
        var response = await ExecuteApiCall<object, List<ClientItemSearch>>(HttpMethod.Get, fullUrl, null);
        return response;
    }

    public async Task<ClientListItemResponse> ClientListAsync(string? nextToken = null, bool? includeInactives = null, bool convertNextTokenToNumeric = true)
    {
        string queryString = null;
        if (!string.IsNullOrWhiteSpace(nextToken))
            queryString = $"{queryString}{(queryString == null ? "?" : "&")}nextToken={nextToken}";
        if (includeInactives != null)
            queryString = $"{queryString}{(queryString == null ? "?" : "&")}includeInactives={includeInactives.ToString().ToLower()}";

        var fullUrl = new Uri(this.ServiceUrl, $"/api/v2/core/client{queryString}");
        var response = await ExecuteApiCall<object, ClientListItemResponse>(HttpMethod.Get, fullUrl, null);

        if (!string.IsNullOrEmpty(response.NextToken) && convertNextTokenToNumeric)
        {
            //Convert "nextToken": "WzYyNjEzXQ" em "62613"
            var newNextToken = Utils.ConvertFromBase64ToString(response.NextToken, true);
            newNextToken = Utils.GetOnlyNumbers(newNextToken);
            response.NextToken = newNextToken;
        }

        return response;
    }

    public async Task<Client?> ClientDetailGetAsync(int clientID)
    {
        try
        {
            var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/Client/ClientDetail?clientID={clientID}");
            var response = await ExecuteApiCall<object, ClientDetailResponse>(HttpMethod.Get, fullUrl, null);
            return response.Entity.Client;
        }
        catch (TaskrowSharpWebException twe)
        {
            //NOTE: API Taskrow retorna erro 500 quando não encontra o registro, deveria retornar erro 404
            if (twe.HttpStatusCode == HttpStatusCode.InternalServerError)
                return null;
            throw;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Client> ClientInsertAsync(ClientInsertRequest clientInsertRequest)
    {
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/Client/SaveClient");
        var response = await ExecuteApiCall<ClientInsertRequest, BaseApiResponse<Client>>(HttpMethod.Post, fullUrl, clientInsertRequest);
        return response.Entity;
    }

    public async Task<Client> ClientUpdateAsync(ClientUpdateRequest updateClientRequest)
    {
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/Client/SaveClient");
        var response = await ExecuteApiCall<ClientUpdateRequest, BaseApiResponse<Client>>(HttpMethod.Post, fullUrl, updateClientRequest);
        return response.Entity;
    }

    #endregion

    #region ClientAddress

    public async Task<ClientAddress> ClientAddressInsertAsync(ClientAddressInsertRequest clientAddressInsertRequest)
    {
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/Client/SaveClientAddress");
        var response = await ExecuteApiCall<ClientAddressInsertRequest, BaseApiResponse<ClientAddress>>(HttpMethod.Post, fullUrl, clientAddressInsertRequest);
        return response.Entity;
    }

    public async Task<ClientAddress> ClientAddressUpdateAsync(ClientAddressUpdateRequest clientAddressUpdateRequest)
    {
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/Client/SaveClientAddress");
        var response = await ExecuteApiCall<ClientAddressUpdateRequest, BaseApiResponse<ClientAddress>>(HttpMethod.Post, fullUrl, clientAddressUpdateRequest);
        return response.Entity;
    }

    #endregion

    #region ClientContact

    public async Task<List<ClientContact>> ClientContactListAsync(int clientID)
    {
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/Client/ListClientContacts?clientID={clientID}");
        var response = await ExecuteApiCall<object, List<ClientContact>>(HttpMethod.Get, fullUrl, null);
        return response;
    }

    public async Task<ClientContact> ClientContactInsertAsync(ClientContactInsertRequest clientContactInsertRequest)
    {
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/Client/SaveContact");
        var response = await ExecuteApiCall<ClientContactInsertRequest, BaseApiResponse<ClientContact>>(HttpMethod.Post, fullUrl, clientContactInsertRequest);
        return response.Entity;

    }

    #endregion

    #region User

    public async Task<List<User>> UserListAsync(bool showInactive = false)
    {
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/User/ListUsers?showInactive={showInactive.ToString().ToLower()}");
        var response = await ExecuteApiCall<object, List<User>>(HttpMethod.Get, fullUrl, null);
        return response;
    }

    public async Task<UserDetailInfo> UserDetailGetAsync(int userID)
    {
        try
        {
            var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/User/UserDetail?userID={userID}");
            var response = await ExecuteApiCall<object, UserDetailResponse>(HttpMethod.Get, fullUrl, null);
            return response.User;
        }
        catch (TaskrowSharpWebException twe)
        {
            //NOTE: API Taskrow retorna erro 500 quando não encontra o registro, deveria retornar erro 404
            if (twe.HttpStatusCode == HttpStatusCode.InternalServerError)
                return null;
            throw;
        }
        catch (Exception)
        {
            throw;
        }
    }

    #endregion

    #region Group

    public async Task<List<UserGroup>> UserGroupListAsync()
    {
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/Administrative/ListGroups?groupTypeID=2");
        var response = await ExecuteApiCall<object, UserGroupListResponse>(HttpMethod.Get, fullUrl, null);
        return response.Groups;
    }

    #endregion

    #region City

    public async Task<List<City>> CityListAsync(string stateAbbreviation)
    {
        if (string.IsNullOrWhiteSpace(stateAbbreviation)) 
            throw new ArgumentNullException(nameof(stateAbbreviation));

        var queryString = $"uf={HttpUtility.UrlEncode(stateAbbreviation)}";
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/Client/ListCities?{queryString}");
        var response = await ExecuteApiCall<object, List<City>>(HttpMethod.Get, fullUrl, null);
        return response;
    }

    public async Task<City?> CityGetByNameAsync(string stateAbbreviation, string cityName)
    {
        if (string.IsNullOrWhiteSpace(stateAbbreviation)) throw new ArgumentNullException(nameof(stateAbbreviation));
        if (string.IsNullOrWhiteSpace(cityName)) throw new ArgumentNullException(nameof(cityName));

        stateAbbreviation = Utils.RemoveDiacritics(stateAbbreviation).ToUpper();
        cityName = Utils.RemoveDiacritics(cityName).ToUpper();

        var queryString = $"uf={HttpUtility.UrlEncode(stateAbbreviation)}&name={HttpUtility.UrlEncode(cityName)}";
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/Client/ListCities?{queryString}");
        var list = await ExecuteApiCall<object, List<City>>(HttpMethod.Get, fullUrl, null);
        return list.FirstOrDefault();
    }

    #endregion

    #region Job

    public async Task<Job> JobDetailGetAsync(string clientNickName, int jobNumber)
    {
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/Job/JobDetail?clientNickName={clientNickName}&JobNumber={jobNumber}");
        var response = await ExecuteApiCall<object, JobDetailEntity>(HttpMethod.Get, fullUrl, null);

        //NOTE: This method from Taskrow API returns different types for success or error sittuation
        // When Success: { Job: {}, Client: {}: ... }
        // When Error:   { "Success": false, "Message": "Acesso negado", "Entity": null, "TargetURL": null }

        return response.Job;
    }

    public async Task<Job> JobInsertAsync(JobInsertRequest jobInsertRequest)
    {
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/Job/SaveJob");
        var response = await ExecuteApiCall<JobInsertRequest, BaseApiResponse<Job>>(HttpMethod.Post, fullUrl, jobInsertRequest);
        return response.Entity;
    }

    public async Task<Job> JobUpdateAsync(JobUpdateRequest jobUpdateRequest)
    {
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/Job/SaveJob");
        var response = await ExecuteApiCall<JobUpdateRequest, BaseApiResponse<Job>>(HttpMethod.Post, fullUrl, jobUpdateRequest);
        return response.Entity;
    }

    public async Task<JobStatusUpdateEntity> JobStatusUpdateAsync(string clientNickName, int jobNumber, int jobStatusID)
    {
        var fullUrl = new Uri(this.ServiceUrl, $"api/v1/Job/UpdateJobStatus?clientNickName={clientNickName}&jobNumber={jobNumber}&status={jobStatusID}");
        var response = await ExecuteApiCall<object, BaseApiResponse<JobStatusUpdateEntity>>(HttpMethod.Post, fullUrl, null);
        return response.Entity;
    }

    public async Task<JobPipelineStepUpdateEntity> JobPipelineStepUpdateAsync(string clientNickName, int jobNumber, int jobPipelineStepID)
    {
        var fullUrl = new Uri(this.ServiceUrl, $"api/v1/Job/UpdateJobPipelineStep?clientNickName={clientNickName}&jobNumber={jobNumber}&jobPipelineStepID={jobPipelineStepID}");
        var response = await ExecuteApiCall<object, BaseApiResponse<JobPipelineStepUpdateEntity>>(HttpMethod.Post, fullUrl, null);
        return response.Entity;
    }

    #endregion

    #region JobClientDependecies

    public async Task<JobClientDependeciesEntity> JobClientDependecyListAsync(int clientID)
    {
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/Job/ListJobClientDependecies?clientID={clientID}");
        var response = await ExecuteApiCall<object, BaseApiResponse<JobClientDependeciesEntity>>(HttpMethod.Get, fullUrl, null);
        return response.Entity;
    }

    #endregion

    #region JobHome + JobWall

    public async Task<JobHome> JobHomeGetAsync(string clientNickname, int jobNumber)
    {
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/Job/JobHome?clientNickName={clientNickname}&jobNumber={jobNumber}");
        var response = await ExecuteApiCall<object, JobHome>(HttpMethod.Get, fullUrl, null);
        return response;
    }

    public async Task<JobWallPost> JobWallPostSaveAsync(JobWallPostSaveRequest jobWallPostSaveRequest)
    {
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/Wall/SaveWallPost");
        var response = await ExecuteApiCall<JobWallPostSaveRequest, BaseApiResponse<JobWallPost>>(HttpMethod.Post, fullUrl, jobWallPostSaveRequest);
        return response.Entity;
    }

    #endregion

    #region Task

    public async Task<TaskListByGroupEntity> TaskListByGroupAsync(int groupID, int? userID = null)
    {
        if (groupID == 0)
            throw new ArgumentException(null, nameof(groupID));
        
        if (userID.HasValue && userID == 0)
            throw new ArgumentException(null, nameof(userID));
        
        Uri relativeUrl;
        if (!userID.HasValue)
            relativeUrl = new Uri($"/api/v1/Dashboard/TasksByGroup?groupID={groupID}&hierarchyEnabled=true&closedDays=20&context=1", UriKind.Relative);
        else
            relativeUrl = new Uri($"/api/v1/Dashboard/TasksByGroup?groupID={groupID}&userID={userID}&hierarchyEnabled=true&closedDays=20&context=1", UriKind.Relative);
        
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);
        var response = await ExecuteApiCall<object, BaseApiResponse<TaskListByGroupEntity>>(HttpMethod.Get, fullUrl, null);
        return response.Entity;
    }
            
    public async Task<TaskDetailResponse> TaskDetailGetAsync(TaskReference taskReference)
    {
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/Task/TaskDetail?clientNickname={taskReference.ClientNickname}&jobNumber={taskReference.JobNumber}&taskNumber={taskReference.TaskNumber}");
        var response = await ExecuteApiCall<object, TaskDetailResponse>(HttpMethod.Get, fullUrl, null);
        return response;
    }
    
    public async Task<TaskEntity> TaskSaveAsync(TaskSaveRequest taskSaveRequest)
    {
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/Task/SaveTask");
        var response = await ExecuteApiCall<TaskSaveRequest, BaseApiResponse<TaskEntity>>(HttpMethod.Post, fullUrl, taskSaveRequest);
        return response.Entity;
    }

    #endregion

    #region ExternalData

    public async Task<Dictionary<string, object?>> ExternalDataGetAsync(string provider, string entityName, int id)
    {
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v2/externaldata/{entityName.ToLower()}?provider={provider}&identification={id}");
        var response = await ExecuteApiCall<object, Dictionary<string, object?>> (HttpMethod.Get, fullUrl, null);
        return response;
    }

    public async Task<List<Dictionary<string, object?>>> ExternalDataSearchByFieldValueAsync(string provider, string entityName, string entityIdName,
        string fieldName, string fieldValue)
    {
        var fullUrl = new Uri(this.ServiceUrl, $"api/v2/externaldata/{entityName.ToLower()}/find?provider={provider}&fieldName={fieldName}&fieldValue={fieldValue}");
        var listSearch = await ExecuteApiCall<object, List<Dictionary<string, object?>>>(HttpMethod.Get, fullUrl, null);

        var listRet = new List<Dictionary<string, object?>>();

        foreach (var dicSearch in listSearch)
        {
            if (!dicSearch.ContainsKey(entityIdName))
                throw new InvalidOperationException($"Invalid entityIdName: \"{entityIdName}\"");

            var id = Convert.ToInt32(dicSearch[entityIdName].ToString());
            var dicRet = await ExternalDataGetAsync(provider, entityName, id);
            if (!dicRet.ContainsKey(entityIdName))
                dicRet.Add(entityIdName, id);

            listRet.Add(dicRet);
        }

        return listRet;
    }

    public async Task ExternalDataSaveAsync(string provider, string entityName, int id, Dictionary<string, object?> values)
    {
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v2/externaldata/{entityName.ToLower()}?provider={provider}&identification={id}");
        await ExecuteApiCall<Dictionary<string, object?>, object>(HttpMethod.Put, fullUrl, values);
    }

    #endregion

    #region InvoiceFee

    public async Task<List<InvoiceFee>> InvoiceFeeInsertAsync(InvoiceFeeInsertRequest invoiceInsertRequest)
    {
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/Invoice/SaveInvoiceFee");
        var response = await ExecuteApiCall<InvoiceFeeInsertRequest, BaseApiResponse<List<InvoiceFee>>>(HttpMethod.Post, fullUrl, invoiceInsertRequest);
        return response.Entity;
    }

    public async Task<InvoiceFee> InvoiceFeeDetailGetAsync(int jobNumber, int invoiceFeeID)
    {
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/Invoice/InvoiceFeeDetail?jobNumber={jobNumber}&invoiceFeeID={invoiceFeeID}");
        var response = await ExecuteApiCall<object, InvoiceFeeeDetailResponse>(HttpMethod.Get, fullUrl, null);
        return response.InvoiceFee;
    }

    #endregion

    #region Invoice

    public async Task<InvoiceDetailResponseEntity> InvoiceDetailGetAsync(int invoiceID)
    {
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/Invoice/GetInvoiceDetail?invoiceID={invoiceID}");
        var response = await ExecuteApiCall<object, BaseApiResponse<InvoiceDetailResponseEntity>>(HttpMethod.Get, fullUrl, null);
        return response.Entity;
    }

    public async Task<Invoice> InvoiceSaveAsync(InvoiceSaveRequest invoiceSaveRequest)
    {
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/Invoice/SaveInvoice");
        var response = await ExecuteApiCall<InvoiceSaveRequest, BaseApiResponse<Invoice>>(HttpMethod.Post, fullUrl, invoiceSaveRequest);
        return response.Entity;
    }

    public async Task<Invoice> InvoiceCancelAsync(InvoiceCancelRequest request)
    {
        var memoEncoded = (!string.IsNullOrEmpty(request.Memo) ? HttpUtility.UrlEncode(request.Memo) : null);
        var relativeUrl = new Uri($"/api/v1/Invoice/CancelInvoice?invoiceID={request.InvoiceID}&memo={memoEncoded}", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);
        var response = await ExecuteApiCall<object, BaseApiResponse<Invoice>> (HttpMethod.Get, fullUrl, null);
        return response.Entity;

    }

    public async Task<Invoice> InvoiceDeleteAsync(InvoiceDeleteRequest request)
    {
        var memoEncoded = (!string.IsNullOrEmpty(request.Memo) ? HttpUtility.UrlEncode(request.Memo) : null);
        var relativeUrl = new Uri($"/api/v1/Invoice/DeleteInvoice?invoiceID={request.InvoiceID}&memo={memoEncoded}", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);
        var response = await ExecuteApiCall<object, BaseApiResponse<Invoice>>(HttpMethod.Get, fullUrl, null);
        return response.Entity;
    }

    #endregion

    #region InvoiceStatus

    public async Task<Invoice> InvoiceStatusUpdateAsync(InvoiceStatusUpdateRequest invoiceUpdateRequest)
    {
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/Invoice/SaveInvoiceIntegrationStatus");
        var response = await ExecuteApiCall<InvoiceStatusUpdateRequest, BaseApiResponse<Invoice>>(HttpMethod.Post, fullUrl, invoiceUpdateRequest);
        return response.Entity;
    }

    #endregion

    #region InvoiceAuthorization

    public async Task<Invoice> InvoiceAuthorizationSaveAsync(InvoiceAuthorizationSaveRequest invoiceAuthorizationSaveRequest)
    {
        string parameters = $"jobNumber={invoiceAuthorizationSaveRequest.JobNumber}";
        parameters += $"&invoiceID={invoiceAuthorizationSaveRequest.InvoiceID}";
        parameters += $"&guidModification={invoiceAuthorizationSaveRequest.GuidModification}";
        for (int i=0; i< invoiceAuthorizationSaveRequest.InvoiceFeeIDs.Count; i++)
            parameters += $"&invoiceFeeIDs[{i}]={invoiceAuthorizationSaveRequest.InvoiceFeeIDs[i]}";

        var relativeUrl = new Uri($"/api/v1/Invoice/SaveInvoiceAuthorization?{parameters}", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);                
        var response = await ExecuteApiCall<InvoiceAuthorizationSaveRequest, BaseApiResponse<Invoice>>(HttpMethod.Post, fullUrl, invoiceAuthorizationSaveRequest);
        return response.Entity;
    }

    #endregion

    #region InvoiceBill

    public async Task<Invoice> InvoiceBillSaveAsync(InvoiceBillSaveRequest invoiceBillSaveRequest)
    {
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/Invoice/SaveInvoiceBill");
        var response = await ExecuteApiCall<InvoiceBillSaveRequest, BaseApiResponse<Invoice>>(HttpMethod.Post, fullUrl, invoiceBillSaveRequest);
        return response.Entity;
    }

    public async Task<Invoice> InvoiceBillCancelAsync(int invoiceBillID)
    {
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/Invoice/CancelInvoiceBill?invoiceBillID={invoiceBillID}");
        var response = await ExecuteApiCall<object, Invoice>(HttpMethod.Get, fullUrl, null);
        return response;
    }

    #endregion

    #region InvoiceBillPayment

    public async Task<InvoiceBillPaymentSaveResponse> InvoiceBillPaymentSaveAsync(InvoiceBillPaymentSaveRequest saveInvoiceBillRequest)
    {
        //NOTE: This method from Taskrow API returns different types for success or error sittuation
        // When Success: { InvoiceDetail: {}, InvoiceBill: {}: AllowEditInvoice: true }
        // When Error:   { "Success": false, "Message": "Cobrança de nota fiscal não encontrada", "Entity": null, "TargetURL": null }

        var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/Invoice/SaveInvoiceBillPayment");
        var response = await ExecuteApiCall<InvoiceBillPaymentSaveRequest, InvoiceBillPaymentSaveResponse>(HttpMethod.Post, fullUrl, saveInvoiceBillRequest);
        if (response.Success == null)
            response.Success = true;
        return response;
    }

    #endregion

    #region SupplierInvoice

    public async Task<SupplierInvoiceResponse> SupplierInvoiceGetAsync(int supplierInvoiceID)
    {
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/SupplierInvoice/GetSupplierInvoiceData?supplierInvoiceID={supplierInvoiceID}");
        var response = await ExecuteApiCall<object, SupplierInvoiceResponse>(HttpMethod.Get, fullUrl, null);
        return response;
    }

    #endregion

    #region Opportunity

    public async Task<Opportunity> OpportunityInsertAsync(OpportunityInsertRequest opportunityInsertRequest)
    {
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/Opportunity/SaveOpportunity");
        var response = await ExecuteApiCall<OpportunityInsertRequest, BaseApiResponse<OpportunityEntity>>(HttpMethod.Post, fullUrl, opportunityInsertRequest);
        return response.Entity.Opportunity;
    }

    public async Task<Opportunity?> OpportunityGetAsync(string clientNickName, int opportunityID)
    {
        try
        {
            var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/Opportunity/GetOpportunity?clientNickName={clientNickName}&opportunityID={opportunityID}");
            var response = await ExecuteApiCall<object, Opportunity?>(HttpMethod.Get, fullUrl, null);
            return response;
        }
        catch (TaskrowSharpWebException twe)
        {
            //NOTE: API Taskrow retorna erro 500 quando não encontra o registro, deveria retornar erro 404
            if (twe.HttpStatusCode == HttpStatusCode.InternalServerError)
                return null;
            throw;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Opportunity> OpportunityTransferToClientAsync(OpportunityTransferToClientRequest opportunityTransferToClientRequest)
    {
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/Opportunity/TransferToClient");
        var response = await ExecuteApiCall<OpportunityTransferToClientRequest, BaseApiResponse<Opportunity>>(HttpMethod.Post, fullUrl, opportunityTransferToClientRequest);
        return response.Entity;
    }

    #endregion

    #region Administrative

    public async Task<AdministrativeJobSubTypesListResponse> AdministrativeJobSubTypesListAsync()
    {
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/Administrative/ListJobSubType");
        var response = await ExecuteApiCall<object, AdministrativeJobSubTypesListResponse>(HttpMethod.Get, fullUrl, null);
        return response;
    }

    #endregion

    #region FinancialAccount

    public async Task<FinancialAccountListResponse> FinancialAccountListAsync()
    {
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v1/Administrative/ListFinancialAccount");
        var response = await ExecuteApiCall<object, FinancialAccountListResponse>(HttpMethod.Get, fullUrl, null);
        return response;
    }

    #endregion

    #region IntegrationLog

    public async Task IntegrationLogInsertAsync(IntegrationLogInsertRequest integrationLogInsertRequest)
    {
        var fullUrl = new Uri(this.ServiceUrl, $"/api/v2/integrationLog/log");
        await ExecuteApiCallWithNoReturn<IntegrationLogInsertRequest>(HttpMethod.Post, fullUrl, integrationLogInsertRequest);
    }

    public async Task<List<IntegrationLogEntry>> IntegrationLogListAsync(string entityType, string entityID)
    {
        var fullUrl = new Uri(this.ServiceUrl, $"api/v2/integrationLog/log?entityType={entityType}&entityID={entityID}");
        var response = await ExecuteApiCall<object, IntegrationLogListResponse>(HttpMethod.Get, fullUrl, null);
        return response.LogEntries;
    }

    #endregion
}
