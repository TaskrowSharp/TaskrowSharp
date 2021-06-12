using System;
using System.Linq;
using Xunit;

namespace TaskrowSharp.IntegrationTests
{
    public class TaskTests : BaseTest
    {
        private readonly TaskrowClient taskrowClient;

        public TaskTests()
        {
            taskrowClient = GetTaskrowClient();
        }

        [Fact]
        public void Task_ListTasksByGroup_OK()
        {
            var groupTest = taskrowClient.ListGroups().First();

            var tasks = taskrowClient.ListTasksByGroup(groupTest.GroupID);
            Assert.True(tasks.Count > 0);
        }

        [Fact]
        public void Task_Forward_OK()
        {
            var configuration = GetConfigurationFile();

            if (configuration.ForwardTaskTests == null || configuration.ForwardTaskTests.Count == 0)
                throw new System.InvalidOperationException("No \"forwardTaskTests\" items configured in configuration file");

            var users = taskrowClient.ListUsers();
            
            foreach (var test in configuration.ForwardTaskTests)
            {
                var user1 = users.Where(a => a.MainEmail.Equals(test.User1Email, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                if (user1 == null)
                    throw new System.InvalidOperationException($"User e-mail=\"{test.User1Email}\" not found");
                if (!user1.Active)
                    throw new System.InvalidOperationException($"User e-mail=\"{test.User1Email}\" is inactive");

                var user2 = users.Where(a => a.MainEmail.Equals(test.User2Email, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                if (user2 == null)
                    throw new System.InvalidOperationException($"User e-mail=\"{test.User2Email}\" not found");
                if (!user2.Active)
                    throw new System.InvalidOperationException($"User e-mail=\"{test.User2Email}\" is inactive");

                if (user1.UserID == user2.UserID)
                    throw new System.InvalidOperationException($"Error in \"forwardTaskTests\" configuration, user1 and user2 are the same");

                var taskReference = new TaskReference(test.TaskUrl);

                var task = taskrowClient.GetTaskDetail(taskReference);
                if (task == null)
                    throw new System.InvalidOperationException($"Task {test.TaskUrl} not found");

                var taskComment = $"Task forwarded on {DateTime.Now:yyyy-MM-dd HH:mm:ss}";
                int ownerUserID = task.Owner.UserID;
                var dueDate = (task.DueDate >= DateTime.Now.Date ? task.DueDate : DateTime.Now.Date);
                
                if (task.Owner.UserID == user1.UserID)
                    ownerUserID = user2.UserID;
                else if (task.Owner.UserID == user2.UserID)
                    ownerUserID = user1.UserID;
                else
                    throw new System.InvalidOperationException($"Task {test.TaskUrl}, has a unexpected owner");

                var request = new SaveTaskRequest(task.TaskID, task.ClientNickname, task.JobNumber, task.TaskNumber, task.TaskTitle, taskComment, ownerUserID,
                    task.RowVersion, task.TaskItems.Last().TaskItemID, dueDate, 0, task.EffortEstimationMinutes);

                var response = taskrowClient.SaveTask(request);

                if (!response.Success)
                    throw new System.InvalidOperationException($"Error saving task: {response.Message}");
            }

            Assert.True(true);
        }
    }
}
