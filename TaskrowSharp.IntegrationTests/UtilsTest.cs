using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskrowSharp.IntegrationTests
{
    internal static class UtilsTest
    {
        public static ConfigurationFile GetConfigurationFile()
        {
            string path = Utils.IOHelper.GetFullPathFromRelative(@"config\main.json", true, true);

            if (!System.IO.File.Exists(path))
                throw new System.InvalidOperationException(string.Format("File not found: {0}", path));

            string json;
            using (var reader = new System.IO.StreamReader(path))
            {
                json = reader.ReadToEnd();
            }

            var configurationFile = Utils.JsonHelper.Deserialize<ConfigurationFile>(json);

            return configurationFile;
        }

        public static TaskrowClient GetTaskrowClient()
        {
            var config = GetConfigurationFile();

            var taskrowClient = new TaskrowClient();
            taskrowClient.Connect(new Uri(config.ServiceUrl), new AccessKeyCredential(config.AccessKey));

            return taskrowClient;
        }
    }
}
