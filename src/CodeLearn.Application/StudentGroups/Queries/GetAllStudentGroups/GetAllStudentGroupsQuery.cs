using CodeLearn.Domain.StudentGroups;

namespace CodeLearn.Application.StudentGroups.Queries.GetAllStudentGroups;

public record GetAllStudentGroupsQuery : IRequest<StudentGroup[]>;