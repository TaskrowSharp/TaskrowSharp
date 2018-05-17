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

            var groupsList = taskrowClient.ListGroups();
            var usersList = taskrowClient.ListUsers();
            
            foreach (var test in configuration.ForwardTaskTests)
            {
                var user1 = usersList.Where(a => a.MainEmail.Equals(test.User1Email, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                if (user1 == null)
                    throw new System.InvalidOperationException(string.Format("User e-mail=\"{0}\" not found", test.User1Email));
                if (!user1.Active)
                    throw new System.InvalidOperationException(string.Format("User e-mail=\"{0}\" is inactive", test.User1Email));

                var user2 = usersList.Where(a => a.MainEmail.Equals(test.User2Email, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
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

                if (task.Owner.UserID == user1.UserID)
                {
                    taskrowClient.SaveTask(new SaveTaskRequest() { OwnerUserID = user2.UserID });
                }
                else if (task.Owner.UserID == user2.UserID)
                {
                    taskrowClient.SaveTask(new SaveTaskRequest() { OwnerUserID = user1.UserID });
                }
                else
                {
                    throw new System.InvalidOperationException(string.Format("Task {0} owner not expected", test.TaskUrl));
                }
            }

            Assert.IsTrue(true);
        }
    }
}
