using Microsoft.Extensions.Configuration;

namespace CodeLearn.Infrastructure.Utilities;

public static class ConnectionStringProvider
{
    private const string DevelopmentEnvironment = "Development";
    private const string LocalConnectionName = "LocalConnection";
    private const string DockerConnectionName = "DockerConnection";

    public static string GetConnectionString(IConfiguration configuration)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        return environment == DevelopmentEnvironment ?
            configuration.GetConnectionString(LocalConnectionName)! :
            configuration.GetConnectionString(DockerConnectionName)!;
    }
}