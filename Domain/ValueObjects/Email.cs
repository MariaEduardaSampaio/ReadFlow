using System.Text.RegularExpressions;
using Domain.Common;
using Domain.Exceptions;

namespace Domain.ValueObjects;

public sealed class Email : ValueObject
{
    public const int MaxLength = 254;

    // regex: good for most real-world emails without being overly strict.
    // - Requires one @
    // - Local part allows common characters
    // - Domain requires at least one dot and valid TLD length
    private static readonly Regex EmailRegex = new(
        pattern: @"^(?=.{1,254}$)(?=.{1,64}@)[A-Za-z0-9.!#$%&'*+/=?^_`{|}~-]+@([A-Za-z0-9-]+\.)+[A-Za-z]{2,63}$",
        options: RegexOptions.Compiled | RegexOptions.CultureInvariant
    );

    public string Address { get; }

    public Email(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new RuleViolationDomainException("The email address cannot be empty.");

        address = address.Trim();

        if (address.Length > MaxLength)
            throw new RuleViolationDomainException($"The email address must be smaller or equal to {MaxLength} characters.");

        if (!EmailRegex.IsMatch(address))
            throw new RuleViolationDomainException("The email address is not in a valid format.");

        Address = address.ToLowerInvariant();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Address;
    }

    public override string ToString() => Address;
}