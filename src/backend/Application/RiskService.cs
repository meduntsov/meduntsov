using Domain;

namespace Application;

public class RiskService
{
    public IReadOnlyCollection<RiskPrediction> Predict(Project project, ReadinessResult readiness)
    {
        var delayed = project.Milestones.Count(x => x.Status == MilestoneStatus.Delayed);
        var baselineRisk = 0.15m + delayed * 0.1m;
        var readinessPenalty = Math.Max(0m, (0.6m - readiness.FinancialReadiness) * 0.5m)
                             + Math.Max(0m, (0.6m - readiness.TechnicalReadiness) * 0.5m);

        var score = Math.Min(1m, baselineRisk + readinessPenalty);

        return project.Milestones.Select(m => new RiskPrediction(
            m.Name,
            score,
            new[]
            {
                $"Delayed milestones: {delayed}",
                $"Financial readiness: {readiness.FinancialReadiness:P1}",
                $"Technical readiness: {readiness.TechnicalReadiness:P1}"
            })).ToArray();
    }
}
