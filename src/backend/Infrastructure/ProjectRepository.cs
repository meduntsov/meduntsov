using Application;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ProjectRepository(AppDbContext dbContext) : IProjectRepository
{
    public async Task<Project?> GetProjectAsync(Guid projectId, CancellationToken cancellationToken = default)
        => await dbContext.Projects
            .Include(x => x.WbsNodes)
            .Include(x => x.Buildings)
            .Include(x => x.Milestones)
            .FirstOrDefaultAsync(x => x.Id == projectId, cancellationToken);

    public async Task<IReadOnlyCollection<Project>> GetHistoricalProjectsAsync(Guid exceptProjectId, CancellationToken cancellationToken = default)
        => await dbContext.Projects
            .Include(x => x.WbsNodes)
            .Include(x => x.Buildings)
            .Where(x => x.Id != exceptProjectId)
            .Take(20)
            .ToArrayAsync(cancellationToken);

    public async Task<IReadOnlyCollection<Asset>> GetAssetsAsync(Guid projectId, CancellationToken cancellationToken = default)
        => await dbContext.Assets.Where(x => x.ProjectId == projectId).ToArrayAsync(cancellationToken);

    public async Task<IReadOnlyCollection<DocumentChunk>> SearchChunksAsync(float[] embedding, int limit, CancellationToken cancellationToken = default)
    {
        var chunks = await dbContext.DocumentChunks.Take(limit).ToArrayAsync(cancellationToken);
        return chunks.Select(x => new DocumentChunk
        {
            Id = x.Id,
            ProjectId = x.ProjectId,
            SourceDocument = x.SourceDocument,
            Text = x.Text,
            Embedding = x.Embedding.ToArray()
        }).ToArray();
    }

    public async Task UpsertProjectAsync(Project project, CancellationToken cancellationToken = default)
    {
        var existing = await dbContext.Projects.FirstOrDefaultAsync(x => x.Code == project.Code, cancellationToken);
        if (existing is null)
        {
            await dbContext.Projects.AddAsync(project, cancellationToken);
            return;
        }

        existing.Name = project.Name;
        existing.BudgetCapex = project.BudgetCapex;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => dbContext.SaveChangesAsync(cancellationToken);
}
