namespace Domain.Extensions;

public static class StringExtensions
{
    extension(string? value)
    {
        public bool IsEmpty => string.IsNullOrWhiteSpace(value);

        public bool HasText => !string.IsNullOrWhiteSpace(value);
    }
}