using Domain.Common;
using Domain.Exceptions;

namespace Domain.ValueObjects;

public class Review: ValueObject
{
    public const int MaxLength = 2000;
    
    public string? Text { get; }
    
    public Review(string? text)
    {
        if (text?.Length > MaxLength)
            throw new RuleViolationDomainException($"The provided review must be smaller or equal to {MaxLength}.");
        
        Text = text;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Text ?? string.Empty;
    }
}