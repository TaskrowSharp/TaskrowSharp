using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using TaskrowSharp.Exceptions;
using TaskrowSharp.Models;

namespace TaskrowSharp
{
    public class TaskrowClient
    {
        public string AccessKey { get; private set; }
        public Uri ServiceUrl { get; private set; }
        public string UserAgent { get; set; }
        public HttpClient HttpClient { get; private set; }
        
        public TaskrowClient(Uri serviceUrl, string accessKey, HttpClient httpClient)
        {
            ValidateServiceUrl(serviceUrl);
            ValidateAccessKey(accessKey);

            this.ServiceUrl = serviceUrl;
            this.AccessKey = accessKey;
            this.HttpClient = httpClient;

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            this.UserAgent = $"TaskrowSharp v{Utils.Application.GetAppVersion()}";
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
                var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);
                request.Headers.Add("__identifier", this.AccessKey);
                    
                var response = await this.HttpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");

                var json = await response.Content.ReadAsStringAsync();

                var model = JsonSerializer.Deserialize<IndexData>(json);

                return model;
            }
            catch (Exception ex)
            {
                throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
            }
        }

        #endregion

        #region Client

        public async Task<List<SearchClientItem>> SearchClients(string term, bool showInactives = true)
        {
            var relativeUrl = new Uri($"/api/v1/Search/SearchClients?&q={term}&showInactives={showInactives}", UriKind.Relative);
            var fullUrl = new Uri(this.ServiceUrl, relativeUrl);
            
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);
                request.Headers.Add("__identifier", this.AccessKey);

                var response = await this.HttpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");

                var json = await response.Content.ReadAsStringAsync();

                var list = JsonSerializer.Deserialize<List<SearchClientItem>>(json);

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
                var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);
                request.Headers.Add("__identifier", this.AccessKey);

                var response = await this.HttpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");

                var json = await response.Content.ReadAsStringAsync();

                var model = JsonSerializer.Deserialize<ClientDetailResponse>(json);

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

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, fullUrl);
                request.Headers.Add("__identifier", this.AccessKey);
                request.Content = JsonContent.Create(insertClientRequest);

                var response = await this.HttpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");

                var json = await response.Content.ReadAsStringAsync();

                var model = JsonSerializer.Deserialize<InsertClientResponse>(json);

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
                var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);
                request.Headers.Add("__identifier", this.AccessKey);

                var response = await this.HttpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");

                var json = await response.Content.ReadAsStringAsync();

                var list = JsonSerializer.Deserialize<List<ClientContact>>(json);

                return list;
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
                var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);
                request.Headers.Add("__identifier", this.AccessKey);

                var response = await this.HttpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");

                var json = await response.Content.ReadAsStringAsync();

                var list = JsonSerializer.Deserialize<List<User>>(json);

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
                var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);
                request.Headers.Add("__identifier", this.AccessKey);

                var response = await this.HttpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");

                var json = await response.Content.ReadAsStringAsync();

                var model = JsonSerializer.Deserialize<UserDetailResponse>(json);

                return model;
            }
            catch (Exception ex)
            {
                throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
            }
        }

        #endregion

        #region Group

        public async Task<List<Group>> ListGroupsAsync()
        {
            var relativeUrl = new Uri("/api/v1/Administrative/ListGroups?groupTypeID=2", UriKind.Relative);
            var fullUrl = new Uri(this.ServiceUrl, relativeUrl);

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);
                request.Headers.Add("__identifier", this.AccessKey);

                var response = await this.HttpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");

                var json = await response.Content.ReadAsStringAsync();

                var model = JsonSerializer.Deserialize<ListGroupResponse>(json);

                return model.Groups;
            }
            catch (Exception ex)
            {
                throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
            }
        }

        #endregion

        #region City

        public async Task<List<City>> ListCitiesAsync(string stateCode)
        {
            var relativeUrl = new Uri($"/api/v1/Client/ListCities?uf={stateCode}", UriKind.Relative);
            var fullUrl = new Uri(this.ServiceUrl, relativeUrl);
            
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);
                request.Headers.Add("__identifier", this.AccessKey);

                var response = await this.HttpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");

                var json = await response.Content.ReadAsStringAsync();

                var list = JsonSerializer.Deserialize<List<City>>(json);

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
                var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);
                request.Headers.Add("__identifier", this.AccessKey);

                var response = await this.HttpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");

                var json = await response.Content.ReadAsStringAsync();

                var model = JsonSerializer.Deserialize<TasksByGroupResponse>(json);

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
                var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);
                request.Headers.Add("__identifier", this.AccessKey);

                var response = await this.HttpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");

                var json = await response.Content.ReadAsStringAsync();

                var model = JsonSerializer.Deserialize<TaskDetailResponse>(json);

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

            try
            {               
                var request = new HttpRequestMessage(HttpMethod.Post, fullUrl);
                request.Headers.Add("__identifier", this.AccessKey);
                request.Content = JsonContent.Create(saveTaskRequest);

                var response = await this.HttpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");

                var json = await response.Content.ReadAsStringAsync();

                var model = JsonSerializer.Deserialize<SaveTaskResponse>(json);

                return model;
            }
            catch (Exception ex)
            {
                throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
            }
        }

        #endregion

        #region ExternalData

        public async Task<Dictionary<string, string?>> GetExternalDataAsync(string provider, string entityName, int id)
        {
            entityName = entityName.ToLower();
            var relativeUrl = new Uri($"/api/v2/externaldata/{entityName}?provider={provider}&identification=${id}", UriKind.Relative);
            var fullUrl = new Uri(this.ServiceUrl, relativeUrl);

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);
                request.Headers.Add("__identifier", this.AccessKey);

                var response = await this.HttpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");

                var json = await response.Content.ReadAsStringAsync();

                var dic = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                return dic;
            }
            catch (Exception ex)
            {
                throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
            }
        }

        public async Task SaveExternalDataAsync(string provider, string entityName, int id, Dictionary<string, string?> values)
        {
            entityName = entityName.ToLower();
            var relativeUrl = new Uri($"/api/v2/externaldata/{entityName}?provider={provider}&identification=${id}", UriKind.Relative);
            var fullUrl = new Uri(this.ServiceUrl, relativeUrl);

            try
            {               
                var request = new HttpRequestMessage(HttpMethod.Post, fullUrl);
                request.Headers.Add("__identifier", this.AccessKey);
                request.Content = JsonContent.Create(values);

                var response = await this.HttpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");
            }
            catch (Exception ex)
            {
                throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
            }
        }

        #endregion
    }
}
