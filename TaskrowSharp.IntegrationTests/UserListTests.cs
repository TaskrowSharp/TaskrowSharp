using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TaskrowSharp.IntegrationTests
{
    [TestFixture]
    public class UserListTests
    {
        TaskrowClient taskrowClient;

        [OneTimeSetUp]
        public void Setup()
        {
            taskrowClient = UtilsTest.GetTaskrowClient();
        }

        [Test]
        public void UserList_OK()
        {
            var listUsers = taskrowClient.ListUsers();
            Assert.IsTrue(listUsers.Count > 0);
        }
    }
}
