using Domain.Common;
using Domain.Exceptions;
using Domain.Extensions;

namespace Domain.ValueObjects;

public class Name: ValueObject
{
    public const int MaxLength = 100;
    
    public string? Text { get; }
    
    public Name(string name)
    {
        if (name.IsEmpty)
            throw new RuleViolationDomainException("The provided name must not be null or empty.");
        
        if (name.Length > MaxLength)
            throw new RuleViolationDomainException($"The provided name must be smaller or equal to {MaxLength}.");
        
        Text = name;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Text ?? string.Empty;
    }
}