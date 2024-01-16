namespace FunctionsDemo.Abstractions;

public record EmailMessages
{
    public required EmailMessage[] Messages { get; set; } = new EmailMessage[0];
}
