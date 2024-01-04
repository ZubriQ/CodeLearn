using CodeLearn.Domain.Tests;

namespace CodeLearn.Application.Tests.Queries.GetAllTests;

public record GetAllTestsQuery : IRequest<Test[]>;