﻿using System;
using System.Linq;
using System.Threading.Tasks;
using TaskrowSharp.IntegrationTests.TestModels;
using TaskrowSharp.Models.TaskModels;
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
                throw new InvalidOperationException("Error in configuration file, \"tasks\" list is empty");

            if (_configurationFile.UserIDs?.Count < 2)
                throw new InvalidOperationException("Error in configuration file, \"users\" list should have 2 or more User IDs");

            var users = await _taskrowClient.UserListAsync();
            
            foreach (var taskItem in _configurationFile.Tasks)
            {
                var user1 = users.Where(a => a.UserID == _configurationFile.UserIDs.First()).FirstOrDefault();
                if (user1 == null)
                    throw new InvalidOperationException($"User userID={_configurationFile.UserIDs.First()} not found");
                if (user1.Inactive)
                    throw new InvalidOperationException($"User userID={_configurationFile.UserIDs.First()} is inactive");

                var user2 = users.Where(a => a.UserID == _configurationFile.UserIDs.Skip(1).First()).FirstOrDefault();
                if (user2 == null)
                    throw new InvalidOperationException($"User userID={_configurationFile.UserIDs.Skip(1).First()} not found");
                if (user2.Inactive)
                    throw new InvalidOperationException($"User userID={_configurationFile.UserIDs.Skip(1).First()} is inactive");

                if (user1.UserID == user2.UserID)
                    throw new InvalidOperationException($"Error in configuration file, \"tasks\" is invalid, user1 and user2 are the same");

                var taskReference = new TaskReference(taskItem.ClientNickName, taskItem.JobNumber, taskItem.TaskNumber);

                var taskResponse = await _taskrowClient.TaskDetailGetAsync(taskReference);
                if (taskResponse == null)
                    throw new InvalidOperationException($"Task {taskReference} not found");
                var task = taskResponse.TaskData;

                var taskComment = $"Task forwarded on {DateTime.Now:yyyy-MM-dd HH:mm:ss}";
                int ownerUserID = task.Owner.UserID;
                var dueDate = (task.DueDate >= DateTime.Now.Date ? task.DueDate : DateTime.Now.Date);

                if (task.Owner.UserID == user1.UserID)
                    ownerUserID = user2.UserID;
                else if (task.Owner.UserID == user2.UserID)
                    ownerUserID = user1.UserID;
                else
                    throw new InvalidOperationException($"Task {taskReference}, has an unexpected owner");

                var request = new TaskSaveRequest(taskResponse.JobData.Client.ClientNickName, taskResponse.JobData.JobNumber, task.TaskNumber, task.TaskID)
                {
                    TaskTitle = task.TaskTitle,
                    TaskItemComment = taskComment,
                    OwnerUserID = ownerUserID,
                    RowVersion = task.RowVersion,
                    LastTaskItemID = task.NewTaskItems.Last().TaskItemID,
                    DueDate = dueDate,
                    EffortEstimation = task.EffortEstimation
                };

                var taskEntity = await _taskrowClient.TaskSaveAsync(request);

                Assert.NotNull(taskEntity);
                Assert.Equal(request.TaskTitle, taskEntity.TaskTitle);
            }
        }
    }
}