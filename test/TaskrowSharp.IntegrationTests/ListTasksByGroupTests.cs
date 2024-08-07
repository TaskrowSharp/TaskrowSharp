﻿using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TaskrowSharp.IntegrationTests
{
    public class ListTasksByGroupTests : BaseTest
    {
        private readonly TaskrowClient _taskrowClient;

        public ListTasksByGroupTests()
        {
            _taskrowClient = GetTaskrowClient();
        }

        [Fact]
        public async Task TaskListByGroupAsync_Success()
        {
            var groups = await _taskrowClient.UserGroupListAsync();
            var group = groups.First();

            var entity = await _taskrowClient.TaskListByGroupAsync(group.GroupID);
            Assert.NotNull(entity);
        }
    }
}