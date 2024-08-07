﻿using System.Threading.Tasks;
using Xunit;

namespace TaskrowSharp.IntegrationTests
{
    public class GroupListTests : BaseTest
    {
        private readonly TaskrowClient _taskrowClient;

        public GroupListTests()
        {
            _taskrowClient = GetTaskrowClient();
        }

        [Fact]
        public async Task UserGroupListAsync_Success()
        {
            var listGroups = await _taskrowClient.UserGroupListAsync();
            Assert.NotEmpty(listGroups);
        }
    }
}