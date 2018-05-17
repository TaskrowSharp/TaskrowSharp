using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace TaskrowSharp
{
    public class TaskrowClient
    {
        private Uri serviceUrl = null;
        private CookieCollection authCookies;
        private string authAccessKey;

        public Uri ServiceUrl { get { return this.serviceUrl; } }

        public string UserAgent { get; set; }

        public RetryPolicy RetryPolicy { get; set; }

        public bool StatusConnected
        {
            get { return (this.serviceUrl != null); }
        }

        public TaskrowClient()
        {
            this.UserAgent = string.Format("TaskrowSharp v{0}", Utils.Application.GetAppVersion());
            this.RetryPolicy = new RetryPolicy(1, 120);
        }

        private void ValidateIsConnected()
        {
            if (!this.StatusConnected)
                throw new NotConnectedException();
        }

        private RetryPolicy GetRetryPolicy(RetryPolicy retryPolicy)
        {
            if (retryPolicy != null)
                return retryPolicy;

            return this.RetryPolicy;
        }

        #region Connect

        public void Connect(Uri serviceUrl, string accessKey)
        {
            Connect(serviceUrl, new AccessKeyCredential(accessKey), this.RetryPolicy);
        }

        public void Connect(Uri serviceUrl, string accessKey, RetryPolicy retryPolicy)
        {
            Connect(serviceUrl, new AccessKeyCredential(accessKey), retryPolicy);
        }

        public void Connect(Uri serviceUrl, Credential credential)
        {
            Connect(serviceUrl, credential, this.RetryPolicy);
        }

        public void Connect(Uri serviceUrl, Credential credential, RetryPolicy retryPolicy)
        {
            ValidateServiceUrl(serviceUrl);
            credential.Validate();            

            if (credential == null)
                throw new ArgumentNullException(nameof(credential));
            
            if (credential is AccessKeyCredential)
                ConnectUsingAccessKey(serviceUrl, (AccessKeyCredential)credential, retryPolicy);
            else if (credential is EmailAndPasswordCredential)
                ConnectUsingEmailAndPassword(serviceUrl, (EmailAndPasswordCredential)credential, retryPolicy);
            else
                throw new TaskrowException("Credential Type not supported");
        }

        private void ConnectUsingEmailAndPassword(Uri serviceUrl, EmailAndPasswordCredential credential, RetryPolicy retryPolicy)
        {
            credential.Validate();

            for (int attempt = 1; attempt <= retryPolicy.MaxAttempts; attempt++)
            {
                try
                {
                    this.serviceUrl = null;

                    Uri url = new Uri(serviceUrl, "/LoginPassword");
                    string myParameters = string.Format("email={0}&password={1}", WebUtility.UrlEncode(credential.Email), WebUtility.UrlEncode(credential.Password));

                    System.Net.HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.UserAgent = UserAgent;
                    request.AllowAutoRedirect = false;
                    request.CookieContainer = new CookieContainer();
                    request.Timeout = retryPolicy.TimeOutSeconds * 1000;
                    using (var writer = new System.IO.StreamWriter(request.GetRequestStream()))
                    {
                        writer.Write(myParameters);
                        writer.Flush();
                    }

                    var response = request.GetResponse() as HttpWebResponse;
                    Utils.JsonWebClient.ValidateHttpStatusCode(response, url, true);

                    string cookie1 = null;
                    string cookie2 = null;
                    var cookies = response.Cookies;
                    if (response.Cookies["AUTHTICKET"] != null)
                        cookie1 = response.Cookies["AUTHTICKET"].Value;
                    if (response.Cookies["AUTHTICKETVOL"] != null)
                        cookie2 = response.Cookies["AUTHTICKETVOL"].Value;

                    if (string.IsNullOrEmpty(cookie1) || string.IsNullOrEmpty(cookie2))
                        throw new AuthenticationException(string.Format("Error connecting in Taskrow. Check parameters: ServiceUrl, Email and Password -- URL: {0} -- email: {1}", url.ToString(), credential.Email));

                    this.serviceUrl = serviceUrl;
                    this.authCookies = cookies;
                    this.authAccessKey = null;

                    response.Close();

                    return; //Success
                }
                catch (System.Exception ex)
                {
                    if (Utils.Application.IsExceptionWithoutRetry(ex))
                        throw;

                    if (attempt == retryPolicy.MaxAttempts)
                        throw new AuthenticationException(string.Format("Error connecting in Taskrow after {0} attempts(s) -- url: {1} -- email: {2} -- {3} -- TimeOut: {4} seconds", retryPolicy.MaxAttempts, serviceUrl.ToString(), credential.Email, ex.Message, retryPolicy.TimeOutSeconds), ex);
                }
            }

            throw new TaskrowException("Unexpected error in attempts control");
        }

        private void ConnectUsingAccessKey(Uri serviceUrl, AccessKeyCredential credential, RetryPolicy retryPolicy)
        {
            credential.Validate();

            for (int attempt = 1; attempt <= retryPolicy.MaxAttempts; attempt++)
            {
                try
                {
                    this.serviceUrl = null;

                    Uri url = new Uri(serviceUrl, "/User/MyInfo");

                    var client = new Utils.JsonWebClient(this.UserAgent);
                    client.AllowAutoRedirect = true;
                    client.TimeOutMilliseconds = retryPolicy.TimeOutSeconds * 1000;
                    client.Headers.Add("__identifier", credential.AccessKey);
                    
                    string json = client.GetReturnString(url);

                    //When password is invalid, Taskrow returns html from login page
                    if (json.IndexOf("<html", StringComparison.CurrentCultureIgnoreCase) != -1)
                        throw new AuthenticationException("Invalid AccessKey");

                    this.serviceUrl = serviceUrl;
                    this.authCookies = null;
                    this.authAccessKey = credential.AccessKey;

                    return; //Success
                }
                catch (System.Exception ex)
                {
                    if (Utils.Application.IsExceptionWithoutRetry(ex))
                        throw;

                    if (attempt == retryPolicy.MaxAttempts)
                        throw new AuthenticationException(string.Format("Error connecting in Taskrow after {0} attempts(s) -- url: {1} -- {2} -- TimeOut: {3} seconds", retryPolicy.MaxAttempts, serviceUrl.ToString(), ex.Message, retryPolicy.TimeOutSeconds), ex);
                }
            }

            throw new TaskrowException("Unexpected error in attempts control");
        }

        private void ValidateServiceUrl(Uri serviceUrl)
        {
            if (serviceUrl == null)
                throw new InvalidServiceUrlException("Service url is required");

            if (serviceUrl.ToString().StartsWith("http://", StringComparison.CurrentCultureIgnoreCase))
                throw new InvalidServiceUrlException("https:// is required");
                        
            if (!serviceUrl.ToString().StartsWith("https://", StringComparison.CurrentCultureIgnoreCase) || !serviceUrl.ToString().EndsWith(".taskrow.com/", StringComparison.CurrentCultureIgnoreCase))
                throw new InvalidServiceUrlException("Invalid service url, use the format: https://yourdomain.taskrow.com");
        }
        
        public void Disconnect()
        {
            if (!StatusConnected)
                return;

            this.serviceUrl = null;
            this.authCookies = null;
            this.authAccessKey = null;
        }

        public void KeepAlive()
        {
            KeepAlive(this.RetryPolicy);
        }

        public void KeepAlive(RetryPolicy retryPolicy)
        {
            //Url: /main/keepalive

            ValidateIsConnected();

            var url = new Uri(this.serviceUrl, "/main/keepalive");

            for (int attempt = 1; attempt <= retryPolicy.MaxAttempts; attempt++)
            {
                try
                {
                    var client = new Utils.JsonWebClient(this.UserAgent);
                    client.TimeOutMilliseconds = 5000; //5 seconds
                    client.Cookies.Add(this.authCookies);

                    string json = client.GetReturnString(url);
                }
                catch (System.Exception ex)
                {
                    if (Utils.Application.IsExceptionWithoutRetry(ex))
                        throw;

                    throw new TaskrowException(string.Format("Error in KeepAlive process -- Url: {0} -- Error: {1}", url.ToString(), ex.Message), ex);
                }
            }
        }

        #endregion

        #region ListUsers

        public List<User> ListUsers(RetryPolicy retryPolicy = null)
        {
            //Url: /User/ListUsers

            ValidateIsConnected();
            retryPolicy = GetRetryPolicy(retryPolicy);

            var url = new Uri(this.serviceUrl, "/User/ListUsers?showInactive=true");
            
            for (int attempt = 1; attempt <= retryPolicy.MaxAttempts; attempt++)
            {
                try
                {
                    var listUsers = new List<User>();

                    var client = new Utils.JsonWebClient(this.UserAgent);
                    client.TimeOutMilliseconds = retryPolicy.TimeOutSeconds * 1000;

                    if (this.authCookies != null)
                        client.Cookies.Add(this.authCookies);

                    if (this.authAccessKey != null)
                        client.Headers.Add("__identifier", this.authAccessKey);

                    string json = client.GetReturnString(url);

                    var responseUsersList = Utils.JsonHelper.Deserialize<List<ApiModels.UserApi>>(json);

                    foreach (var userResponse in responseUsersList)
                        listUsers.Add(new User(userResponse));

                    listUsers = listUsers.OrderBy(a => a.UserID).ToList();

                    return listUsers; //Success
                }
                catch (System.Exception ex)
                {
                    if (Utils.Application.IsExceptionWithoutRetry(ex))
                        throw;

                    if (attempt == retryPolicy.MaxAttempts)
                        throw new TaskrowException(string.Format("Error listing users after {0} attempt(s) -- Url: {1} -- Error: {2} -- TimeOut: {3} seconds", retryPolicy.MaxAttempts, url.ToString(), ex.Message, retryPolicy.TimeOutSeconds), ex);
                }
            }

            throw new TaskrowException("Unexpected error in attempts control");
        }

        #endregion

        #region GetUserDetail

        public UserDetail GetUserDetail(int userID, RetryPolicy retryPolicy = null)
        {
            //Url: /User/UserDetail?userID=3564

            if (userID == 0)
                throw new ArgumentException(nameof(userID));

            ValidateIsConnected();
            retryPolicy = GetRetryPolicy(retryPolicy);

            var url = new Uri(this.serviceUrl, string.Format("/User/UserDetail?userID={0}", userID));

            for (int attempt = 1; attempt <= retryPolicy.MaxAttempts; attempt++)
            {
                try
                {
                    var client = new Utils.JsonWebClient(this.UserAgent);
                    client.TimeOutMilliseconds = retryPolicy.TimeOutSeconds * 1000;

                    if (this.authCookies != null)
                        client.Cookies.Add(this.authCookies);

                    if (this.authAccessKey != null)
                        client.Headers.Add("__identifier", this.authAccessKey);

                    string json = client.GetReturnString(url);

                    var userDetailresponse = Utils.JsonHelper.Deserialize<ApiModels.UserDetailResponse>(json);

                    var user = new UserDetail(userDetailresponse);
                    return user; //Success
                }
                catch (System.Exception ex)
                {
                    if (Utils.Application.IsExceptionWithoutRetry(ex))
                        throw;

                    if (attempt == retryPolicy.MaxAttempts)
                        throw new TaskrowException(string.Format("Error getting user after {0} attempt(s) -- Url: {1} -- Error: {2} -- TimeOut: {3} seconds", retryPolicy.MaxAttempts, url.ToString(), ex.Message, retryPolicy.TimeOutSeconds), ex);
                }
            }

            throw new TaskrowException("Unexpected error in attempts control");
        }

        #endregion

        #region ListGroups

        public List<Group> ListGroups(RetryPolicy retryPolicy = null)
        {
            //Url: /Administrative/ListGroups?groupTypeID=2

            ValidateIsConnected();
            retryPolicy = GetRetryPolicy(retryPolicy);

            var url = new Uri(this.serviceUrl, "/Administrative/ListGroups?groupTypeID=2");

            for (int attempt = 1; attempt <= retryPolicy.MaxAttempts; attempt++)
            {
                try
                {
                    var client = new Utils.JsonWebClient(this.UserAgent);
                    client.TimeOutMilliseconds = retryPolicy.TimeOutSeconds * 1000;

                    if (this.authCookies != null)
                        client.Cookies.Add(this.authCookies);

                    if (this.authAccessKey != null)
                        client.Headers.Add("__identifier", this.authAccessKey);

                    var json = client.GetReturnString(url);

                    var groupsResponse = Utils.JsonHelper.Deserialize<ApiModels.GroupListResponseApi>(json);

                    var listGroups = new List<Group>();
                    foreach (var groupApi in groupsResponse.Groups)
                        listGroups.Add(new Group(groupApi));
                    
                    return listGroups; //Success
                }
                catch (System.Exception ex)
                {
                    if (Utils.Application.IsExceptionWithoutRetry(ex))
                        throw;

                    if (attempt == retryPolicy.MaxAttempts)
                        throw new TaskrowException(string.Format("Error listing groups after {0} attempts(s) -- url: {1} -- Error: {2} -- TimeOut: {3} seconds", retryPolicy.MaxAttempts, url.ToString(), ex.Message, retryPolicy.TimeOutSeconds), ex);
                }
            }

            throw new TaskrowException("Unexpected error in attempts control");
        }

        #endregion

        #region ListTasksByGroup

        public List<Task> ListTasksByGroup(int groupID, int? userID = null, RetryPolicy retryPolicy = null)
        {
            //Url: /Dashboard/TasksByGroup?groupID=421&hierarchyEnabled=true&userID=3564&closedDays=20&context=1

            if (groupID == 0)
                throw new ArgumentException(nameof(groupID));
            
            if (userID.HasValue && userID == 0)
                throw new ArgumentException(nameof(userID));

            ValidateIsConnected();
            retryPolicy = GetRetryPolicy(retryPolicy);

            Uri url;
            if (!userID.HasValue)
                url = new Uri(this.serviceUrl, string.Format("/Dashboard/TasksByGroup?groupID={0}&hierarchyEnabled=true&closedDays=20&context=1", groupID));
            else
                url = new Uri(this.serviceUrl, string.Format("/Dashboard/TasksByGroup?groupID={0}&userID={1}&hierarchyEnabled=true&closedDays=20&context=1", groupID, userID.Value));
            
            for (int attempt = 1; attempt <= retryPolicy.MaxAttempts; attempt++)
            {
                try
                {
                    var client = new Utils.JsonWebClient(this.UserAgent);
                    client.TimeOutMilliseconds = retryPolicy.TimeOutSeconds * 1000;

                    if (this.authCookies != null)
                        client.Cookies.Add(this.authCookies);

                    if (this.authAccessKey != null)
                        client.Headers.Add("__identifier", this.authAccessKey);

                    string json = client.GetReturnString(url);

                    var response = Utils.JsonHelper.Deserialize<ApiModels.TasksByGroupResponseApi>(json);

                    var listTasks = new List<Task>();

                    foreach (var taskResponse in response.Entity.OpenTasks)
                        listTasks.Add(new Task(taskResponse, TaskSituation.Open));

                    foreach (var taskResponse in response.Entity.ClosedTasks)
                        listTasks.Add(new Task(taskResponse, TaskSituation.Closed));

                    return listTasks; //Success
                }
                catch (System.Exception ex)
                {
                    if (Utils.Application.IsExceptionWithoutRetry(ex))
                        throw;

                    if (attempt == retryPolicy.MaxAttempts)
                        throw new TaskrowException(string.Format("Error listing tasks from group after {0} attempts(s) -- url: {1} -- Error: {2} -- TimeOut: {3} seconds", retryPolicy.MaxAttempts, url.ToString(), ex.Message, retryPolicy.TimeOutSeconds), ex);
                }
            }

            throw new TaskrowException("Unexpected error in attempts control");
        }

        #endregion

        #region GetTaskDetail

        public TaskDetail GetTaskDetail(TaskReference taskReference, RetryPolicy retryPolicy = null)
        {
            //Url: /Task/TaskDetail?clientNickname={clientNickname}&jobNumber={jobNumber}&taskNumber={taskNumber}

            ValidateIsConnected();
            retryPolicy = GetRetryPolicy(retryPolicy);

            Uri url = new Uri(this.serviceUrl, string.Format("/Task/TaskDetail?jobNumber={0}&taskNumber={1}&clientNickname={2}", taskReference.JobNumber, taskReference.TaskNumber, taskReference.ClientNickname));

            for (int attempt = 1; attempt <= retryPolicy.MaxAttempts; attempt++)
            {
                try
                {
                    var client = new Utils.JsonWebClient(this.UserAgent);

                    if (this.authCookies != null)
                        client.Cookies.Add(this.authCookies);

                    if (this.authAccessKey != null)
                        client.Headers.Add("__identifier", this.authAccessKey);
                                        
                    var json = client.GetReturnString(url);

                    var response = Utils.JsonHelper.Deserialize<ApiModels.TaskDetailResponseApi>(json);
                    
                    var taskDetail = new TaskDetail(response.TaskData, response.JobData);
                    return taskDetail;
                }
                catch (System.Exception ex)
                {
                    if (Utils.Application.IsExceptionWithoutRetry(ex))
                        throw;

                    if (attempt == retryPolicy.MaxAttempts)
                        throw new TaskrowException(string.Format("Error getting Task Detail after {0} attempts(s) -- Url: {1} -- Error: {2} -- TimeOut: {3} seconds", retryPolicy.MaxAttempts, url.ToString(), ex.Message, retryPolicy.TimeOutSeconds), ex);
                }
            }

            throw new TaskrowException("Unexpected error in attempts control");
        }

        #endregion

        #region SaveTask (not implemented)

        public void SaveTask(SaveTaskRequest request, RetryPolicy retryPolicy = null)
        {
            //Url: /Task/SaveTask
            
            ValidateIsConnected();
            retryPolicy = GetRetryPolicy(retryPolicy);

            var url = new Uri(this.ServiceUrl, "/Task/SaveTask");

            for (int attempt = 1; attempt <= retryPolicy.MaxAttempts; attempt++)
            {
                try
                {
                    var client = new Utils.JsonWebClient(this.UserAgent);
                    client.TimeOutMilliseconds = retryPolicy.TimeOutSeconds * 1000;

                    if (this.authCookies != null)
                        client.Cookies.Add(this.authCookies);

                    if (this.authAccessKey != null)
                        client.Headers.Add("__identifier", this.authAccessKey);

                    throw new System.NotImplementedException();

                    /*
                    //client.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                    client.Host = this.ServiceUrl.Host;
                    client.Origin = this.ServiceUrl.ToString();
                    client.Referer = this.ServiceUrl.ToString();
                    client.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36";

                    var values = new System.Collections.Specialized.NameValueCollection();
                    values.Add("jobNumber", request.JobNumber.ToString());
                    values.Add("clientNickName", request.ClientNickName);
                    values.Add("lastTaskItemID", request.LastTaskItemID.ToString());
                    values.Add("TaskID", request.TaskID.ToString());
                    values.Add("MemberListString", request.MemberListString);
                    values.Add("TaskNumber", request.TaskNumber.ToString());

                    if (!string.IsNullOrEmpty(request.RowVersion))
                        values.Add("RowVersion", request.RowVersion);

                    values.Add("TaskTitle", request.TaskTitle);
                    values.Add("TaskItemComment", request.TaskItemComment);
                    values.Add("OwnerUserID", request.OwnerUserID.ToString());
                    values.Add("SpentTime", request.SpentTime.ToString());
                    values.Add("DueDate", request.DueDate.ToString("yyyy-MM-dd"));
                    values.Add("PercentComplete", request.PercentComplete.ToString());

                    var jObject = client.PostValuesReturnJObject(url, values);

                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (var key in values.AllKeys)
                            sb.AppendFormat("{0}={1}\r\n", key, values[key]);

                        string jsonRequest = sb.ToString();

                        string jsonReponse = jObject.ToString();

                        System.Diagnostics.Debug.WriteLine(string.Format("Taskrow -- url= {0}", url));
                        System.Diagnostics.Debug.WriteLine(string.Format("Taskrow -- request= {0}", jsonRequest));
                        System.Diagnostics.Debug.WriteLine(string.Format("Taskrow -- response= {0}", jsonReponse));
                    }

                    bool success = Convert.ToBoolean(jObject["Success"]);
                    string message = jObject["Message"].ToString();

                    if (!success)
                        throw new TaskrowException(message);

                    return; //Success
                    */
                }
                catch (System.Exception ex)
                {
                    if (Utils.Application.IsExceptionWithoutRetry(ex))
                        throw;

                    if (attempt == retryPolicy.MaxAttempts)
                        throw new TaskrowException(string.Format("Error saving task after {0} attempt(s) -- Url: {1} -- Error: {2} -- TimeOut: {3} seconds", retryPolicy.MaxAttempts, url.ToString(), ex.Message, retryPolicy.TimeOutSeconds), ex);
                }
            }

            throw new TaskrowException("Unexpected error in attempts control");
        }

        #endregion
    }
}
