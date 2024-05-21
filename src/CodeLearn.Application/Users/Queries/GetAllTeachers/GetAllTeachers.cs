using CodeLearn.Application.Common.Models;
using CodeLearn.Domain.Constants;

namespace CodeLearn.Application.Users.Queries.GetAllTeachers;

public record GetAllTeachersQuery() : IRequest<UserDto[]>;

public class GetAllStudentsQueryHandler(IIdentityService _identityService) : IRequestHandler<GetAllTeachersQuery, UserDto[]>
{
    public async Task<UserDto[]> Handle(GetAllTeachersQuery request, CancellationToken cancellationToken)
    {
        var teachers = await _identityService.GetUsersInRoleAsync(Roles.Teacher);

        return teachers.Length == 0 ? [] : teachers;
    }
}