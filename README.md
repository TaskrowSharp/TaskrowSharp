# TaskrowSharp - Simple Taskrow Client for .NET

`Package License: MIT`

[Taskrow](https://taskrow.com/) is software for organizing your team's work.

TaskrowSharp is a client that facilitates the integration of .NET applications with the Taskrow API.

Using TaskrowSharp you can perform operations like:

- Get users and groups
- Get task details
- Comment, forward and close tasks
- Create invoice

---

## 1. How to use the TaskrowSharp lib

## 1.1. Create a Taskrow AccessKey

You need a Taskrow AccessKey to use in client, to create this, do the following:

1.  Go to your Taskrow account (https://yourdomain.taskrow.com)
2.  Log in using the e-mail of user you want to use in integration with API (you can use your, or create a new user)
3.  Go to User List and acces User page
4.  Click the button to create a new Mobile API Key

### 1.2. Add the dependency

Add the referece to "TaskrowSharp.dll" in your project, get this file from directory: "binaries".

A version of this package should be available on NuGet in the future.

### 1.3. Connect

Use your Taskrow AccessKey to connect to Taskrow API:

```csharp
var httpClient = new HttpClient(); 
//TIP: HttpClient is intended to be instantiated once and reused throughout the life of an application, more info: https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient
//TIP: You can use a retry policy with Poly, more info: https://learn.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/implement-http-call-retries-exponential-backoff-polly

var taskrowClient = new TaskrowSharp.TaskrowClient(new Uri("https://yourdomain.taskrow.com"), "AccessKey_xxxxxxxxxxxxx", httpClient);
```

---

## 2. Examples

### 2.1. List users

```csharp
var taskrowClient = new TaskrowSharp.TaskrowClient(new Uri("https://yourdomain.taskrow.com"), "AccessKey_xxxxxxxxxxxxx", httpClient);
var users = await taskrowClient.UserListAsync();
```


## 2.2. Forward Task

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

---

## 3. Additional features

### 3.1. OnApiCallExecuted event

```csharp
var taskrowClient = new TaskrowSharp.TaskrowClient(new Uri("https://yourdomain.taskrow.com"), "AccessKey_xxxxxxxxxxxxx", httpClient);
taskrowClient.OnApiCallExecuted += (HttpMethod httpMethod, Uri fullUrl, HttpStatusCode httpStatusCode, bool isSuccess, string? jsonRequest, string? jsonResponse, long elapsedMilliseconds) =>
            {
                System.Diagnostics.Debug.WriteLine($"API Call - {httpMethod} {fullUrl} -- HttpStatus: {(int)httpStatusCode}");
            };
var users = await taskrowClient.UserListAsync();
```

---

## 4. Explore source code / debug

Steps to run the code:

1. Open solution "TaskrowSharp.sln" in Visual Studio 2026
2. Create a file "main.json"
    - create a file "main.json" in folder: \TaskrowSharp.IntegrationTests\config\ using the example file
    - add your Taskrow credentials to this file
    - change main.json file configuration to "Copy if newer"
3. Build the solution (CTRL+SHIFT+B)
4. Run the tests

