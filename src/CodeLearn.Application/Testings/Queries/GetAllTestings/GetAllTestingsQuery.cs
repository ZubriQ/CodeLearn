using CodeLearn.Domain.Testings;

namespace CodeLearn.Application.Testings.Queries.GetAllTestings;

public record GetAllTestingsQuery : IRequest<Testing[]>;