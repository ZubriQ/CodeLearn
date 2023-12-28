using CodeLearn.Domain.Testings;

namespace CodeLearn.Application.Testings.Commands.CreateTesting;

public class CreateTestingCommandHandler(
    IApplicationDbContext context,
    IValidator<CreateTestingCommand> validator)
    : IRequestHandler<CreateTestingCommand, OneOf<Guid, ValidationFailed>>
{
    public async Task<OneOf<Guid, ValidationFailed>> Handle(CreateTestingCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new ValidationFailed(validationResult.Errors);
        }

        var testing = Testing.Create(request.TeacherId, request.Title, request.Description, request.DurationInMinutes);

        context.Testings.Add(testing);

        await context.SaveChangesAsync(cancellationToken);

        return testing.Id.Value;
    }
}