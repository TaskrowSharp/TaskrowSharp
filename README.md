# TaskrowSharp - Simple Taskrow Client for .NET

### License: MIT

Taskrow is a software for organizing teamwork.

https://taskrow.com/

TaskrowSharp is a client that allows .NET applications communicate with Taskrow system to execute this operations:
.	Get users and groups
.	Get task details
.	Comment, forward and close tasks


## 1- Create a Taskrow account

To use TaskrowSharp client you need a Taskrow account, to create a account go to website:

https://taskrow.com/


## 2- Create a Taskrow AccessKey

The second step is create a AccessKey to use in client, to create this, do the following:

1.  Go to your Taskrow account (https://yourdomain.taskrow.com)
2.  Log in using the e-mail of user you want to use in integration with API (you can use your, or create a new user)
3.  Go to User List and acces User page
4.  Click the button to create a new Mobile API Key



## 3- Add nuget reference to your project

(available soon)



## 4- Examples

### 4.1- List users

```csharp
var httpClient = new HttpClient(); 
//TIP: HttpClient is intended to be instantiated once and reused throughout the life of an application, more info: https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient
//TIP: You can use a retry policy with Poly, more info: https://learn.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/implement-http-call-retries-exponential-backoff-polly

var taskrowClient = new TaskrowSharp.TaskrowClient(new Uri("https://yourdomain.taskrow.com"), "AccessKey_xxxxxxxxxxxxx", httpClient);
var users = await taskrowClient.UserListAsync();
```


## 4.2- Forward Task

```csharp
var taskrowClient = new TaskrowSharp.TaskrowClient(new Uri("https://yourdomain.taskrow.com"), "AccessKey_xxxxxxxxxxxxx", httpClient);
var users = await taskrowClient.UserListAsync();

var taskReference = new TaskReference("client", 12, 1235);
var taskResponse = await _taskrowClient.TaskDetailGetAsync(taskReference);
var task = taskResponse.TaskData;

var taskComment = "Task forwarded";
int ownerUserID = users.First().UserID;
var dueDate = DateTime.Now.Date;

var request = new TaskSaveRequest(taskResponse.JobData.Client.ClientNickName, taskResponse.JobData.JobNumber, task.TaskNumber, task.TaskID)
{
    TaskTitle = task.TaskTitle,
    TaskItemComment = taskComment,
    OwnerUserID = ownerUserID,
    RowVersion = task.RowVersion,
    LastTaskItemID = task.NewTaskItems.Last().TaskItemID,
    DueDate = dueDate.ToString("yyyy-MM-dd"),
    EffortEstimation = task.EffortEstimation
};

var response = await _taskrowClient.TaskSaveAsync(request);
```


## 5- Additional features

### 5.1 - OnApiCallExecuted event

```csharp
var taskrowClient = new TaskrowSharp.TaskrowClient(new Uri("https://yourdomain.taskrow.com"), "AccessKey_xxxxxxxxxxxxx", httpClient);
taskrowClient.OnApiCallExecuted += (HttpMethod httpMethod, Uri fullUrl, HttpStatusCode httpStatusCode, bool isSuccess, string? jsonRequest, string? jsonResponse, long elapsedMilliseconds) =>
            {
                System.Diagnostics.Debug.WriteLine($"API Call - {httpMethod} {fullUrl} -- HttpStatus: {(int)httpStatusCode}");
            };
var users = await taskrowClient.UserListAsync();
```


## 6- Explore source code / debug

Open solution "TaskrowSharp.sln" in Visual Studio 2022

To Run Tests, you need to create a file "main.json"
1.	create a file "main.json" in folder: \TaskrowSharp.IntegrationTests\config\ using the example file
2.	add your Taskrow credentials to this file
3.	change main.json file configuration to "Copy if newer"

