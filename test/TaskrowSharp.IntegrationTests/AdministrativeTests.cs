using System.Threading.Tasks;
using TaskrowSharp.IntegrationTests.TestModels;
using Xunit;

namespace TaskrowSharp.IntegrationTests;

public class AdministrativeTests : BaseTest
{
    private readonly TaskrowClient _taskrowClient;
    private readonly ConfigurationFile _configurationFile;

    public AdministrativeTests()
    {
        _taskrowClient = GetTaskrowClient();
        _configurationFile = GetConfigurationFile();
    }

    [Fact]
    public async Task AdministrativeJobSubTypesListAsync_Success()
    {
        var response = await _taskrowClient.AdministrativeJobSubTypesListAsync();

        Assert.NotNull(response);
        Assert.NotEmpty(response.JobSubTypeList);
    }
}
