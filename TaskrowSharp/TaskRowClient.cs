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
        public string AccessKey { get; private set; }

        public Uri ServiceUrl { get; private set; }

        public string UserAgent { get; set; }

        public RetryPolicy RetryPolicy { get; set; }

        public TaskrowClient(Uri serviceUrl, string accessKey, RetryPolicy retryPolicy = null)
        {
            ValidateServiceUrl(serviceUrl);
            ValidateAccessKey(accessKey);

            this.ServiceUrl = serviceUrl;
            this.AccessKey = accessKey;

            this.UserAgent = string.Format("TaskrowSharp v{0}", Utils.Application.GetAppVersion());
            this.RetryPolicy = GetRetryPolicy(retryPolicy);
        }

        private RetryPolicy GetRetryPolicy(RetryPolicy retryPolicy)
        {
            if (retryPolicy != null)
                return retryPolicy;

            if (this.RetryPolicy != null)
                return this.RetryPolicy;

            return new RetryPolicy(1, 120); //Default RetryPolicy
        }

        private void ValidateAccessKey(string accessKey)
        {
            if (accessKey == null)
                throw new ArgumentNullException(nameof(accessKey));
            
            if (accessKey.Length < 20)
                throw new System.ArgumentException("Invalid AccessKey");
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
                
        #region ListUsers

        public List<User> ListUsers(RetryPolicy retryPolicy = null)
        {
            //Url: /User/ListUsers

            retryPolicy = GetRetryPolicy(retryPolicy);

            var url = new Uri(this.ServiceUrl, "/User/ListUsers?showInactive=true");
            
            for (int attempt = 1; attempt <= retryPolicy.MaxAttempts; attempt++)
            {
                try
                {
                    var listUsers = new List<User>();

                    var client = new Utils.JsonWebClient(this.UserAgent);
                    client.TimeOutMilliseconds = retryPolicy.TimeOutSeconds * 1000;
                    client.Headers.Add("__identifier", this.AccessKey);

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

            retryPolicy = GetRetryPolicy(retryPolicy);

            var url = new Uri(this.ServiceUrl, string.Format("/User/UserDetail?userID={0}", userID));

            for (int attempt = 1; attempt <= retryPolicy.MaxAttempts; attempt++)
            {
                try
                {
                    var client = new Utils.JsonWebClient(this.UserAgent);
                    client.TimeOutMilliseconds = retryPolicy.TimeOutSeconds * 1000;
                    client.Headers.Add("__identifier", this.AccessKey);

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

            retryPolicy = GetRetryPolicy(retryPolicy);

            var url = new Uri(this.ServiceUrl, "/Administrative/ListGroups?groupTypeID=2");

            for (int attempt = 1; attempt <= retryPolicy.MaxAttempts; attempt++)
            {
                try
                {
                    var client = new Utils.JsonWebClient(this.UserAgent);
                    client.TimeOutMilliseconds = retryPolicy.TimeOutSeconds * 1000;
                    client.Headers.Add("__identifier", this.AccessKey);

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

            retryPolicy = GetRetryPolicy(retryPolicy);

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
                    client.Headers.Add("__identifier", this.AccessKey);

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

            retryPolicy = GetRetryPolicy(retryPolicy);

            Uri url = new Uri(this.ServiceUrl, string.Format("/Task/TaskDetail?jobNumber={0}&taskNumber={1}&clientNickname={2}", taskReference.JobNumber, taskReference.TaskNumber, taskReference.ClientNickname));

            for (int attempt = 1; attempt <= retryPolicy.MaxAttempts; attempt++)
            {
                try
                {
                    var client = new Utils.JsonWebClient(this.UserAgent);
                    client.Headers.Add("__identifier", this.AccessKey);
                    
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

        #region SaveTask

        public SaveTaskResponse SaveTask(SaveTaskRequest request, RetryPolicy retryPolicy = null)
        {
            //Url: /Task/SaveTask

            retryPolicy = GetRetryPolicy(retryPolicy);

            var url = new Uri(this.ServiceUrl, "/Task/SaveTask");

            for (int attempt = 1; attempt <= retryPolicy.MaxAttempts; attempt++)
            {
                try
                {
                    var client = new Utils.JsonWebClient(this.UserAgent);
                    client.TimeOutMilliseconds = retryPolicy.TimeOutSeconds * 1000;
                    client.Headers.Add("__identifier", this.AccessKey);

                    var response = client.PostObjReturnObject<SaveTaskResponse>(url, request);
                    return response;
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
