using CodeLearn.Domain.Testings;

namespace CodeLearn.Application.Testings.Queries.GetTestingById;

public record GetTestingByIdQuery(Guid TestingId) : IRequest<OneOf<Testing, NotFound>>;