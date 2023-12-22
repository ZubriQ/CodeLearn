using CodeLearn.Application.Common.Interfaces;
using CodeLearn.Domain.Teachers.ValueObjects;
using CodeLearn.Domain.Testings;

namespace CodeLearn.Application.Testings.Commands;

public record CreateTestingCommand : IRequest<Guid>
{
    public TeacherId? TeacherId { get; init; }
    public string? Title { get; init; }
    public string? Description { get; init; }
    public int DurationInMinutes { get; init; }
}

public class CreateTestingCommandHandler : IRequestHandler<CreateTestingCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateTestingCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateTestingCommand command, CancellationToken cancellationToken)
    {
        // TODO: Validate

        var testing = Testing.Create(command.TeacherId, command.Title, command.Description, command.DurationInMinutes);

        _context.Testings.Add(testing);

        await _context.SaveChangesAsync(cancellationToken);

        return testing.Id.Value;
    }
}