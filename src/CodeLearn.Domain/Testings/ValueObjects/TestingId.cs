
namespace CodeLearn.Domain.Testings.ValueObjects;

public sealed class TestingId(int value) : ValueObject
{
    public int Value { get; private set; } = value;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}