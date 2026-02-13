using Domain;

namespace Application;

public class SimilarityService(IProjectRepository repository)
{
    public async Task<IReadOnlyCollection<SimilarProjectResult>> FindAsync(Project project, CancellationToken cancellationToken = default)
    {
        var history = await repository.GetHistoricalProjectsAsync(project.Id, cancellationToken);

        var currentVector = new[] { (double)project.BudgetCapex, (double)project.WbsNodes.Count, (double)project.Buildings.Count };

        return history
            .Select(candidate =>
            {
                var vector = new[] { (double)candidate.BudgetCapex, (double)candidate.WbsNodes.Count, (double)candidate.Buildings.Count };
                var similarity = Cosine(currentVector, vector);
                return new SimilarProjectResult(candidate.Id, candidate.Code, similarity, "CAPEX/WBS/Building profile");
            })
            .OrderByDescending(x => x.SimilarityScore)
            .Take(5)
            .ToArray();
    }

    private static double Cosine(IReadOnlyList<double> a, IReadOnlyList<double> b)
    {
        var dot = 0d;
        var normA = 0d;
        var normB = 0d;
        for (var i = 0; i < a.Count; i++)
        {
            dot += a[i] * b[i];
            normA += a[i] * a[i];
            normB += b[i] * b[i];
        }

        if (normA == 0 || normB == 0) return 0;
        return dot / (Math.Sqrt(normA) * Math.Sqrt(normB));
    }
}
