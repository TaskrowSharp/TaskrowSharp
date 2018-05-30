# TaskrowSharp - Simple Taskrow Client for .NET

### License: MIT



## 1- Nuget package

(available soon)


## 2- Taskrow

Taskrow is system to control and organize the Tasks from Teams and Clients.

To create an account, access: http://taskrow.com/



## 3- AccessKey

To create a AccessKey in Taskrow:

1.  Go to your Taskrow account (https://yourdomain.taskrow.com)
2.  Log in using the e-mail of user you want to use in integration with API
3.  Go to user page from logged user
4.  Click the button to create a new Mobile API Key

This AccessKey can be used in Connect() method


## 4- Examples

### 4.1- Basic Example

```csharp
var taskrowClient = new TaskrowSharp.TaskrowClient(new Uri("https://yourdomain.taskrow.com"), "AccessKey_xxxxxxxxxxxxx");
var users = taskrowClient.ListUsers();
```


## 4.2- Forward Task

```csharp
var taskrowClient = new TaskrowSharp.TaskrowClient(new Uri("https://yourdomain.taskrow.com"), "AccessKey_xxxxxxxxxxxxx");
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

## 5- Changing code

Open solution "TaskrowSharp.sln" in Visual Studio 2017

To Run Tests, you need to create a file "main.json" in folder: \TaskrowSharp.IntegrationTests\config\
you should include the credentials of your Taskrow account in this file, follow the example file: example.json


