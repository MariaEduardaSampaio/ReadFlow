using Domain.Common;
using Domain.Exceptions;

namespace Domain.ValueObjects;

public class Synopsis: ValueObject
{
    public const int MaxLength = 1000;
    
    public string? Text { get; }
    
    public Synopsis(string? text)
    {
        if (text?.Length > MaxLength)
            throw new RuleViolationDomainException($"The provided synopsis must be smaller or equal to {MaxLength}.");
        
        Text = text;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Text ?? string.Empty;
    }
}