namespace Domain;

public enum LifecycleStage { LC0, LC1, LC2, LC3, LC4, LC5, LC6, LC7 }
public enum MilestoneStatus { Planned, InProgress, Completed, Delayed }
public enum AssetSystemType { Structural, MEP, Envelope, ExternalWorks }

public class Project
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public LifecycleStage Stage { get; set; }
    public decimal BudgetCapex { get; set; }
    public ICollection<WbsNode> WbsNodes { get; set; } = new List<WbsNode>();
    public ICollection<Building> Buildings { get; set; } = new List<Building>();
    public ICollection<Milestone> Milestones { get; set; } = new List<Milestone>();
}

public class WbsNode
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ProjectId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public Guid? ParentId { get; set; }
    public decimal Budget { get; set; }
    public decimal Actual { get; set; }
}

public class Building
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ProjectId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public ICollection<Floor> Floors { get; set; } = new List<Floor>();
}

public class Floor
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid BuildingId { get; set; }
    public string Code { get; set; } = string.Empty;
    public ICollection<Lot> Lots { get; set; } = new List<Lot>();
}

public class Lot
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid FloorId { get; set; }
    public string Code { get; set; } = string.Empty;
}

public class Asset
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ProjectId { get; set; }
    public Guid WbsNodeId { get; set; }
    public Guid LotId { get; set; }
    public AssetSystemType SystemType { get; set; }
    public decimal FinancialWeight { get; set; }
    public decimal TechnicalWeight { get; set; }
    public decimal FinancialProgress { get; set; }
    public decimal TechnicalProgress { get; set; }
}

public class Milestone
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ProjectId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly PlannedDate { get; set; }
    public DateOnly? ActualDate { get; set; }
    public MilestoneStatus Status { get; set; }
}

public class DocumentChunk
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ProjectId { get; set; }
    public string SourceDocument { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public float[] Embedding { get; set; } = Array.Empty<float>();
}

public class HistoricalFeature
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ProjectId { get; set; }
    public string FeatureType { get; set; } = string.Empty;
    public float[] Embedding { get; set; } = Array.Empty<float>();
    public decimal Value { get; set; }
}

public class IngestionBatch
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string SourceKey { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public bool IsApplied { get; set; }
}
