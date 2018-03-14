using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp.Tests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void User_List()
        {
            var taskrowClient = UtilsTest.GetTaskrowClient();
            var listUsers = taskrowClient.ListUsers();
            Assert.IsTrue(listUsers.Count > 0);
        }

        [TestMethod]
        public void User_GetUser()
        {
            var taskrowClient = UtilsTest.GetTaskrowClient();
            var listUsers = taskrowClient.ListUsers();
            var userTest = listUsers.Where(a => a.Active).First();
            
            var user = taskrowClient.GetUser(userTest.UserID);
            Assert.IsTrue(user != null);
            Assert.IsTrue(string.Equals(userTest.UserID, user.UserID));
            Assert.IsTrue(string.Equals(userTest.MainEmail, user.MainEmail));
        }
    }
}
