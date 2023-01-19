using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");

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
                var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);
                request.Headers.Add("__identifier", this.AccessKey);

                var response = await this.HttpClient.SendAsync(request);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");

                var list = JsonSerializer.Deserialize<List<SearchClientItem>>(jsonResponse);

                return list;
            }
            catch (Exception ex)
            {
                throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
            }
        }

        public async Task<ListClientItemResponse> ListClientsAsync(string? nextToken = null)
        {
            var relativeUrl = new Uri($"/api/v2/core/client?nextToken={nextToken}", UriKind.Relative);
            var fullUrl = new Uri(this.ServiceUrl, relativeUrl);

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);
                request.Headers.Add("__identifier", this.AccessKey);

                var response = await this.HttpClient.SendAsync(request);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");

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
                var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);
                request.Headers.Add("__identifier", this.AccessKey);

                var response = await this.HttpClient.SendAsync(request);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        return null;
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");
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
            var jsonRequest = JsonSerializer.Serialize(insertClientRequest);

            try
            {
                var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                requestContent.Headers.Add("__identifier", this.AccessKey);

                var response = await this.HttpClient.PostAsync(fullUrl, requestContent);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");

                var model = JsonSerializer.Deserialize<InsertClientResponse>(jsonResponse);

                return model;
            }
            catch (Exception ex)
            {
                throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
            }
        }

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

                var response = await this.HttpClient.PostAsync(fullUrl, requestContent);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");

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

                var response = await this.HttpClient.PostAsync(fullUrl, requestContent);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");

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
                var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);
                request.Headers.Add("__identifier", this.AccessKey);

                var response = await this.HttpClient.SendAsync(request);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");

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

                var response = await this.HttpClient.PostAsync(fullUrl, requestContent);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");

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
                var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);
                request.Headers.Add("__identifier", this.AccessKey);

                var response = await this.HttpClient.SendAsync(request);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");

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
                var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);
                request.Headers.Add("__identifier", this.AccessKey);

                var response = await this.HttpClient.SendAsync(request);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        return null;
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");
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

        public async Task<List<Group>> ListGroupsAsync()
        {
            var relativeUrl = new Uri("/api/v1/Administrative/ListGroups?groupTypeID=2", UriKind.Relative);
            var fullUrl = new Uri(this.ServiceUrl, relativeUrl);

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);
                request.Headers.Add("__identifier", this.AccessKey);

                var response = await this.HttpClient.SendAsync(request);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");

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

        public async Task<List<City>> ListCitiesAsync(string stateCode)
        {
            var relativeUrl = new Uri($"/api/v1/Client/ListCities?uf={stateCode}", UriKind.Relative);
            var fullUrl = new Uri(this.ServiceUrl, relativeUrl);
            
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);
                request.Headers.Add("__identifier", this.AccessKey);

                var response = await this.HttpClient.SendAsync(request);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");

                var list = JsonSerializer.Deserialize<List<City>>(jsonResponse);

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
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");

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
                var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);
                request.Headers.Add("__identifier", this.AccessKey);

                var response = await this.HttpClient.SendAsync(request);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        return null;
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");
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

                var response = await this.HttpClient.PostAsync(fullUrl, requestContent);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");

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
                var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);
                request.Headers.Add("__identifier", this.AccessKey);

                var response = await this.HttpClient.SendAsync(request);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        return null;
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");
                }

                var json = await response.Content.ReadAsStringAsync();

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
                var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);
                request.Headers.Add("__identifier", this.AccessKey);

                var response = await this.HttpClient.SendAsync(request);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");

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

                var response = await this.HttpClient.PutAsync(fullUrl, requestContent);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");
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

                var response = await this.HttpClient.PostAsync(fullUrl, requestContent);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");

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
                var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);
                request.Headers.Add("__identifier", this.AccessKey);

                var response = await this.HttpClient.SendAsync(request);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        return null;
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");
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

        public async Task<InvoiceDetail> GetInvoiceDetailAsync(int invoiceID)
        {
            var relativeUrl = new Uri($"/api/v1/Invoice/GetInvoiceDetail?invoiceID={invoiceID}", UriKind.Relative);
            var fullUrl = new Uri(this.ServiceUrl, relativeUrl);

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);
                request.Headers.Add("__identifier", this.AccessKey);

                var response = await this.HttpClient.SendAsync(request);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        return null;
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");
                }

                var model = JsonSerializer.Deserialize<InvoiceDetailResponse>(jsonResponse);

                return model.Entity?.InvoiceDetail;
            }
            catch (Exception ex)
            {
                throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
            }
        }

        #endregion

        #region InvoiceAuthorization

        public async Task<SaveInvoiceAuthorization> SaveInvoiceAuthorizationAsync(int jobNumber, int invoiceID, string guidModification, List<int> invoiceFeeIDs)
        {
            string parameters = $"jobNumber={jobNumber}";
            parameters += $"&invoiceID={invoiceID}";
            parameters += $"&guidModification={guidModification}";
            for (int i=0; i< invoiceFeeIDs.Count; i++)
                parameters += $"&invoiceFeeIDs[{i}]={invoiceFeeIDs[i]}";

            var relativeUrl = new Uri($"/api/v1/Invoice/SaveInvoiceAuthorization?{parameters}", UriKind.Relative);
            var fullUrl = new Uri(this.ServiceUrl, relativeUrl);

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, fullUrl);
                request.Headers.Add("__identifier", this.AccessKey);

                var response = await this.HttpClient.SendAsync(request);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        return null;
                    throw new TaskrowException($"Error statusCode: {(int)response.StatusCode}");
                }

                var model = JsonSerializer.Deserialize<SaveInvoiceAuthorization>(jsonResponse);

                return model;
            }
            catch (Exception ex)
            {
                throw new TaskrowException($"Error in Taskrow API Call {relativeUrl} -- {ex.Message} -- Url: {fullUrl}", ex);
            }
        }

        #endregion
    }
}
