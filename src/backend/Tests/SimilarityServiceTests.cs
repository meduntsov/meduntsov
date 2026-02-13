using Application;
using Domain;
using FluentAssertions;

namespace Tests;

public class SimilarityServiceTests
{
    [Fact]
    public async Task Find_ShouldReturnOrderedSimilarity()
    {
        var current = new Project { Id = Guid.NewGuid(), Code = "CUR", BudgetCapex = 100, WbsNodes = [new WbsNode()], Buildings = [new Building()] };
        var similar = new Project { Id = Guid.NewGuid(), Code = "SIM", BudgetCapex = 102, WbsNodes = [new WbsNode()], Buildings = [new Building()] };
        var far = new Project { Id = Guid.NewGuid(), Code = "FAR", BudgetCapex = 10, WbsNodes = [], Buildings = [] };
        var repo = new FakeRepo(current, similar, far);

        var result = await new SimilarityService(repo).FindAsync(current);

        result.First().ProjectCode.Should().Be("SIM");
    }

    private class FakeRepo(params Project[] projects) : IProjectRepository
    {
        public Task<Project?> GetProjectAsync(Guid projectId, CancellationToken cancellationToken = default) => Task.FromResult(projects.FirstOrDefault(x => x.Id == projectId));
        public Task<IReadOnlyCollection<Project>> GetHistoricalProjectsAsync(Guid exceptProjectId, CancellationToken cancellationToken = default) => Task.FromResult<IReadOnlyCollection<Project>>(projects.Where(x => x.Id != exceptProjectId).ToArray());
        public Task<IReadOnlyCollection<Asset>> GetAssetsAsync(Guid projectId, CancellationToken cancellationToken = default) => Task.FromResult<IReadOnlyCollection<Asset>>(Array.Empty<Asset>());
        public Task<IReadOnlyCollection<DocumentChunk>> SearchChunksAsync(float[] embedding, int limit, CancellationToken cancellationToken = default) => Task.FromResult<IReadOnlyCollection<DocumentChunk>>(Array.Empty<DocumentChunk>());
        public Task UpsertProjectAsync(Project project, CancellationToken cancellationToken = default) => Task.CompletedTask;
        public Task SaveChangesAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
    }
}
