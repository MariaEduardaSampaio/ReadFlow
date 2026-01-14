using Domain.Common;
using Domain.Exceptions;

namespace Domain.ValueObjects;

public class Rating: ValueObject
{
    public const int MaxValue = 10;
    public int Value { get; }
    
    protected Rating() { }
    
    public Rating(int? value)
    {
        if (value < 0 || value > MaxValue)
            throw new RuleViolationDomainException($"The provided value must be greater than or equal to 0 and smaller or equal to {MaxValue}.");
        
        Value = value ?? default(int);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
