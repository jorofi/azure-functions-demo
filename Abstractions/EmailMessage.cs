namespace FunctionsDemo.Abstractions;

public record EmailMessage
{
    public required string To { get; set; } = string.Empty;

    public required string Subject { get; set; } = string.Empty;

    public required string Body { get; set; } = string.Empty;
}
