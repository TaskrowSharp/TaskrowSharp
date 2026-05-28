using System.Threading.Tasks;
using TaskrowSharp.IntegrationTests.TestModels;
using Xunit;

namespace TaskrowSharp.IntegrationTests;

public class PaymentConditionTests : BaseTest
{
    private readonly TaskrowClient _taskrowClient;
    private readonly ConfigurationFile _configurationFile;

    public PaymentConditionTests()
    {
        _taskrowClient = GetTaskrowClient();
        _configurationFile = GetConfigurationFile();
    }
    
    /*
    [Fact]
    public async Task PaymentConditionGetAsync_Success()
    {
        var paymentCondition = await _taskrowClient.PaymentConditionGetAsync(client.ClientNickName, 1);
        
        Assert.NotNull(job);
        Assert.Equal(jobNumber, job.JobNumber);
    }
    */
}
