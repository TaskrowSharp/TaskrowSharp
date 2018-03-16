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
        private Uri ServiceUrl = null;
        private CookieCollection AuthCookies;
        private string AuthAccessKey;

        public string UserAgent { get; set; }

        public RetryPolicy RetryPolicy { get; set; }

        public bool StatusConnected
        {
            get { return (this.ServiceUrl != null); }
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
                    this.ServiceUrl = null;

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

                    this.ServiceUrl = serviceUrl;
                    this.AuthCookies = cookies;
                    this.AuthAccessKey = null;

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
                    this.ServiceUrl = null;

                    Uri url = new Uri(serviceUrl, "/User/MyInfo");

                    var client = new Utils.JsonWebClient(this.UserAgent);
                    client.AllowAutoRedirect = true;
                    client.TimeOutMilliseconds = retryPolicy.TimeOutSeconds * 1000;
                    client.Headers.Add("__identifier", credential.AccessKey);
                    
                    string json = client.GetReturnString(url);

                    //When password is invalid, Taskrow returns html from login page
                    if (json.IndexOf("<html", StringComparison.CurrentCultureIgnoreCase) != -1)
                        throw new AuthenticationException("Invalid AccessKey");

                    this.ServiceUrl = serviceUrl;
                    this.AuthCookies = null;
                    this.AuthAccessKey = credential.AccessKey;

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

            this.ServiceUrl = null;
            this.AuthCookies = null;
            this.AuthAccessKey = null;
        }

        public void KeepAlive()
        {
            KeepAlive(this.RetryPolicy);
        }

        public void KeepAlive(RetryPolicy retryPolicy)
        {
            //Url: /main/keepalive

            ValidateIsConnected();

            var url = new Uri(this.ServiceUrl, "/main/keepalive");

            for (int attempt = 1; attempt <= retryPolicy.MaxAttempts; attempt++)
            {
                try
                {
                    var client = new Utils.JsonWebClient(this.UserAgent);
                    client.TimeOutMilliseconds = 5000; //5 seconds
                    client.Cookies.Add(this.AuthCookies);

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

        public List<User> ListUsers()
        {
            return ListUsers(this.RetryPolicy);
        }

        public List<User> ListUsers(RetryPolicy retryPolicy)
        {
            //Url: /User/TeamHome

            ValidateIsConnected();

            var url = new Uri(this.ServiceUrl, "/User/TeamHome");
            
            for (int attempt = 1; attempt <= retryPolicy.MaxAttempts; attempt++)
            {
                try
                {
                    var listUsers = new List<User>();

                    var client = new Utils.JsonWebClient(this.UserAgent);
                    client.TimeOutMilliseconds = retryPolicy.TimeOutSeconds * 1000;

                    if (this.AuthCookies != null)
                        client.Cookies.Add(this.AuthCookies);

                    if (this.AuthAccessKey != null)
                        client.Headers.Add("__identifier", this.AuthAccessKey);

                    string json = client.GetReturnString(url);

                    var userListResponse = Utils.JsonHelper.Deserialize<ApiModels.UserListApiResponse>(json);

                    foreach (var userResponse in userListResponse.Users)
                        listUsers.Add(new User(userResponse));
                    
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

        public UserDetail GetUserDetail(int userID)
        {
            return GetUserDetail(userID, this.RetryPolicy);
        }

        public UserDetail GetUserDetail(int userID, RetryPolicy retryPolicy)
        {
            //Url: /User/UserDetail?userID=3564

            if (userID == 0)
                throw new ArgumentException(nameof(userID));

            ValidateIsConnected();

            var url = new Uri(this.ServiceUrl, string.Format("/User/UserDetail?userID={0}", userID));

            for (int attempt = 1; attempt <= retryPolicy.MaxAttempts; attempt++)
            {
                try
                {
                    var client = new Utils.JsonWebClient(this.UserAgent);
                    client.TimeOutMilliseconds = retryPolicy.TimeOutSeconds * 1000;

                    if (this.AuthCookies != null)
                        client.Cookies.Add(this.AuthCookies);

                    if (this.AuthAccessKey != null)
                        client.Headers.Add("__identifier", this.AuthAccessKey);

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

        public List<Group> ListGroups()
        {
            return ListGroups(this.RetryPolicy);
        }

        public List<Group> ListGroups(RetryPolicy retryPolicy)
        {
            //Url: /Administrative/ListGroups?groupTypeID=2

            ValidateIsConnected();

            var url = new Uri(this.ServiceUrl, "/Administrative/ListGroups?groupTypeID=2");

            for (int attempt = 1; attempt <= retryPolicy.MaxAttempts; attempt++)
            {
                try
                {
                    var client = new Utils.JsonWebClient(this.UserAgent);
                    client.TimeOutMilliseconds = retryPolicy.TimeOutSeconds * 1000;

                    if (this.AuthCookies != null)
                        client.Cookies.Add(this.AuthCookies);

                    if (this.AuthAccessKey != null)
                        client.Headers.Add("__identifier", this.AuthAccessKey);

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

        public List<Task> ListTasksByGroup(int groupID)
        {
            return ListTasksByGroup(groupID, null, this.RetryPolicy);
        }

        public List<Task> ListTasksByGroup(int groupID, int userID)
        {
            return ListTasksByGroup(groupID, userID, this.RetryPolicy);
        }

        public List<Task> ListTasksByGroup(int groupID, int? userID, RetryPolicy retryPolicy)
        {
            //Url: /Dashboard/TasksByGroup?groupID=421&hierarchyEnabled=true&userID=3564&closedDays=20&context=1

            if (groupID == 0)
                throw new ArgumentException(nameof(groupID));
            
            if (userID.HasValue && userID == 0)
                throw new ArgumentException(nameof(userID));

            ValidateIsConnected();

            Uri url;
            if (!userID.HasValue)
                url = new Uri(this.ServiceUrl, string.Format("/Dashboard/TasksByGroup?groupID={0}&hierarchyEnabled=true&closedDays=20&context=1", groupID));
            else
                url = new Uri(this.ServiceUrl, string.Format("/Dashboard/TasksByGroup?groupID={0}&userID={1}&hierarchyEnabled=true&closedDays=20&context=1", groupID, userID.Value));
            
            for (int attempt = 1; attempt <= retryPolicy.MaxAttempts; attempt++)
            {
                try
                {
                    var client = new Utils.JsonWebClient(this.UserAgent);
                    client.TimeOutMilliseconds = retryPolicy.TimeOutSeconds * 1000;

                    if (this.AuthCookies != null)
                        client.Cookies.Add(this.AuthCookies);

                    if (this.AuthAccessKey != null)
                        client.Headers.Add("__identifier", this.AuthAccessKey);

                    string json = client.GetReturnString(url);

                    var response = Utils.JsonHelper.Deserialize<ApiModels.TasksByGroupResponseApi>(json);

                    var listTasks = new List<Task>();

                    foreach (var taskResponse in response.Entity.OpenTasks)
                        listTasks.Add(new Task(taskResponse, TaskStatus.Open));

                    foreach (var taskResponse in response.Entity.ClosedTasks)
                        listTasks.Add(new Task(taskResponse, TaskStatus.Closed));

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

        #region GetTaskDetail (not implemented)

        public TaskDetail GetTaskDetail(int jobNumber, int taskNumber, string clientNickName)
        {
            return GetTaskDetail(jobNumber, taskNumber, clientNickName, this.RetryPolicy);
        }

        public TaskDetail GetTaskDetail(int jobNumber, int taskNumber, string clientNickName, RetryPolicy retryPolicy)
        {
            //Url: /Task/TaskDetail?clientNickName=[clientName]&jobNumber=[job number]&taskNumber=[task number]

            ValidateIsConnected();

            Uri url = new Uri(this.ServiceUrl, string.Format("/Task/TaskDetail?jobNumber={0}&taskNumber={1}&clientNickName={2}", jobNumber, taskNumber, clientNickName));

            for (int attempt = 1; attempt <= retryPolicy.MaxAttempts; attempt++)
            {
                try
                {
                    var client = new Utils.JsonWebClient(this.UserAgent);

                    if (this.AuthCookies != null)
                        client.Cookies.Add(this.AuthCookies);

                    if (this.AuthAccessKey != null)
                        client.Headers.Add("__identifier", this.AuthAccessKey);

                    //var jObject = client.GetReturnJObject(url);

                    //var taskData = jObject["TaskData"];
                    //var jobData = jObject["JobData"];

                    //var taskDetail = GetTaskDetailFromJson(taskData, jobData);

                    var json = client.GetReturnString(url);

                    //TaskDetail taskDetail = null;
                    //return taskDetail; //Success

                    throw new System.NotImplementedException();
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

        //private TaskDetail GetTaskDetailFromJson(Newtonsoft.Json.Linq.JToken taskData, Newtonsoft.Json.Linq.JToken jobData)
        //{
        //    var taskDetail = new TaskDetail();

        //    taskDetail.TaskID = Utils.Parser.ToInt32(taskData["TaskID"]);
        //    taskDetail.TaskNumber = Utils.Parser.ToInt32(taskData["TaskNumber"]);
        //    taskDetail.TaskTitle = taskData["TaskTitle"].ToString();
        //    taskDetail.MemberListString = taskData["MemberListString"].ToString();
        //    taskDetail.RowVersion = taskData["RowVersion"].ToString();
        //    taskDetail.DueDate = Convert.ToDateTime(taskData["DueDate"].ToString());

        //    taskDetail.TaskItems = new List<TaskItem>();
        //    foreach (var item in taskData["NewTaskItems"])
        //    {
        //        var taskItem = new TaskItem();
        //        taskItem.TaskItemID = Utils.Parser.ToInt32(item["TaskItemID"]);
        //        taskItem.OldOwnerUserID = Utils.Parser.ToInt32(item["OldOwnerUserID"]);
        //        taskItem.OldOwnerName = item["OldOwnerName"].ToString();
        //        taskItem.NewOwnerUserID = Utils.Parser.ToInt32(item["NewOwnerUserID"]);
        //        taskItem.NewOwnerName = item["NewOwnerName"].ToString();
        //        taskItem.TaskItemComment = item["TaskItemComment"].ToString();
        //        taskDetail.TaskItems.Add(taskItem);
        //    }

        //    taskDetail.JobID = Utils.Parser.ToInt32(jobData["JobID"]);
        //    taskDetail.JobNumber = Utils.Parser.ToInt32(jobData["JobNumber"]);
        //    taskDetail.JobTitle = jobData["JobTitle"].ToString();

        //    var clientData = jobData["Client"];
        //    taskDetail.ClientNickName = clientData["ClientNickName"].ToString();

        //    taskDetail.Tags = new List<TaskTag>();
        //    foreach (var item in taskData["Tags"])
        //        taskDetail.Tags.Add(new TaskTag() { TaskTagID = Utils.Parser.ToInt32(item["TaskTagID"]), TagTitle = item["TagTitle"].ToString() });

        //    taskDetail.SubTasks = new List<SubTask>();
        //    if (taskData["Subtasks"] != null)
        //    {
        //        foreach (var item in taskData["Subtasks"])
        //        {
        //            var subTask = new SubTask();
        //            subTask.SubtaskID = Utils.Parser.ToInt32(item["SubtaskID"]);
        //            subTask.TaskID = Utils.Parser.ToInt32(item["TaskID"]);
        //            subTask.ChildTaskID = Utils.Parser.ToInt32(item["ChildTaskID"]);
        //            subTask.Title = item["Title"].ToString();

        //            var childTaskData = item["ChildTask"];
        //            if (childTaskData != null)
        //            {
        //                TaskDetail childTask = GetTaskDetailFromJson(childTaskData, jobData);
        //                subTask.ChildTask = childTask;
        //            }

        //            taskDetail.SubTasks.Add(subTask);
        //        }
        //    }

        //    return taskDetail;
        //}

        #endregion

        #region SaveTask (not implemented)

        //public void SaveTask(SaveTaskRequest request, int maxAttempts = 1, int timeOutSeconds = 120)
        //{
        //    //Url: /Task/SaveTask

        //    ValidateStatusConnected();

        //    var url = new Uri(this.ServiceUrl, "/Task/SaveTask");

        //    for (int attempt = 1; attempt <= maxAttempts; attempt++)
        //    {
        //        try
        //        {
        //            var client = new Utils.JsonWebClient(this.UserAgent);
        //            client.TimeOutMilliseconds = timeOutSeconds * 1000;
        //            client.Cookies.Add(this.Cookies);
        //            client.AllowAutoRedirect = false;

        //            //client.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
        //            client.Host = this.ServiceUrl.Host;
        //            client.Origin = this.ServiceUrl.ToString();
        //            client.Referer = this.ServiceUrl.ToString();
        //            client.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36";

        //            var values = new System.Collections.Specialized.NameValueCollection();
        //            values.Add("jobNumber", request.JobNumber.ToString());
        //            values.Add("clientNickName", request.ClientNickName);
        //            values.Add("lastTaskItemID", request.LastTaskItemID.ToString());
        //            values.Add("TaskID", request.TaskID.ToString());
        //            values.Add("MemberListString", request.MemberListString);
        //            values.Add("TaskNumber", request.TaskNumber.ToString());

        //            if (!string.IsNullOrEmpty(request.RowVersion))
        //                values.Add("RowVersion", request.RowVersion);

        //            values.Add("TaskTitle", request.TaskTitle);
        //            values.Add("TaskItemComment", request.TaskItemComment);
        //            values.Add("OwnerUserID", request.OwnerUserID.ToString());
        //            values.Add("SpentTime", request.SpentTime.ToString());
        //            values.Add("DueDate", request.DueDate.ToString("yyyy-MM-dd"));
        //            values.Add("PercentComplete", request.PercentComplete.ToString());

        //            var jObject = client.PostValuesReturnJObject(url, values);

        //            if (System.Diagnostics.Debugger.IsAttached)
        //            {
        //                StringBuilder sb = new StringBuilder();
        //                foreach (var key in values.AllKeys)
        //                    sb.AppendFormat("{0}={1}\r\n", key, values[key]);

        //                string jsonRequest = sb.ToString();

        //                string jsonReponse = jObject.ToString();

        //                System.Diagnostics.Debug.WriteLine(string.Format("Taskrow -- url= {0}", url));
        //                System.Diagnostics.Debug.WriteLine(string.Format("Taskrow -- request= {0}", jsonRequest));
        //                System.Diagnostics.Debug.WriteLine(string.Format("Taskrow -- response= {0}", jsonReponse));
        //            }

        //            bool success = Convert.ToBoolean(jObject["Success"]);
        //            string message = jObject["Message"].ToString();

        //            if (!success)
        //                throw new TaskrowException(message);

        //            return; //Success
        //        }
        //        catch (System.Exception ex)
        //        {
        //            if (attempt == maxAttempts)
        //                throw new TaskrowException(string.Format("Error saving task after {0} attempts(s) -- url: {1} -- Error: {2} -- TimeOut: {3} seconds", maxAttempts, url.ToString(), ex.Message, timeOutSeconds), ex);
        //        }
        //    }

        //    throw new TaskrowException("Unexpected error in attempts control");
        //}

        #endregion
    }
}
