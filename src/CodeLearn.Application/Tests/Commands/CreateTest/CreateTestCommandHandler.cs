using CodeLearn.Domain.Tests;

namespace CodeLearn.Application.Tests.Commands.CreateTest;

public class CreateTestCommandHandler(
    IApplicationDbContext context,
    IValidator<CreateTestCommand> validator)
    : IRequestHandler<CreateTestCommand, OneOf<int, ValidationFailed>>
{
    public async Task<OneOf<int, ValidationFailed>> Handle(CreateTestCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new ValidationFailed(validationResult.Errors);
        }

        var test = Test.Create(request.Title, request.Description);

        context.Tests.Add(test);

        await context.SaveChangesAsync(cancellationToken);

        return test.Id.Value;
    }
}