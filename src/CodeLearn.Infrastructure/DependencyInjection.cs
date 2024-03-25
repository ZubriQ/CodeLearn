using CodeLearn.Application.Common.Interfaces;
using CodeLearn.Application.ExerciseSubmissions.Commands.CreateExerciseSubmission;
using CodeLearn.Domain.StudentGroups.ValueObjects;
using CodeLearn.Domain.Testings.ValueObjects;
using CodeLearn.Domain.TestingSessions.ValueObjects;
using CodeLearn.Domain.Tests.ValueObjects;
using CodeLearn.Infrastructure.Authentication;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CodeLearn.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor<TestId>>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor<TestId>>();

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor<TestingId>>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor<TestingId>>();

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor<TestingSessionId>>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor<TestingSessionId>>();

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor<StudentGroupId>>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor<StudentGroupId>>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseSqlServer(connectionString);
        });

        services.ConfigureRabbitMQ(configuration);

        services.ConfigureMassTransit();

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddSingleton(TimeProvider.System);

        services.AddTransient<IIdentityService, IdentityService>();

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequiredLength = 5;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddJwtAuth(configuration);

        return services;
    }

    public static IServiceCollection ConfigureRabbitMQ(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<MessageBrokerSettings>(configuration.GetSection("MessageBroker"));

        services.AddSingleton(sp => sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

        return services;
    }

    private static IServiceCollection ConfigureMassTransit(this IServiceCollection services)
    {
        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();

            // Register new Event Consumers here
            busConfigurator.AddConsumer<ExerciseSubmissionEventConsumer>();

            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                var settings = context.GetRequiredService<MessageBrokerSettings>();

                configurator.Host(new Uri(settings.Host), h =>
                {
                    h.Username(settings.Username);
                    h.Password(settings.Password);
                });
            });
        });

        return services;
    }

    public static IServiceCollection AddJwtAuth(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidateAudience = true,
                ValidAudience = configuration["JwtSettings:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!)),
                ValidateLifetime = true
            };
        });

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.ConfigureApplicationCookie(options =>
        {
            options.Events.OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = 405;
                return Task.CompletedTask;
            };
        });

        services.AddAuthorization();

        return services;
    }
}