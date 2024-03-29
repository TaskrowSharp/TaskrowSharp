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

    public async Task<IndexData> GetIndexDataAsync()
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

    public async Task<List<SearchClientItem>> SearchClientsAsync(string term, bool showInactives = true)
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

            var list = JsonSerializer.Deserialize<List<SearchClientItem>>(jsonResponse);

            return list;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    public async Task<ListClientItemResponse> ListClientsAsync(string? nextToken = null, bool? includeInactives = null)
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

            var list = JsonSerializer.Deserialize<ListClientItemResponse>(jsonResponse);

            return list;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    public async Task<ClientDetailEntity> GetClientDetailAsync(int clientID)
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

    public async Task<InsertClientResponse> InsertClientAsync(InsertClientRequest insertClientRequest)
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

            var model = JsonSerializer.Deserialize<InsertClientResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    public async Task<UpdateClientResponse> UpdateClientAsync(UpdateClientRequest updateClientRequest)
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

            var model = JsonSerializer.Deserialize<UpdateClientResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    #endregion

    #region ClientAddress

    public async Task<InsertClientAddressResponse> InsertClientAddressAsync(InsertClientAddressRequest insertClientAddressRequest)
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

            var model = JsonSerializer.Deserialize<InsertClientAddressResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    public async Task<UpdateClientAddressResponse> UpdateClientAddressAsync(UpdateClientAddressRequest updateClientAddressRequest)
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

            var model = JsonSerializer.Deserialize<UpdateClientAddressResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    #endregion

    #region ClientContact

    public async Task<List<ClientContact>> ListClientContactsAsync(int clientID)
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

    public async Task<InsertClientContactResponse> InsertClientContactAsync(InsertClientContactRequest insertClientContactRequest)
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

            var model = JsonSerializer.Deserialize<InsertClientContactResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    #endregion

    #region User

    public async Task<List<User>> ListUsersAsync(bool showInactive = false)
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

    public async Task<UserDetailResponse> GetUserDetailAsync(int userID)
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

    public async Task<List<UserGroup>> ListGroupsAsync()
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

            var model = JsonSerializer.Deserialize<ListGroupResponse>(jsonResponse);

            return model.Groups;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    #endregion

    #region City

    public async Task<List<City>> ListCitiesAsync(string stateAbbreviation)
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

    public async Task<City?> GetCityByNameAsync(string stateAbbreviation, string cityName)
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

    public async Task<JobDetailEntity> GetJobDetailAsync(string clientNickName, int jobNumber)
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

            var model = JsonSerializer.Deserialize<JobDetailEntity>(jsonResponse);

            if (model.Job == null)
                return null;

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    public async Task<InsertJobResponse> InsertJobAsync(InsertJobRequest insertJobRequest)
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

            var model = JsonSerializer.Deserialize<InsertJobResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    /*public async Task<UpdateJobResponse> UpdateJobAsync(UpdateJobRequest updateJobRequest)
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

            var model = JsonSerializer.Deserialize<UpdateJobResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }*/

    public async Task<UpdateJobStatusResponse> UpdateJobStatusAsync(string clientNickName, int jobNumber, int jobStatusID)
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

            var model = JsonSerializer.Deserialize<UpdateJobStatusResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    #endregion

    #region JobClientDependecies

    public async Task<ListJobClientDependeciesResponse> ListJobClientDependeciesAsync(int clientID)
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

            var list = JsonSerializer.Deserialize<ListJobClientDependeciesResponse>(jsonResponse);

            return list;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    #endregion

    #region Task

    public async Task<TasksByGroupEntity> ListTasksByGroupAsync(int groupID, int? userID = null)
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
            
    public async Task<TaskDetailResponse> GetTaskDetailAsync(TaskReference taskReference)
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
            
    public async Task<SaveTaskResponse> SaveTaskAsync(SaveTaskRequest saveTaskRequest)
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

            var model = JsonSerializer.Deserialize<SaveTaskResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    #endregion

    #region ExternalData

    public async Task<Dictionary<string, object?>> GetExternalDataAsync(string provider, string entityName, int id)
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

    public async Task<List<Dictionary<string, object?>>> SearchExternalDataByFieldValueAsync(string provider, string entityName, string entityIdName,
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
                var dicRet = await GetExternalDataAsync(provider, entityName, id);
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

    public async Task SaveExternalDataAsync(string provider, string entityName, int id, Dictionary<string, object?> values)
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

    public async Task<InsertInvoiceFeeResponse> InsertInvoiceFeeAsync(InsertInvoiceFeeRequest insertInvoiceRequest)
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

            var model = JsonSerializer.Deserialize<InsertInvoiceFeeResponse>(jsonResponse);
            
            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    public async Task<InvoiceFee> GetInvoiceFeeDetailAsync(int jobNumber, int invoiceFeeID)
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

    public async Task<InvoiceDetailResponseEntity> GetInvoiceDetailAsync(int invoiceID)
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

    public async Task<SaveInvoiceResponse> SaveInvoiceAsync(SaveInvoiceRequest saveInvoiceRequest)
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

            var model = JsonSerializer.Deserialize<SaveInvoiceResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    public async Task<CancelInvoiceResponse> CancelInvoiceAsync(CancelInvoiceRequest request)
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

            var model = JsonSerializer.Deserialize<CancelInvoiceResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    public async Task<DeleteInvoiceResponse> DeleteInvoiceAsync(DeleteInvoiceRequest request)
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

            var model = JsonSerializer.Deserialize<DeleteInvoiceResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    #endregion

    #region InvoiceStatus

    public async Task<UpdateInvoiceResponse> UpdateInvoiceStatusAsync(UpdateInvoiceStatusRequest updateInvoiceRequest)
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

            var model = JsonSerializer.Deserialize<UpdateInvoiceResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    #endregion

    #region InvoiceAuthorization

    public async Task<SaveInvoiceAuthorizationResponse> SaveInvoiceAuthorizationAsync(SaveInvoiceAuthorizationRequest request)
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

            var model = JsonSerializer.Deserialize<SaveInvoiceAuthorizationResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    #endregion

    #region InvoiceBill

    public async Task<SaveInvoiceBillResponse> SaveInvoiceBillAsync(SaveInvoiceBillRequest saveInvoiceBillRequest)
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

            var model = JsonSerializer.Deserialize<SaveInvoiceBillResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    public async Task<CancelInvoiceBillResponse> CancelInvoiceBillAsync(int invoiceBillID)
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

            var model = JsonSerializer.Deserialize<CancelInvoiceBillResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    #endregion

    #region InvoiceBillPayment

    public async Task<SaveInvoiceBillPaymentResponse> SaveInvoiceBillPaymentAsync(SaveInvoiceBillPaymentRequest saveInvoiceBillRequest)
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

            var model = JsonSerializer.Deserialize<SaveInvoiceBillPaymentResponse>(jsonResponse);

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

    public async Task<InsertOpportunityResponse> InsertOpportunityAsync(InsertOpportunityRequest insertOpportunityRequest)
    {
        var relativeUrl = new Uri($"/api/v1/Opportunity/SaveOpportunity", UriKind.Relative);
        var fullUrl = new Uri(this.ServiceUrl, relativeUrl);
        var jsonRequest = JsonSerializer.Serialize(insertOpportunityRequest, jsonSerializerOptions);

        try
        {
            var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            requestContent.Headers.Add("__identifier", this.AccessKey);

            var httpResponse = await this.HttpClient.PostAsync(fullUrl, requestContent);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
                throw new TaskrowException($"Error statusCode: {(int)httpResponse.StatusCode}");

            var model = JsonSerializer.Deserialize<InsertOpportunityResponse>(jsonResponse);

            return model;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    #endregion

    #region Administrative

    public async Task<ListAdministrativeJobSubTypesResponse> ListAdministrativeJobSubTypesAsync()
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

            var list = JsonSerializer.Deserialize<ListAdministrativeJobSubTypesResponse>(jsonResponse);

            return list;
        }
        catch (Exception ex)
        {
            throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
        }
    }

    #endregion
}
