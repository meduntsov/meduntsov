using Domain;

namespace Application;

public record IngestionProjectDto(string SourceKey, string Code, string Name, decimal BudgetCapex, IReadOnlyCollection<string> WbsCodes);

public class IngestionService(IProjectRepository repository)
{
    public async Task<Guid> UpsertAsync(IngestionProjectDto dto, CancellationToken cancellationToken = default)
    {
        var project = new Project
        {
            Code = dto.Code,
            Name = dto.Name,
            BudgetCapex = dto.BudgetCapex,
            Stage = LifecycleStage.LC2,
            WbsNodes = dto.WbsCodes.Select(code => new WbsNode { Code = code, Name = code }).ToArray()
        };

        await repository.UpsertProjectAsync(project, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return project.Id;
    }
}
