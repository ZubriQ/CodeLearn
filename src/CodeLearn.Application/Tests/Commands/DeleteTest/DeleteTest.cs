using CodeLearn.Domain.Tests.ValueObjects;

namespace CodeLearn.Application.Tests.Commands.DeleteTest;

public record DeleteTestCommand(int Id) : IRequest<OneOf<Success, NotFound>>;

public class DeleteTestCommandHandler(IApplicationDbContext context)
    : IRequestHandler<DeleteTestCommand, OneOf<Success, NotFound>>
{
    public async Task<OneOf<Success, NotFound>> Handle(DeleteTestCommand request, CancellationToken cancellationToken)
    {
        var test = await context.Tests
            .FirstOrDefaultAsync(t => t.Id == TestId.Create(request.Id), cancellationToken);

        if (test is null)
        {
            return new NotFound();
        }

        context.Tests.Remove(test);

        await context.SaveChangesAsync(cancellationToken);

        return new Success();
    }
}