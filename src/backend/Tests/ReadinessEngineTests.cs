using Application;
using Domain;
using FluentAssertions;

namespace Tests;

public class ReadinessEngineTests
{
    [Fact]
    public void Calculate_ShouldComputeWeightedReadiness()
    {
        var project = new Project { Name = "Test" };
        var assets = new[]
        {
            new Asset { FinancialWeight = 0.7m, FinancialProgress = 0.5m, TechnicalWeight = 0.5m, TechnicalProgress = 0.4m, SystemType = AssetSystemType.Structural },
            new Asset { FinancialWeight = 0.3m, FinancialProgress = 0.9m, TechnicalWeight = 0.5m, TechnicalProgress = 0.6m, SystemType = AssetSystemType.MEP }
        };

        var result = new ReadinessEngine().Calculate(project, assets);

        result.FinancialReadiness.Should().BeApproximately(0.62m, 0.001m);
        result.TechnicalReadiness.Should().BeApproximately(0.5m, 0.001m);
        result.DrillDown.Should().HaveCount(2);
    }
}
