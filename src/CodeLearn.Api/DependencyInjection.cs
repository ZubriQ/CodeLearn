using CodeLearn.Api.Services;
using CodeLearn.Application.Common.Interfaces;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace CodeLearn.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddScoped<IUser, CurrentUser>();

        services.AddHttpContextAccessor();

        // TODO: Add health checks / exception handlers

        services.AddEndpointsApiExplorer();

        services.AddSwagger();

        services.AddMappings();

        services.AddRouting(options => options.LowercaseUrls = true);

        return services;
    }

    private static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "CodeLearn Web API", Version = "v1" });
        });

        return services;
    }

    private static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);

        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}