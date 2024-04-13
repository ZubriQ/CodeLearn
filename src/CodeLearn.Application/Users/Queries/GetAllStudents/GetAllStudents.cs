using CodeLearn.Application.Common.Models;
using CodeLearn.Domain.Constants;

namespace CodeLearn.Application.Users.Queries.GetAllStudents;

public record GetAllStudentsQuery() : IRequest<UserDto[]>;

public class GetAllStudentsQueryHandler(IIdentityService _identityService) : IRequestHandler<GetAllStudentsQuery, UserDto[]>
{
    public async Task<UserDto[]> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        var students = await _identityService.GetUsersInRoleAsync(Roles.Student);

        return students.Length == 0 ? [] : students;
    }
}