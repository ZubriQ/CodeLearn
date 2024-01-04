using CodeLearn.Domain.Tests;

namespace CodeLearn.Application.Tests.Queries.GetTestById;

public record GetTestByIdQuery(Guid TestId) : IRequest<OneOf<Test, NotFound>>;