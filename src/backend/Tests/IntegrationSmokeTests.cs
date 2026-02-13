using FluentAssertions;
using Testcontainers.PostgreSql;

namespace Tests;

public class IntegrationSmokeTests : IAsyncLifetime
{
    private readonly PostgreSqlContainer _container = new PostgreSqlBuilder()
        .WithImage("pgvector/pgvector:pg16")
        .WithDatabase("erp")
        .WithUsername("erp")
        .WithPassword("erp")
        .Build();

    [Fact]
    public void Placeholder_ShouldPass()
    {
        _container.GetConnectionString().Should().NotBeNullOrWhiteSpace();
    }

    public Task InitializeAsync() => Task.CompletedTask;
    public Task DisposeAsync() => Task.CompletedTask;
}
