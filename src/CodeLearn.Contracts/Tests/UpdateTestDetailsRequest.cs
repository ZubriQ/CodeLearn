namespace CodeLearn.Contracts.Tests;

public record UpdateTestDetailsRequest(
    string Title,
    string Description,
    bool IsPublic);