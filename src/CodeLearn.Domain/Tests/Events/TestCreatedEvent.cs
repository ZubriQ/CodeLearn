namespace CodeLearn.Domain.Tests.Events;

public class TestCreatedEvent(Test test) : BaseEvent
{
    public Test Test { get; } = test;
}