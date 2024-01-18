using CodeLearn.Application.Common.Interfaces;

namespace CodeLearn.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}