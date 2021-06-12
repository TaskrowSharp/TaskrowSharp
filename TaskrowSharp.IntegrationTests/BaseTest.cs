using System;

namespace TaskrowSharp.IntegrationTests
{
    public abstract class BaseTest
    {
        protected static ConfigurationFile GetConfigurationFile()
        {
            string path = Utils.IOHelper.GetFullPathFromRelative(@"config\main.json");

            if (!System.IO.File.Exists(path))
                throw new System.InvalidOperationException($"File not found: {path}");

            string json;
            using (var reader = new System.IO.StreamReader(path))
            {
                json = reader.ReadToEnd();
            }

            var configurationFile = Utils.JsonHelper.Deserialize<ConfigurationFile>(json);

            return configurationFile;
        }

        protected static TaskrowClient GetTaskrowClient()
        {
            var config = GetConfigurationFile();
            var taskrowClient = new TaskrowClient(new Uri(config.ServiceUrl), config.AccessKey);
            return taskrowClient;
        }
    }
}
