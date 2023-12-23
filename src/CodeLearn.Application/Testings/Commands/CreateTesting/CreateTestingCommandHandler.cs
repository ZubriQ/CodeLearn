using CodeLearn.Domain.Testings;

namespace CodeLearn.Application.Testings.Commands.CreateTesting;

public class CreateTestingCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateTestingCommand, Guid>
{
    public async Task<Guid> Handle(CreateTestingCommand request, CancellationToken cancellationToken)
    {
        // TODO: Validate

        var testing = Testing.Create(request.TeacherId, request.Title, request.Description, request.DurationInMinutes);

        context.Testings.Add(testing);

        await context.SaveChangesAsync(cancellationToken);

        return testing.Id.Value;
    }
}