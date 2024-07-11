using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web;
using TaskrowSharp.Exceptions;
using TaskrowSharp.Models.AdministrativeModels;
using TaskrowSharp.Models.BasicDataModels;
using TaskrowSharp.Models.ClientModels;
using TaskrowSharp.Models.IndexDataModels;
using TaskrowSharp.Models.InvoiceModels;
using TaskrowSharp.Models.JobModels;
using TaskrowSharp.Models.OpportunityModels;
using TaskrowSharp.Models.TaskModels;
using TaskrowSharp.Models.UserModels;

namespace TaskrowSharp;

public class TaskrowClient
{
    public string AccessKey { get; private set; }
    public Uri ServiceUrl { get; private set; }
    public string UserAgent { get; set; }
    public HttpClient HttpClient { get; private set; }

    private readonly JsonSerializerOptions jsonSerializerOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

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

    private static void ValidateAccessKey(string accessKey)
    {
        if (accessKey == null)
            throw new ArgumentNullException(nameof(accessKey));
        
        if (accessKey.Length < 20)
            throw new ArgumentException("Invalid AccessKey");
    }

    private static void ValidateServiceUrl(Uri serviceUrl)
    {
        if (serviceUrl == null)
            throw new ArgumentNullException(nameof(serviceUrl));

        if (serviceUrl.ToString().StartsWith("http://", StringComparison.CurrentCultureIgnoreCase))
            throw new InvalidServiceUrlException("https:// is required");
        
        if (!serviceUrl.ToString().StartsWith("https://", StringComparison.CurrentCultureIgnoreCase) || !serviceUrl.ToString().EndsWith(".taskrow.com/", StringComparison.CurrentCultureIgnoreCase))
            throw new InvalidServiceUrlException("Invalid service url, use the format: https://yourdomain.taskrow.com");
    }

    #region IndexData

    public async Task<IndexData> IndexDataGetAsync()
    {
        var relativeUrl = new Uri("/api/v1/Main/IndexData", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);

        try
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, fullUrl);
            httpRequest.Headers.Add("__identifier", this.AccessKey);
                
            var httpResponse = await this.HttpClient.SendAsync(httpRequest);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var model = JsonSerializer.Deserialize<IndexData>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    #endregion

    #region Client

    public async Task<List<ClientItemSearch>> ClientSearchAsync(string term, bool showInactives = true)
    {
        var relativeUrl = new Uri($"/api/v1/Search/SearchClients?&q={term}&showInactives={showInactives}", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);
        
        try
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, fullUrl);
            httpRequest.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.SendAsync(httpRequest);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var list = JsonSerializer.Deserialize<List<ClientItemSearch>>(jsonResponse);

            return list;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    public async Task<ClientListItemResponse> ClientListAsync(string? nextToken = null, bool? includeInactives = null)
    {
        string queryString = null;
        if (!string.IsNullOrWhiteSpace(nextToken))
            queryString = $"{queryString}{(queryString == null ? "?" : "&")}nextToken={nextToken}";
        if (includeInactives != null)
            queryString = $"{queryString}{(queryString == null ? "?" : "&")}includeInactives={includeInactives.ToString().ToLower()}";

        var relativeUrl = new Uri($"/api/v2/core/client{queryString}", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);

        try
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, fullUrl);
            httpRequest.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.SendAsync(httpRequest);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var list = JsonSerializer.Deserialize<ClientListItemResponse>(jsonResponse);

