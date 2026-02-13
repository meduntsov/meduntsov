using Api;
using Application;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/projects")]
public class ProjectsController(
    IProjectRepository repository,
    ReadinessEngine readinessEngine,
    SimilarityService similarityService,
    RiskService riskService,
    RagService ragService,
    IngestionService ingestionService) : ControllerBase
{
    [HttpPost("ingest")]
    public async Task<ActionResult<Guid>> Ingest([FromBody] IngestRequest request, CancellationToken cancellationToken)
    {
        var id = await ingestionService.UpsertAsync(new IngestionProjectDto(
            request.SourceKey,
            request.Code,
            request.Name,
            request.BudgetCapex,
            request.WbsCodes), cancellationToken);

        return Ok(id);
    }

    [HttpGet("{projectId:guid}/dashboard")]
    public async Task<ActionResult<object>> Dashboard(Guid projectId, CancellationToken cancellationToken)
    {
        var project = await repository.GetProjectAsync(projectId, cancellationToken);
        if (project is null) return NotFound();

        var assets = await repository.GetAssetsAsync(projectId, cancellationToken);
        var readiness = readinessEngine.Calculate(project, assets);
        var risk = riskService.Predict(project, readiness);
        var similar = await similarityService.FindAsync(project, cancellationToken);

        return Ok(new
        {
            project.Id,
            project.Code,
            readiness,
            risk,
            similar
        });
    }

    [HttpPost("{projectId:guid}/ask-ai")]
    public async Task<ActionResult<object>> AskAi(Guid projectId, [FromBody] AskAiRequest request, CancellationToken cancellationToken)
    {
        var answer = await ragService.AskAsync(request.Question, cancellationToken);
        return Ok(answer);
    }
}
