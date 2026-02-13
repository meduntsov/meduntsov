namespace Api;

public record IngestRequest(string SourceKey, string Code, string Name, decimal BudgetCapex, IReadOnlyCollection<string> WbsCodes);
public record AskAiRequest(string Question);
