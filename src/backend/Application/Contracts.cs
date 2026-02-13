using Domain;

namespace Application;

public record ReadinessResult(decimal FinancialReadiness, decimal TechnicalReadiness, IReadOnlyCollection<ReadinessSlice> DrillDown);
public record ReadinessSlice(string Dimension, string Key, decimal FinancialReadiness, decimal TechnicalReadiness);
public record SimilarProjectResult(Guid ProjectId, string ProjectCode, double SimilarityScore, string Explanation);
public record RiskPrediction(string Milestone, decimal RiskScore, IReadOnlyCollection<string> Drivers);
public record RAGAnswer(string Answer, IReadOnlyCollection<RagCitation> Citations);
public record RagCitation(string ProjectCode, string Document, string Snippet, double Similarity);

public interface IProjectRepository
{
    Task<Project?> GetProjectAsync(Guid projectId, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Project>> GetHistoricalProjectsAsync(Guid exceptProjectId, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Asset>> GetAssetsAsync(Guid projectId, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<DocumentChunk>> SearchChunksAsync(float[] embedding, int limit, CancellationToken cancellationToken = default);
    Task UpsertProjectAsync(Project project, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}

public interface IEmbeddingService
{
    Task<float[]> BuildEmbeddingAsync(string text, CancellationToken cancellationToken = default);
}
