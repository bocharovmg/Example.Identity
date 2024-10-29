namespace Infrastructure.Processors.Models;

internal class EmailMessageModel
{
    public string? From { get; init; }

    public IEnumerable<string> To { get; init; } = Enumerable.Empty<string>();

    public string Subject { get; init; } = string.Empty;

    public string Message { get; init; } = string.Empty;
}
