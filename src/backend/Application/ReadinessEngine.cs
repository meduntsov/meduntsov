using Domain;

namespace Application;

public class ReadinessEngine
{
    public ReadinessResult Calculate(Project project, IReadOnlyCollection<Asset> assets)
    {
        if (assets.Count == 0)
        {
            return new ReadinessResult(0, 0, Array.Empty<ReadinessSlice>());
        }

        var financialWeight = assets.Sum(x => x.FinancialWeight);
        var technicalWeight = assets.Sum(x => x.TechnicalWeight);

        var financial = financialWeight == 0 ? 0 : assets.Sum(x => x.FinancialProgress * x.FinancialWeight) / financialWeight;
        var technical = technicalWeight == 0 ? 0 : assets.Sum(x => x.TechnicalProgress * x.TechnicalWeight) / technicalWeight;

        var bySystem = assets
            .GroupBy(x => x.SystemType)
            .Select(group => new ReadinessSlice(
                "system",
                group.Key.ToString(),
                group.Average(x => x.FinancialProgress),
                group.Average(x => x.TechnicalProgress)))
            .ToArray();

        return new ReadinessResult(financial, technical, bySystem);
    }
}
