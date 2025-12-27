namespace Domain.Aggregates.ReadingItems.ValueObjects;

public sealed record Rating
{
    public int Value { get; }

    private Rating(int value) => Value = value;

    public static Rating Create(int value)
    {
        if (value < 0 || value > 10)
            throw new ArgumentOutOfRangeException(nameof(value), "Rating must be between 0 and 10.");

        return new Rating(value);
    }
}
