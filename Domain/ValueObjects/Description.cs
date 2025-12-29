using Domain.Common;
using Domain.Exceptions;

namespace Domain.ValueObjects;

public class Description: ValueObject
{
    public const int MaxLength = 200;
    
    public string? Text { get; }
    
    public Description(string? description)
    {
        if (description?.Length > MaxLength)
            throw new RuleViolationDomainException($"The provided description must be smaller or equal to {MaxLength}.");
        
        Text = description;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Text ?? string.Empty;
    }
}