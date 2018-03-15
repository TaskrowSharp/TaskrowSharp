using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp.Tests
{
    [TestClass]
    public class UserDetailTests
    {
        [TestMethod]
        public void UserDetail_Get_FirstActive()
        {
            var taskrowClient = UtilsTest.GetTaskrowClient();
            var listUsers = taskrowClient.ListUsers();
            var userTest = listUsers.Where(a => a.Active).First();

            var userDetail = taskrowClient.GetUserDetail(userTest.UserID);
            Assert.IsTrue(userDetail != null);
            Assert.IsTrue(string.Equals(userTest.UserID, userDetail.UserID));
            Assert.IsTrue(string.Equals(userTest.MainEmail, userDetail.MainEmail));
        }

        [TestMethod]
        public void UserDetail_Get_FirstInactive()
        {
            var taskrowClient = UtilsTest.GetTaskrowClient();
            var listUsers = taskrowClient.ListUsers();
            var userTest = listUsers.Where(a => !a.Active).First();

            var userDetail = taskrowClient.GetUserDetail(userTest.UserID);
            Assert.IsTrue(userDetail != null);
            Assert.IsTrue(string.Equals(userTest.UserID, userDetail.UserID));
            Assert.IsTrue(string.Equals(userTest.MainEmail, userDetail.MainEmail));
        }
    }
}
