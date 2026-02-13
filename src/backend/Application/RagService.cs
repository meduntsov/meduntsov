namespace Application;

public class RagService(IProjectRepository repository, IEmbeddingService embeddingService)
{
    public async Task<RAGAnswer> AskAsync(string question, CancellationToken cancellationToken = default)
    {
        var embedding = await embeddingService.BuildEmbeddingAsync(question, cancellationToken);
        var chunks = await repository.SearchChunksAsync(embedding, 5, cancellationToken);

        var citations = chunks.Select(chunk => new RagCitation(
            chunk.ProjectId.ToString(),
            chunk.SourceDocument,
            chunk.Text[..Math.Min(140, chunk.Text.Length)],
            0.8)).ToArray();

        var summary = $"Found {chunks.Count} relevant fragments for question: {question}";
        return new RAGAnswer(summary, citations);
    }
}
