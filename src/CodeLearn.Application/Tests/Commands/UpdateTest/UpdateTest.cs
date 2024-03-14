using CodeLearn.Domain.Tests.ValueObjects;

namespace CodeLearn.Application.Tests.Commands.UpdateTest;

public record UpdateTestCommand(int Id, string Title, string Description, bool IsPublic)
    : IRequest<OneOf<Success, NotFound, BadRequest>>;

public class UpdateTestCommandHandler(IApplicationDbContext context)
    : IRequestHandler<UpdateTestCommand, OneOf<Success, NotFound, BadRequest>>
{
    public async Task<OneOf<Success, NotFound, BadRequest>> Handle(UpdateTestCommand request, CancellationToken cancellationToken)
    {
        var test = await context.Tests
            .FirstOrDefaultAsync(x => x.Id == TestId.Create(request.Id), cancellationToken);

        if (test is null)
        {
            return new NotFound();
        }

        var result = test.UpdateDetails(request.Title, request.Description, request.IsPublic);

        if (result.IsFailure)
        {
            return new BadRequest();
        }

        await context.SaveChangesAsync(cancellationToken);

        return new Success();
    }
}