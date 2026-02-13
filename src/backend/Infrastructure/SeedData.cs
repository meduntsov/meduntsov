using Domain;

namespace Infrastructure;

public static class SeedData
{
    public static async Task EnsureSeededAsync(AppDbContext dbContext)
    {
        if (dbContext.Projects.Any()) return;

        var active = new Project { Code = "PRJ-ACTIVE", Name = "Active Residential", Stage = LifecycleStage.LC4, BudgetCapex = 120_000_000m };
        var p1 = new Project { Code = "PRJ-HIS-1", Name = "Historic One", Stage = LifecycleStage.LC7, BudgetCapex = 90_000_000m };
        var p2 = new Project { Code = "PRJ-HIS-2", Name = "Historic Two", Stage = LifecycleStage.LC7, BudgetCapex = 130_000_000m };
        var p3 = new Project { Code = "PRJ-HIS-3", Name = "Historic Three", Stage = LifecycleStage.LC6, BudgetCapex = 70_000_000m };

        active.WbsNodes.Add(new WbsNode { Code = "1.1", Name = "Design", Budget = 5_000_000m, Actual = 3_000_000m });
        active.WbsNodes.Add(new WbsNode { Code = "2.1", Name = "Structure", Budget = 40_000_000m, Actual = 12_000_000m });

        active.Buildings.Add(new Building { Code = "B1", Name = "Building 1" });
        active.Milestones.Add(new Milestone { Name = "Permit", PlannedDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(20)), Status = MilestoneStatus.InProgress });

        dbContext.Projects.AddRange(active, p1, p2, p3);

        dbContext.Assets.Add(new Asset
        {
            ProjectId = active.Id,
            WbsNodeId = active.WbsNodes.First().Id,
            LotId = Guid.NewGuid(),
            SystemType = AssetSystemType.Structural,
            FinancialWeight = 0.6m,
            TechnicalWeight = 0.4m,
            FinancialProgress = 0.5m,
            TechnicalProgress = 0.45m
        });

        dbContext.DocumentChunks.Add(new DocumentChunkEntity
        {
            ProjectId = p1.Id,
            SourceDocument = "postmortem-h1.md",
            Text = "Concrete supplier delay increased schedule by 18 days.",
            Embedding = new Pgvector.Vector(new float[] {0.1f, 0.3f, 0.2f, 0.2f, 0.4f, 0.1f, 0.2f, 0.3f})
        });

        await dbContext.SaveChangesAsync();
    }
}
