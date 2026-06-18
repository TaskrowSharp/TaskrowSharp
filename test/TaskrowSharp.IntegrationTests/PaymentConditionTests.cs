using Microsoft.VisualBasic;
using System.Threading.Tasks;
using TaskrowSharp.IntegrationTests.TestModels;
using TaskrowSharp.Models.FinanceiroModels;
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
    
    [Fact]
    public async Task PaymentConditionInsertGetAndUpdate_Success()
    {
        var name = $"Teste {DateAndTime.Now:yyyy-MM-dd hh:mm:ss}";

        var paymentCondition = new PaymentCondition()
        {
            PaymentConditionID = 0,
            Name = name,
            Inactive = false,
            ManualEntry = false,
            Installments = [
                new PaymentConditionInstallment() {
                    Percentage = 50,
                    Days = 30
                },
                new PaymentConditionInstallment() {
                    Percentage = 50,
                    Days = 60
                }
            ]
        };

        var retInsert = await _taskrowClient.PaymentConditionInsertAsync(paymentCondition);
        paymentCondition.PaymentConditionID = retInsert.PaymentConditionID;
        Assert.NotNull(retInsert);
        Assert.NotEqual(0, paymentCondition.PaymentConditionID);
        Assert.Equal(paymentCondition.Name, retInsert.Name);
        Assert.Equal(paymentCondition.Inactive, retInsert.Inactive);

        var retGet = await _taskrowClient.PaymentConditionGetAsync(paymentCondition.PaymentConditionID);
        Assert.NotNull(retGet);
        Assert.Equal(paymentCondition.Name, retGet.Name);
        Assert.Equal(paymentCondition.Inactive, retGet.Inactive);
        Assert.Equal(paymentCondition.Installments.Count, retGet.Installments.Count);

        paymentCondition.Inactive = false;
        var retUpdate = await _taskrowClient.PaymentConditionUpdateAsync(paymentCondition);
        Assert.NotNull(retUpdate);
        Assert.Equal(paymentCondition.Inactive, retInsert.Inactive);
    }
}
