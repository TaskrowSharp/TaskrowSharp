using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TaskrowSharp.IntegrationTests
{
    [TestFixture]
    public class TaskTests
    {
        TaskrowClient taskrowClient;
        
        [OneTimeSetUp]
        public void Setup()
        {
            taskrowClient = UtilsTest.GetTaskrowClient();
        }

        [Test]
        public void Task_ListTasksByGroup_OK()
        {
            var groupTest = taskrowClient.ListGroups().First();

            var tasks = taskrowClient.ListTasksByGroup(groupTest.GroupID);
            Assert.IsTrue(tasks.Count > 0);
        }

        [Test]
        public void Task_Forward_OK()
        {
            var configuration = UtilsTest.GetConfigurationFile();

            if (configuration.ForwardTaskTests == null || configuration.ForwardTaskTests.Count == 0)
                throw new System.InvalidOperationException("No \"forwardTaskTests\" items configured in configuration file");

            var users = taskrowClient.ListUsers();
            
            foreach (var test in configuration.ForwardTaskTests)
            {
                var user1 = users.Where(a => a.MainEmail.Equals(test.User1Email, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                if (user1 == null)
                    throw new System.InvalidOperationException(string.Format("User e-mail=\"{0}\" not found", test.User1Email));
                if (!user1.Active)
                    throw new System.InvalidOperationException(string.Format("User e-mail=\"{0}\" is inactive", test.User1Email));

                var user2 = users.Where(a => a.MainEmail.Equals(test.User2Email, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                if (user2 == null)
                    throw new System.InvalidOperationException(string.Format("User e-mail=\"{0}\" not found", test.User2Email));
                if (!user2.Active)
                    throw new System.InvalidOperationException(string.Format("User e-mail=\"{0}\" is inactive", test.User2Email));

                if (user1.UserID == user2.UserID)
                    throw new System.InvalidOperationException(string.Format("Error in \"forwardTaskTests\" configuration, user1 and user2 are the same"));

                var taskReference = new TaskReference(test.TaskUrl);

                var task = taskrowClient.GetTaskDetail(taskReference);
                if (task == null)
                    throw new System.InvalidOperationException(string.Format("Task {0} not found", test.TaskUrl));

                var taskComment = string.Format("Task forwarded on {0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                int ownerUserID = task.Owner.UserID;
                var dueDate = (task.DueDate >= DateTime.Now.Date ? task.DueDate : DateTime.Now.Date);
                
                if (task.Owner.UserID == user1.UserID)
                    ownerUserID = user2.UserID;
                else if (task.Owner.UserID == user2.UserID)
                    ownerUserID = user1.UserID;
                else
                    throw new System.InvalidOperationException(string.Format("Task {0}, has a unexpected owner", test.TaskUrl));

                var request = new SaveTaskRequest(task.TaskID, task.ClientNickname, task.JobNumber, task.TaskNumber, task.TaskTitle, taskComment, ownerUserID,
                    task.RowVersion, task.TaskItems.Last().TaskItemID, dueDate, 0, task.EffortEstimationMinutes);

                var response = taskrowClient.SaveTask(request);

                if (!response.Success)
                    throw new System.InvalidOperationException(string.Format("Error saving task: {0}", response.Message));
            }

            Assert.IsTrue(true);
        }
    }
}
