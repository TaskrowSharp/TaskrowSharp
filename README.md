# TaskrowSharp - Simple Taskrow Client for .NET

### License: MIT



## Nuget package

(available soon)


## Taskrow

Taskrow is system to control and organize the Tasks from Teams and Clients.

To create an account, access: http://taskrow.com/



## AccessKey

To create a AccessKey in Taskrow:

1.  Go to your Taskrow account (https://yourdomain.taskrow.com)
2.  Log in using the e-mail of user you want to use in integration with API
3.  Go to user page from logged user
4.  Click the button to create a new Mobile API Key

This AccessKey can be used in Connect() method



## Basic Example

```csharp
var taskrowClient = new TaskrowSharp.TaskrowClient();
taskrowClient.Connect("https://yourdomain.taskrow.com", "AccessKey_xxxxslwlqlqlwqlql23234jewjewj");
var users = taskrowClient.ListUsers();
```


## Forward Task

```csharp
var taskrowClient = new TaskrowSharp.TaskrowClient();
taskrowClient.Connect("https://yourdomain.taskrow.com", "AccessKey_xxxxslwlqlqlwqlql23234jewjewj");
var users = taskrowClient.ListUsers();

var taskReference = new TaskReference("client", 12, 1235);
var task = taskrowClient.GetTaskDetail(taskReference);

var taskComment = "Task forwarded";
int ownewUserID = users.First().UserID;
var dueDate = DateTime.Now.Date;

var request = new SaveTaskRequest(task.TaskID, task.ClientNickname, task.JobNumber, task.TaskNumber, task.TaskTitle, taskComment, ownewUserID,
	task.RowVersion, task.TaskItems.Last().TaskItemID, dueDate, 0, 0, 0);

var response = taskrowClient.SaveTask(request);
```

