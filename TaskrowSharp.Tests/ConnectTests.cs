using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TaskrowSharp.Tests
{
    [TestClass]
    public class ConnectTests
    {
        [TestMethod]
        public void Connect_EmailAndPassword_OK()
        {
            var configurationFile = UtilsTest.GetConfigurationFile();

            var client = new TaskrowSharp.TaskrowClient();
            client.Connect(new Uri(configurationFile.ServiceUrl), new EmailAndPasswordCredential(configurationFile.Email, configurationFile.Password));

            client.Disconnect();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidServiceUrlException))]
        public void Connect_EmailAndPassword_ServiceUrl_HttpsRequired()
        {
            var client = new TaskrowSharp.TaskrowClient();
            client.Connect(new Uri("http://yourdomain.taskrow.com"), new EmailAndPasswordCredential("test@test.com", "123456789"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidServiceUrlException))]
        public void Connect_EmailAndPassword_ServiceUrl_Invalid()
        {
            var client = new TaskrowSharp.TaskrowClient();
            client.Connect(new Uri("http://test.com"), new EmailAndPasswordCredential("test@test.com", "123456789"));
        }

        [TestMethod]
        [ExpectedException(typeof(AuthenticationException))]
        public void Connect_EmailAndPassword_WrongPassword()
        {
            var configurationFile = UtilsTest.GetConfigurationFile();

            var client = new TaskrowSharp.TaskrowClient();
            client.Connect(new Uri(configurationFile.ServiceUrl), new EmailAndPasswordCredential(configurationFile.Email, "123456789"));
        }

        [TestMethod]
        public void Connect_AccessKey_OK()
        {
            var configurationFile = UtilsTest.GetConfigurationFile();

            var client = new TaskrowSharp.TaskrowClient();
            client.Connect(new Uri(configurationFile.ServiceUrl), configurationFile.AccessKey);

            client.Disconnect();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidServiceUrlException))]
        public void Connect_AccessKey_ServiceUrl_HttpsRequired()
        {
            var configurationFile = UtilsTest.GetConfigurationFile();

            var client = new TaskrowSharp.TaskrowClient();
            client.Connect(new Uri("http://yourdomain.taskrow.com"), configurationFile.AccessKey);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidServiceUrlException))]
        public void Connect_AccessKey_ServiceUrl_Invalid()
        {
            var configurationFile = UtilsTest.GetConfigurationFile();

            var client = new TaskrowSharp.TaskrowClient();
            client.Connect(new Uri("http://test.com"), configurationFile.AccessKey);
        }

        [TestMethod]
        [ExpectedException(typeof(AuthenticationException))]
        public void Connect_AccessKey_WrongAccessKey()
        {
            var configurationFile = UtilsTest.GetConfigurationFile();

            var client = new TaskrowSharp.TaskrowClient();
            client.Connect(new Uri(configurationFile.ServiceUrl), "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        }
    }
}
