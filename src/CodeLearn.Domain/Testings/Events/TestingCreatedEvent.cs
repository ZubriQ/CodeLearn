namespace CodeLearn.Domain.Testings.Events;

public class TestingCreatedEvent(Testing testing) : BaseEvent
{
    public Testing Testing { get; } = testing;
}