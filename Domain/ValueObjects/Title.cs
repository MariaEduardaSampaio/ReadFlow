using Domain.Common;
using Domain.Exceptions;

namespace Domain.ValueObjects;

public class Title: ValueObject
{
    public const int MaxLength = 200;
    
    public string? Text { get; }
    
    public Title(string? text)
    {
        if (text?.Length > MaxLength)
            throw new RuleViolationDomainException($"The provided title must be smaller or equal to {MaxLength}.");
        
        Text = text;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Text ?? string.Empty;
    }
}