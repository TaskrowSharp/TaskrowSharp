# TaskrowSharp - Simple Taskrow Client for .NET

### License: MIT

TaskrowSharp is a client that allows .NET applications communicate with Taskrow system to execute this operations:
.	Get users and groups
.	get tasks
.	Comment, forward and close tasks

Taskrow is a software for organizing teamwork.

https://taskrow.com/



## 1- Create Taskrow account

The first step to use TaskrowSharp client is create an account in Taskrow, so go to website:

https://taskrow.com/

an create free account using your e-mail.



## 2- Create a Taskrow AccessKey

The second step is create a AccessKey to use in client, to create this, do the following:

1.  Go to your Taskrow account (https://yourdomain.taskrow.com)
2.  Log in using the e-mail of user you want to use in integration with API (you can use your, or create a new user)
3.  Go to User List and acces User page
4.  Click the button to create a new Mobile API Key

This AccessKey will be used in Connect() method



## 3- Add nuget reference to your project

(available soon)



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
int ownerUserID = users.First().UserID;
var dueDate = DateTime.Now.Date;

var request = new SaveTaskRequest(task.TaskID, task.ClientNickname, task.JobNumber, task.TaskNumber, task.TaskTitle, taskComment, ownerUserID,
	task.RowVersion, task.TaskItems.Last().TaskItemID, dueDate, 0, 0, 0);

var response = taskrowClient.SaveTask(request);
```



## 5- Explore source code / debug

Open solution "TaskrowSharp.sln" in Visual Studio 2019

To Run Tests, you need to create a file "main.json"
1- create a file "main.json" in folder: \TaskrowSharp.IntegrationTests\config\ using the example file
2- add your Taskrow credentials to this file
3- change main.json file configuration to "Copy if newer"
