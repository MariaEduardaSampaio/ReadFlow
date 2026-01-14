using System.Text.RegularExpressions;
using Domain.Common;
using Domain.Exceptions;
using Domain.Extensions;

namespace Domain.ValueObjects;

public sealed class Isbn : ValueObject
{
    private static readonly Regex Isbn10Regex = new(@"^\d{9}[\dX]$", RegexOptions.Compiled | RegexOptions.CultureInvariant);
    private static readonly Regex Isbn13Regex = new(@"^\d{13}$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

    public string Value { get; }

    public Isbn(string? value)
    {
        if (value.IsEmpty)
            throw new RuleViolationDomainException("ISBN cannot be empty.");

        var normalized = Normalize(value);

        if (normalized.Length == 10)
        {
            if (!Isbn10Regex.IsMatch(normalized))
                throw new RuleViolationDomainException("ISBN-10 format is invalid.");

            if (!IsValidIsbn10(normalized))
                throw new RuleViolationDomainException("ISBN-10 checksum is invalid.");

            Value = normalized;
            return;
        }
        else if (normalized.Length == 13)
        {
            if (!Isbn13Regex.IsMatch(normalized))
                throw new RuleViolationDomainException("ISBN-13 format is invalid.");

            if (!IsValidIsbn13(normalized))
                throw new RuleViolationDomainException("ISBN-13 checksum is invalid.");

            Value = normalized;
            return;
        }

        throw new RuleViolationDomainException("ISBN must have 10 or 13 characters (after normalization).");
    }

    private static string Normalize(string raw)
        => raw.Trim()
              .Replace("-", string.Empty)
              .Replace(" ", string.Empty)
              .ToUpperInvariant();

    private static bool IsValidIsbn10(string isbn10)
    {
        int sum = 0;

        for (int i = 0; i < 10; i++)
        {
            int digit = (i == 9 && isbn10[i] == 'X') ? 10 : (isbn10[i] - '0');
            int weight = 10 - i;
            sum += digit * weight;
        }

        return sum % 11 == 0;
    }

    private static bool IsValidIsbn13(string isbn13)
    {
        var first12 = isbn13.Substring(0, 12);
        var expected = ComputeIsbn13CheckDigit(first12);
        var actual = isbn13[12] - '0';
        return expected == actual;
    }

    private static int ComputeIsbn13CheckDigit(string first12Digits)
    {
        int sum = 0;

        for (int i = 0; i < 12; i++)
        {
            int digit = first12Digits[i] - '0';
            sum += (i % 2 == 0) ? digit : digit * 3;
        }

        int mod = sum % 10;
        return mod == 0 ? 0 : 10 - mod;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
