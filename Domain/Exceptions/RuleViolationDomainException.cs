using Domain.Extensions;

namespace Domain.Exceptions;

/// <summary>
/// Represents an error when an expected domain entity violates a business rule.
/// </summary>
public class RuleViolationDomainException(string message)
    : DomainException(message.IsEmpty ? DefaultMessage : message)
{
    private const string DefaultMessage = "A domain rule was violated.";
}