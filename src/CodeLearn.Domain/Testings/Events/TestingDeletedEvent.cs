namespace CodeLearn.Domain.Testings.Events;

public class TestingDeletedEvent(Testing testing) : BaseEvent
{
    public Testing Testing { get; } = testing;
}