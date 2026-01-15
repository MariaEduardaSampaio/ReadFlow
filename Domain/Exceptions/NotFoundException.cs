namespace Domain.Exceptions;

/// <summary>
/// Represents an error when an expected domain entity is not found.
/// </summary>
public sealed class NotFoundException : Exception
{
    public string EntityName { get; }
    public object? Key { get; }

    public NotFoundException(string entityName)
        : base($"{entityName} was not found.")
    {
        EntityName = entityName;
    }

    public NotFoundException(string entityName, object key)
        : base($"{entityName} with identifier '{key}' was not found.")
    {
        EntityName = entityName;
        Key = key;
    }
}