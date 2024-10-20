using CodeLearn.Api.Services;
using CodeLearn.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace CodeLearn.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
        });

        services.AddScoped<IUser, CurrentUser>();

        services.AddHttpContextAccessor();

        // TODO: Add health checks / exception handlers

        services.AddEndpointsApiExplorer();

        services.AddSwaggerWithJwt();

        services.AddMappings();

        services.AddRouting(options => options.LowercaseUrls = true);

        return services;
    }

    private static IServiceCollection AddSwaggerWithJwt(this IServiceCollection services)
    {
        services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "CodeLearn Web API",
                Version = "v1"
            });
            s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter: 'Bearer [token]'",
            });
            s.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
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