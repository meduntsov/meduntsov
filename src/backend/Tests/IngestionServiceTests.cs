using Application;
using Domain;
using FluentAssertions;

namespace Tests;

public class IngestionServiceTests
{
    [Fact]
    public async Task Upsert_ShouldBeIdempotentByCode()
    {
        var repo = new InMemoryProjectRepository();
        var service = new IngestionService(repo);

        await service.UpsertAsync(new IngestionProjectDto("s", "PRJ-1", "Name A", 10, new[] { "1.1" }));
        await service.UpsertAsync(new IngestionProjectDto("s", "PRJ-1", "Name B", 12, new[] { "1.2" }));

        repo.Projects.Should().HaveCount(1);
        repo.Projects.Single().Name.Should().Be("Name B");
    }

    private class InMemoryProjectRepository : IProjectRepository
    {
        public List<Project> Projects { get; } = [];
        public Task<Project?> GetProjectAsync(Guid projectId, CancellationToken cancellationToken = default) => Task.FromResult(Projects.FirstOrDefault(x => x.Id == projectId));
        public Task<IReadOnlyCollection<Project>> GetHistoricalProjectsAsync(Guid exceptProjectId, CancellationToken cancellationToken = default) => Task.FromResult<IReadOnlyCollection<Project>>(Projects.Where(x => x.Id != exceptProjectId).ToArray());
        public Task<IReadOnlyCollection<Asset>> GetAssetsAsync(Guid projectId, CancellationToken cancellationToken = default) => Task.FromResult<IReadOnlyCollection<Asset>>(Array.Empty<Asset>());
        public Task<IReadOnlyCollection<DocumentChunk>> SearchChunksAsync(float[] embedding, int limit, CancellationToken cancellationToken = default) => Task.FromResult<IReadOnlyCollection<DocumentChunk>>(Array.Empty<DocumentChunk>());
        public Task UpsertProjectAsync(Project project, CancellationToken cancellationToken = default)
        {
            var existing = Projects.FirstOrDefault(x => x.Code == project.Code);
            if (existing is null) Projects.Add(project);
            else { existing.Name = project.Name; existing.BudgetCapex = project.BudgetCapex; }
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
    }
}
