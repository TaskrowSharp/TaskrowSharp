﻿using System.Threading.Tasks;
using Xunit;

namespace TaskrowSharp.IntegrationTests
{
    public class IndexDataTests : BaseTest
    {
        private readonly TaskrowClient _taskrowClient;

        public IndexDataTests()
        {
            _taskrowClient = GetTaskrowClient();
        }

        [Fact]
        public async Task GetIndexDataAsync_Success()
        {
            var indexData = await _taskrowClient.GetIndexDataAsync();
            
            Assert.NotNull(indexData);
            Assert.NotEqual(0, indexData.InternalClient.AppMainCompanyID);
        }
    }
}