            return list;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    public async Task<ClientDetailEntity> ClientDetailGetAsync(int clientID)
    {
        var relativeUrl = new Uri($"/api/v1/Client/ClientDetail?clientID={clientID}", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);

        try
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, fullUrl);
            httpRequest.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.SendAsync(httpRequest);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");
            }

            var model = JsonSerializer.Deserialize<ClientDetailResponse>(jsonResponse);

            return model.Entity;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    public async Task<ClientInsertResponse> ClientInsertAsync(ClientInsertRequest insertClientRequest)
    {
        var relativeUrl = new Uri($"/api/v1/Client/SaveClient", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);
        var jsonRequest = JsonSerializer.Serialize(insertClientRequest, jsonSerializerOptions);

        try
        {
            var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            requestContent.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.PostAsync(fullUrl, requestContent);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var model = JsonSerializer.Deserialize<ClientInsertResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    public async Task<ClientUpdateResponse> ClientUpdateAsync(ClientUpdateRequest updateClientRequest)
    {
        var relativeUrl = new Uri($"/api/v1/Client/SaveClient", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);
        var jsonRequest = JsonSerializer.Serialize(updateClientRequest, jsonSerializerOptions);

        try
        {
            var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            requestContent.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.PostAsync(fullUrl, requestContent);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var model = JsonSerializer.Deserialize<ClientUpdateResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    #endregion

    #region ClientAddress

    public async Task<ClientAddressInsertResponse> ClientAddressInsertAsync(ClientAddressInsertRequest insertClientAddressRequest)
    {
        var relativeUrl = new Uri($"/api/v1/Client/SaveClientAddress", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);
        var jsonRequest = JsonSerializer.Serialize(insertClientAddressRequest);

        try
        {
            var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            requestContent.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.PostAsync(fullUrl, requestContent);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var model = JsonSerializer.Deserialize<ClientAddressInsertResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    public async Task<ClientAddressUpdateResponse> ClientAddressUpdateAsync(ClientAddressUpdateRequest updateClientAddressRequest)
    {
        var relativeUrl = new Uri($"/api/v1/Client/SaveClientAddress", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);
        var jsonRequest = JsonSerializer.Serialize(updateClientAddressRequest);

        try
        {
            var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            requestContent.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.PostAsync(fullUrl, requestContent);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var model = JsonSerializer.Deserialize<ClientAddressUpdateResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    #endregion

    #region ClientContact

    public async Task<List<ClientContact>> ClientContactListAsync(int clientID)
    {
        var relativeUrl = new Uri($"/api/v1/Client/ListClientContacts?clientID={clientID}", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);

        try
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, fullUrl);
            httpRequest.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.SendAsync(httpRequest);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var list = JsonSerializer.Deserialize<List<ClientContact>>(jsonResponse);

            return list;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    public async Task<ClientContactInsertResponse> ClientContactInsertAsync(ClientContactInsertRequest insertClientContactRequest)
    {
        var relativeUrl = new Uri($"/api/v1/Client/SaveContact", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);
        var jsonRequest = JsonSerializer.Serialize(insertClientContactRequest);

        try
        {
            var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            requestContent.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.PostAsync(fullUrl, requestContent);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var model = JsonSerializer.Deserialize<ClientContactInsertResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    #endregion

    #region User

    public async Task<List<User>> UserListAsync(bool showInactive = false)
    {
        var relativeUrl = new Uri($"/api/v1/User/ListUsers?showInactive={showInactive.ToString().ToLower()}", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);

        try
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, fullUrl);
            httpRequest.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.SendAsync(httpRequest);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var list = JsonSerializer.Deserialize<List<User>>(jsonResponse);

            return list;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    public async Task<UserDetailResponse> UserDetailGetAsync(int userID)
    {
        var relativeUrl = new Uri($"/api/v1/User/UserDetail?userID={userID}", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);

        try
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, fullUrl);
            httpRequest.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.SendAsync(httpRequest);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");
            }

            var model = JsonSerializer.Deserialize<UserDetailResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    #endregion

    #region Group

    public async Task<List<UserGroup>> UserGroupListAsync()
    {
        var relativeUrl = new Uri("/api/v1/Administrative/ListGroups?groupTypeID=2", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);

        try
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, fullUrl);
            httpRequest.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.SendAsync(httpRequest);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var model = JsonSerializer.Deserialize<UserGroupListResponse>(jsonResponse);

            return model.Groups;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    #endregion

    #region City

    public async Task<List<City>> CityListAsync(string stateAbbreviation)
    {
        if (string.IsNullOrWhiteSpace(stateAbbreviation)) throw new ArgumentNullException(nameof(stateAbbreviation));

        var queryString = $"uf={HttpUtility.UrlEncode(stateAbbreviation)}";
        var relativeUrl = new Uri($"/api/v1/Client/ListCities?{queryString}", UriKind.Relative);

        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);
        
        try
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, fullUrl);
            httpRequest.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.SendAsync(httpRequest);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var list = JsonSerializer.Deserialize<List<City>>(jsonResponse);

            return list;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    public async Task<City?> CityGetByNameAsync(string stateAbbreviation, string cityName)
    {
        if (string.IsNullOrWhiteSpace(stateAbbreviation)) throw new ArgumentNullException(nameof(stateAbbreviation));
        if (string.IsNullOrWhiteSpace(cityName)) throw new ArgumentNullException(nameof(cityName));

        stateAbbreviation = Utils.RemoveDiacritics(stateAbbreviation).ToUpper();
        cityName = Utils.RemoveDiacritics(cityName).ToUpper();

        var queryString = $"uf={HttpUtility.UrlEncode(stateAbbreviation)}&name={HttpUtility.UrlEncode(cityName)}";
        var relativeUrl = new Uri($"/api/v1/Client/ListCities?{queryString}", UriKind.Relative);

        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);

        try
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, fullUrl);
            httpRequest.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.SendAsync(httpRequest);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var list = JsonSerializer.Deserialize<List<City>>(jsonResponse);

            return list.FirstOrDefault();
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    #endregion

    #region Job

    public async Task<JobDetailEntity> JobDetailGetAsync(string clientNickName, int jobNumber)
    {
        var relativeUrl = new Uri($"/api/v1/Job/JobDetail?clientNickName={clientNickName}&JobNumber={jobNumber}", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);

        try
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, fullUrl);
            httpRequest.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.SendAsync(httpRequest);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");
            }

            var response = JsonSerializer.Deserialize<JobDetailEntity>(jsonResponse);

            if (response.Job == null)
                return null;

            return response;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    public async Task<JobInsertResponse> JobInsertAsync(JobInsertRequest insertJobRequest)
    {
        var relativeUrl = new Uri($"/api/v1/Job/SaveJob", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);
        var jsonRequest = JsonSerializer.Serialize(insertJobRequest, jsonSerializerOptions);

        try
        {
            var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            requestContent.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.PostAsync(fullUrl, requestContent);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var model = JsonSerializer.Deserialize<JobInsertResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    public async Task<JobUpdateResponse> JobUpdateAsync(JobUpdateRequest updateJobRequest)
    {
        var relativeUrl = new Uri($"/api/v1/Job/SaveJob", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);
        var jsonRequest = JsonSerializer.Serialize(updateJobRequest, jsonSerializerOptions);

        try
        {
            var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            requestContent.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.PostAsync(fullUrl, requestContent);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var model = JsonSerializer.Deserialize<JobUpdateResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    public async Task<JobStatusUpdateResponse> JobStatusUpdateAsync(string clientNickName, int jobNumber, int jobStatusID)
    {
        var relativeUrl = new Uri($"api/v1/Job/UpdateJobStatus?clientNickName={clientNickName}&jobNumber={jobNumber}&status={jobStatusID}", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);
        
        try
        {
            var requestContent = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            requestContent.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.PostAsync(fullUrl, requestContent);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var model = JsonSerializer.Deserialize<JobStatusUpdateResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    #endregion

    #region JobClientDependecies

    public async Task<JobClientDependencyListResponse> JobClientDependecyListAsync(int clientID)
    {
        var relativeUrl = new Uri($"/api/v1/Job/ListJobClientDependecies?clientID={clientID}", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);

        try
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, fullUrl);
            httpRequest.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.SendAsync(httpRequest);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var list = JsonSerializer.Deserialize<JobClientDependencyListResponse>(jsonResponse);

            return list;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    #endregion

    #region JobHome + JobWall

    public async Task<JobHome> JobHomeGetAsync(string clientNickname, int jobNumber)
    {
        var relativeUrl = new Uri($"/api/v1/Job/JobHome?clientNickName={clientNickname}&jobNumber={jobNumber}", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);

        try
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, fullUrl);
            httpRequest.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.SendAsync(httpRequest);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var jobHome = JsonSerializer.Deserialize<JobHome>(jsonResponse);

            return jobHome;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    public async Task<JobWallPostSaveResponse> JobWallPostSaveAsync(JobWallPostSaveRequest saveJobWallPostRequest)
    {
        var relativeUrl = new Uri($"/api/v1/Wall/SaveWallPost", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);
        var jsonRequest = JsonSerializer.Serialize(saveJobWallPostRequest, jsonSerializerOptions);

        try
        {
            var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            requestContent.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.PostAsync(fullUrl, requestContent);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var model = JsonSerializer.Deserialize<JobWallPostSaveResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
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

        try
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, fullUrl);
            httpRequest.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.SendAsync(httpRequest);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var model = JsonSerializer.Deserialize<TasksByGroupResponse>(jsonResponse);

            return model.Entity;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }
            
    public async Task<TaskDetailResponse> TaskDetailGetAsync(TaskReference taskReference)
    {
        var relativeUrl = new Uri($"/api/v1/Task/TaskDetail?clientNickname={taskReference.ClientNickname}&jobNumber={taskReference.JobNumber}&taskNumber={taskReference.TaskNumber}", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);

        try
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, fullUrl);
            httpRequest.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.SendAsync(httpRequest);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");
            }

            var model = JsonSerializer.Deserialize<TaskDetailResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }
    
    public async Task<TaskSaveResponse> TaskSaveAsync(TaskSaveRequest saveTaskRequest)
    {
        var relativeUrl = new Uri("/api/v1/Task/SaveTask", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);
        var jsonRequest = JsonSerializer.Serialize(saveTaskRequest);

        try
        {
            var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            requestContent.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.PostAsync(fullUrl, requestContent);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var model = JsonSerializer.Deserialize<TaskSaveResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    #endregion

    #region ExternalData

    public async Task<Dictionary<string, object?>> ExternalDataGetAsync(string provider, string entityName, int id)
    {
        entityName = entityName.ToLower();
        var relativeUrl = new Uri($"/api/v2/externaldata/{entityName}?provider={provider}&identification={id}", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);

        try
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, fullUrl);
            httpRequest.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.SendAsync(httpRequest);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");
            }

            var json = await httpResponse.Content.ReadAsStringAsync();

            var dic = JsonSerializer.Deserialize<Dictionary<string, object?>>(jsonResponse);
            return dic;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    public async Task<List<Dictionary<string, object?>>> ExternalDataSearchByFieldValueAsync(string provider, string entityName, string entityIdName,
        string fieldName, string fieldValue)
    {
        entityName = entityName.ToLower();
        var relativeUrl = new Uri($"api/v2/externaldata/{entityName}/find?provider={provider}&fieldName={fieldName}&fieldValue={fieldValue}", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);

        try
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, fullUrl);
            httpRequest.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.SendAsync(httpRequest);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var listRet = new List<Dictionary<string, object?>>();

            var listSearch = JsonSerializer.Deserialize<List<Dictionary<string, object?>>>(jsonResponse);
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
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    public async Task ExternalDataSaveAsync(string provider, string entityName, int id, Dictionary<string, object?> values)
    {
        entityName = entityName.ToLower();
        var relativeUrl = new Uri($"/api/v2/externaldata/{entityName}?provider={provider}&identification={id}", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);
        var jsonRequest = JsonSerializer.Serialize(values);

        try
        {
            var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            requestContent.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.PutAsync(fullUrl, requestContent);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    #endregion

    #region InvoiceFee

    public async Task<InvoiceFeeInsertResponse> InvoiceFeeInsertAsync(InvoiceFeeInsertRequest insertInvoiceRequest)
    {
        var relativeUrl = new Uri($"/api/v1/Invoice/SaveInvoiceFee", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);
        var jsonRequest = JsonSerializer.Serialize(insertInvoiceRequest);

        try
        {
            var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            requestContent.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.PostAsync(fullUrl, requestContent);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var model = JsonSerializer.Deserialize<InvoiceFeeInsertResponse>(jsonResponse);
            
            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    public async Task<InvoiceFee> InvoiceFeeDetailGetAsync(int jobNumber, int invoiceFeeID)
    {
        var relativeUrl = new Uri($"/api/v1/Invoice/InvoiceFeeDetail?jobNumber={jobNumber}&invoiceFeeID={invoiceFeeID}", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);

        try
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, fullUrl);
            httpRequest.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.SendAsync(httpRequest);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");
            }

            var model = JsonSerializer.Deserialize<InvoiceFeeeDetailResponse>(jsonResponse);

            return model.InvoiceFee;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    #endregion

    #region Invoice

    public async Task<InvoiceDetailResponseEntity> InvoiceDetailGetAsync(int invoiceID)
    {
        var relativeUrl = new Uri($"/api/v1/Invoice/GetInvoiceDetail?invoiceID={invoiceID}", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);

        try
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, fullUrl);
            httpRequest.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.SendAsync(httpRequest);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");
            }

            var model = JsonSerializer.Deserialize<InvoiceDetailResponse>(jsonResponse);

            return model?.Entity;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    public async Task<InvoiceSaveResponse> InvoiceSaveAsync(InvoiceSaveRequest saveInvoiceRequest)
    {
        var relativeUrl = new Uri($"/api/v1/Invoice/SaveInvoice", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);
        var jsonRequest = JsonSerializer.Serialize(saveInvoiceRequest);

        try
        {
            var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            requestContent.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.PostAsync(fullUrl, requestContent);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var model = JsonSerializer.Deserialize<InvoiceSaveResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    public async Task<InvoiceCancelResponse> InvoiceCancelAsync(InvoiceCancelRequest request)
    {
        var memoEncoded = (!string.IsNullOrEmpty(request.Memo) ? HttpUtility.UrlEncode(request.Memo) : null);
        var relativeUrl = new Uri($"/api/v1/Invoice/CancelInvoice?invoiceID={request.InvoiceID}&memo={memoEncoded}", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);

        try
        {
            var httRequest = new HttpRequestMessage(HttpMethod.Get, fullUrl);
            httRequest.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.SendAsync(httRequest);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var model = JsonSerializer.Deserialize<InvoiceCancelResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    public async Task<InvoiceDeleteResponse> InvoiceDeleteAsync(InvoiceDeleteRequest request)
    {
        var memoEncoded = (!string.IsNullOrEmpty(request.Memo) ? HttpUtility.UrlEncode(request.Memo) : null);
        var relativeUrl = new Uri($"/api/v1/Invoice/DeleteInvoice?invoiceID={request.InvoiceID}&memo={memoEncoded}", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);

        try
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, fullUrl);
            httpRequest.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.SendAsync(httpRequest);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var model = JsonSerializer.Deserialize<InvoiceDeleteResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    #endregion

    #region InvoiceStatus

    public async Task<InvoiceUpdateResponse> InvoiceStatusUpdateAsync(InvoiceStatusUpdateRequest updateInvoiceRequest)
    {
        var relativeUrl = new Uri($"/api/v1/Invoice/SaveInvoiceIntegrationStatus", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);
        var jsonRequest = JsonSerializer.Serialize(updateInvoiceRequest);

        try
        {
            var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            requestContent.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.PostAsync(fullUrl, requestContent);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var model = JsonSerializer.Deserialize<InvoiceUpdateResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    #endregion

    #region InvoiceAuthorization

    public async Task<InvoiceAuthorizationSaveResponse> InvoiceAuthorizationSaveAsync(InvoiceAuthorizationSaveRequest request)
    {
        string parameters = $"jobNumber={request.JobNumber}";
        parameters += $"&invoiceID={request.InvoiceID}";
        parameters += $"&guidModification={request.GuidModification}";
        for (int i=0; i< request.InvoiceFeeIDs.Count; i++)
            parameters += $"&invoiceFeeIDs[{i}]={request.InvoiceFeeIDs[i]}";

        var relativeUrl = new Uri($"/api/v1/Invoice/SaveInvoiceAuthorization?{parameters}", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);

        try
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, fullUrl);
            httpRequest.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.SendAsync(httpRequest);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");
            }

            var model = JsonSerializer.Deserialize<InvoiceAuthorizationSaveResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    #endregion

    #region InvoiceBill

    public async Task<InvoiceBillSaveResponse> InvoiceBillSaveAsync(InvoiceBillSaveRequest saveInvoiceBillRequest)
    {
        var relativeUrl = new Uri($"/api/v1/Invoice/SaveInvoiceBill", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);
        var jsonRequest = JsonSerializer.Serialize(saveInvoiceBillRequest);

        try
        {
            var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            requestContent.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.PostAsync(fullUrl, requestContent);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var model = JsonSerializer.Deserialize<InvoiceBillSaveResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    public async Task<InvoiceBillCancelResponse> InvoiceBillCancelAsync(int invoiceBillID)
    {
        var relativeUrl = new Uri($"/api/v1/Invoice/CancelInvoiceBill?invoiceBillID={invoiceBillID}", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);
        
        try
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, fullUrl);
            httpRequest.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.SendAsync(httpRequest);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var model = JsonSerializer.Deserialize<InvoiceBillCancelResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    #endregion

    #region InvoiceBillPayment

    public async Task<InvoiceBillPaymentSaveResponse> InvoiceBillPaymentSaveAsync(InvoiceBillPaymentSaveRequest saveInvoiceBillRequest)
    {
        var relativeUrl = new Uri($"/api/v1/Invoice/SaveInvoiceBillPayment", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);
        var jsonRequest = JsonSerializer.Serialize(saveInvoiceBillRequest);

        try
        {
            var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            requestContent.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.PostAsync(fullUrl, requestContent);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var model = JsonSerializer.Deserialize<InvoiceBillPaymentSaveResponse>(jsonResponse);

            if (model.Success == null)
                model.Success = true;

            //NOTE: This method returns different types for success or error sittuation
            // When Success: { InvoiceDetail: {}, InvoiceBill: {}: AllowEditInvoice: true }
            // When Error:   { "Success": false, "Message": "Cobrança de nota fiscal não encontrada", "Entity": null, "TargetURL": null }

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    #endregion

    #region Opportunity

    public async Task<Opportunity> OpportunityInsertAsync(OpportunityInsertRequest opportunityInsertRequest)
    {
        var relativeUrl = new Uri($"/api/v1/Opportunity/SaveOpportunity", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);
        var jsonRequest = JsonSerializer.Serialize(opportunityInsertRequest, jsonSerializerOptions);

        try
        {
            var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            requestContent.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.PostAsync(fullUrl, requestContent);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var response = JsonSerializer.Deserialize<OpportunityInsertResponse>(jsonResponse);
            if (!response.Success)
                throw new TaskrowException($"Error {response.Message}");

            return response.Entity.Opportunity;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    public async Task<Opportunity?> OpportunityGetAsync(string clientNickName, int opportunityID)
    {
        var relativeUrl = new Uri($"/api/v1/Opportunity/GetOpportunity?clientNickName={clientNickName}&opportunityID={opportunityID}", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);
        
        try
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, fullUrl);
            httpRequest.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.SendAsync(httpRequest);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            //TODO: API Taskrow retorna erro 500 quando não encontra o registro, deveria retornar erro 404
            if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound || httpResponse.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                return null;
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var response = JsonSerializer.Deserialize<Opportunity>(jsonResponse);
            return response;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    public async Task<Opportunity> OpportunityTransferToClientAsync(OpportunityTransferToClientRequest opportunityTransferToClientRequest)
    {
        var relativeUrl = new Uri($"/api/v1/Opportunity/TransferToClient", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);
        var jsonRequest = JsonSerializer.Serialize(opportunityTransferToClientRequest, jsonSerializerOptions);

        try
        {
            var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            requestContent.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.PostAsync(fullUrl, requestContent);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var response = JsonSerializer.Deserialize<OpportunityTransferToClientResponse>(jsonResponse);
            if (!response.Success)
                throw new TaskrowException($"Error {response.Message}");

            return response.Entity;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    #endregion

    #region Administrative

    public async Task<AdministrativeJobSubTypesListResponse> AdministrativeJobSubTypesListAsync()
    {
        var relativeUrl = new Uri($"/api/v1/Administrative/ListJobSubType", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);

        try
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, fullUrl);
            httpRequest.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.SendAsync(httpRequest);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var list = JsonSerializer.Deserialize<AdministrativeJobSubTypesListResponse>(jsonResponse);

            return list;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    #endregion
}
