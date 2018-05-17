# TaskrowSharp - Simple Taskrow Client for .NET

### License: MIT


## Nuget package

(available soon)


## Basic Example

```csharp
var taskrowClient = new TaskrowSharp.TaskrowClient();
taskrowClient.Connect("https://yourdomain.taskrow.com", "AccessKey_xxxxslwlqlqlwqlql23234jewjewj");
var users = taskrowClient.ListUsers();
```

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

