using System;
using System.Linq;
using System.Threading.Tasks;
using TaskrowSharp.IntegrationTests.TestModels;
using TaskrowSharp.Models;
using Xunit;

namespace TaskrowSharp.IntegrationTests
{
    public class SaveTaskTests : BaseTest
    {
        private readonly TaskrowClient _taskrowClient;
        private readonly ConfigurationFile _configurationFile;

        public SaveTaskTests()
        {
            _taskrowClient = GetTaskrowClient();
            _configurationFile = GetConfigurationFile();
        }

        [Fact]
        public async Task Task_Forward_OK()
        {
            if (_configurationFile.Tasks?.Count == 0)
                throw new InvalidOperationException("Error in \"tasks\" configuration file, empty list");

            var users = await _taskrowClient.ListUsersAsync();
            
            foreach (var test in _configurationFile.Tasks)
            {
                var user1 = users.Where(a => a.MainEmail.Equals(test.User1Email, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                if (user1 == null)
                    throw new InvalidOperationException($"User e-mail=\"{test.User1Email}\" not found");
                if (user1.Inactive)
                    throw new InvalidOperationException($"User e-mail=\"{test.User1Email}\" is inactive");

                var user2 = users.Where(a => a.MainEmail.Equals(test.User2Email, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                if (user2 == null)
                    throw new InvalidOperationException($"User e-mail=\"{test.User2Email}\" not found");
                if (user2.Inactive)
                    throw new InvalidOperationException($"User e-mail=\"{test.User2Email}\" is inactive");

                if (user1.UserID == user2.UserID)
                    throw new InvalidOperationException($"Error in \"tasks\" configuration fiel, user1 and user2 are the same");

                var taskReference = new TaskReference(test.TaskUrl);

                var taskResponse = await _taskrowClient.GetTaskDetailAsync(taskReference);
                if (taskResponse == null)
                    throw new InvalidOperationException($"Task {test.TaskUrl} not found");
                var task = taskResponse.TaskData;

                var taskComment = $"Task forwarded on {DateTime.Now:yyyy-MM-dd HH:mm:ss}";
                int ownerUserID = task.Owner.UserID;
                var dueDate = (task.DueDateParsed >= DateTime.Now.Date ? task.DueDateParsed : DateTime.Now.Date);
                
                if (task.Owner.UserID == user1.UserID)
                    ownerUserID = user2.UserID;
                else if (task.Owner.UserID == user2.UserID)
                    ownerUserID = user1.UserID;
                else
                    throw new InvalidOperationException($"Task {test.TaskUrl}, has a unexpected owner");

                var request = new SaveTaskRequest(taskResponse.JobData.Client.ClientNickName, taskResponse.JobData.JobNumber, task.TaskNumber, task.TaskID)
                {
                    TaskTitle = task.TaskTitle,
                    TaskItemComment = taskComment,
                    OwnerUserID = ownerUserID,
                    RowVersion = task.RowVersion,
                    LastTaskItemID = task.NewTaskItems.Last().TaskItemID,
                    DueDate = dueDate.ToString("yyyy-MM-dd"),
                    EffortEstimation = task.EffortEstimation
                };

                var response = await _taskrowClient.SaveTaskAsync(request);

                if (!response.Success)
                    throw new InvalidOperationException($"Error saving task: {response.Message}");
            }

            Assert.True(true);
        }
    }
}