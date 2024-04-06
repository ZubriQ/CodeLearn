using CodeLearn.Domain.StudentGroups.ValueObjects;
using CodeLearn.Domain.Testings;
using CodeLearn.Domain.Tests.ValueObjects;

namespace CodeLearn.Application.Testings.Commands.CreateTesting;

public record CreateTestingCommand(
    int TestId,
    int StudentGroupId,
    DateTimeOffset DeadlineDate,
    int DurationInMinutes)
    : IRequest<OneOf<int, ValidationFailed, NotFound>>;

public class CreateTestingCommandHandler(
    IApplicationDbContext _context,
    IValidator<CreateTestingCommand> _validator)
    : IRequestHandler<CreateTestingCommand, OneOf<int, ValidationFailed, NotFound>>
{
    public async Task<OneOf<int, ValidationFailed, NotFound>> Handle(CreateTestingCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return new ValidationFailed(validationResult.Errors);
        }

        var testExists = await _context.Tests
            .AnyAsync(x => x.Id == TestId.Create(request.TestId), cancellationToken);

        if (!testExists)
        {
            return new NotFound();
        }

        var studentGroupExists = await _context.StudentGroups
            .AnyAsync(x => x.Id == StudentGroupId.Create(request.StudentGroupId), cancellationToken);

        if (!studentGroupExists)
        {
            return new NotFound();
        }

        var testing = Testing.Create(
            TestId.Create(request.TestId),
            StudentGroupId.Create(request.StudentGroupId),
            request.DeadlineDate,
            request.DurationInMinutes);

        _context.Testings.Add(testing);

        await _context.SaveChangesAsync(cancellationToken);

        return testing.Id.Value;
    }
}