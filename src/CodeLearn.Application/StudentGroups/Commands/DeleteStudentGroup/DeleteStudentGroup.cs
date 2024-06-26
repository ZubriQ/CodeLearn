﻿using CodeLearn.Domain.StudentGroups.ValueObjects;

namespace CodeLearn.Application.StudentGroups.Commands.DeleteStudentGroup;

public record DeleteStudentGroupCommand(int Id) : IRequest<OneOf<Success, NotFound>>;

public class DeleteStudentGroupCommandHandler(IApplicationDbContext _context)
    : IRequestHandler<DeleteStudentGroupCommand, OneOf<Success, NotFound>>
{
    public async Task<OneOf<Success, NotFound>> Handle(DeleteStudentGroupCommand request, CancellationToken cancellationToken)
    {
        var studentGroup = await _context.StudentGroups
            .FirstOrDefaultAsync(t => t.Id == StudentGroupId.Create(request.Id), cancellationToken);

        if (studentGroup is null)
        {
            return new NotFound();
        }

        _context.StudentGroups.Remove(studentGroup);

        await _context.SaveChangesAsync(cancellationToken);

        return new Success();
    }
}