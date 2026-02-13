using Domain;
using Microsoft.EntityFrameworkCore;
using Pgvector;

namespace Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<WbsNode> WbsNodes => Set<WbsNode>();
    public DbSet<Building> Buildings => Set<Building>();
    public DbSet<Floor> Floors => Set<Floor>();
    public DbSet<Lot> Lots => Set<Lot>();
    public DbSet<Asset> Assets => Set<Asset>();
    public DbSet<Milestone> Milestones => Set<Milestone>();
    public DbSet<DocumentChunkEntity> DocumentChunks => Set<DocumentChunkEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("vector");
        modelBuilder.Entity<DocumentChunkEntity>().Property(x => x.Embedding).HasColumnType("vector(8)");
        base.OnModelCreating(modelBuilder);
    }
}

public class DocumentChunkEntity
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string SourceDocument { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public Vector Embedding { get; set; } = new(new float[8]);
}
