using CodeLearn.Domain.Tests;

namespace CodeLearn.Application.Tests.Commands.CreateTest;

public record CreateTestCommand(string Title, string Description) : IRequest<OneOf<int, ValidationFailed>>;

public class CreateTestCommandHandler(
    IApplicationDbContext _context,
    IValidator<CreateTestCommand> _validator)
    : IRequestHandler<CreateTestCommand, OneOf<int, ValidationFailed>>
{
    public async Task<OneOf<int, ValidationFailed>> Handle(CreateTestCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return new ValidationFailed(validationResult.Errors);
        }

        var test = Test.Create(request.Title, request.Description);

        _context.Tests.Add(test);

        await _context.SaveChangesAsync(cancellationToken);

        return test.Id.Value;
    }
}