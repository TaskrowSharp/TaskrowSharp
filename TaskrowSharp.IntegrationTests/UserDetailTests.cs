using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TaskrowSharp.IntegrationTests
{
    [TestFixture]
    public class UserDetailTests
    {
        TaskrowClient taskrowClient;

        [OneTimeSetUp]
        public void Setup()
        {
            taskrowClient = UtilsTest.GetTaskrowClient();
        }

        [Test]
        public void UserDetail_Get_FirstActive()
        {
            var listUsers = taskrowClient.ListUsers();
            var userTest = listUsers.Where(a => a.Active).First();

            var userDetail = taskrowClient.GetUser(userTest.UserID);
            Assert.IsTrue(userDetail != null);
            Assert.IsTrue(string.Equals(userTest.UserID, userDetail.UserID));
            Assert.IsTrue(string.Equals(userTest.MainEmail, userDetail.MainEmail));
        }

        [Test]
        public void UserDetail_Get_FirstInactive()
        {
            var listUsers = taskrowClient.ListUsers();
            var userTest = listUsers.Where(a => !a.Active).FirstOrDefault();
            if (userTest == null)
                throw new System.InvalidOperationException(string.Format("0 users inactive in {0}", taskrowClient.ServiceUrl.ToString()));

            var userDetail = taskrowClient.GetUser(userTest.UserID);
            Assert.IsTrue(userDetail != null);
            Assert.IsTrue(string.Equals(userTest.UserID, userDetail.UserID));
            Assert.IsTrue(string.Equals(userTest.MainEmail, userDetail.MainEmail));
        }
    }
}
