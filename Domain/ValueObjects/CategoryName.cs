using Domain.Common;
using Domain.Exceptions;
using Domain.Extensions;

namespace Domain.ValueObjects;

public class CategoryName: ValueObject
{
    public const int MaxLength = 50;
    
    public string? Text { get; }
    
    public CategoryName(string? text)
    {
        if (text.IsEmpty)
            throw new RuleViolationDomainException("The provided category name must not be null or empty.");

        if (text?.Length > MaxLength)
            throw new RuleViolationDomainException($"The provided category name must be smaller or equal to {MaxLength}.");
        
        Text = text;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Text ?? string.Empty;
    }
}