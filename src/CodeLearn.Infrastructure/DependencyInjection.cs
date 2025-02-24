using CodeLearn.Application.Common.Interfaces;
using CodeLearn.Application.ExerciseSubmissions.MethodCoding.Commands.CreateExerciseSubmission;
using CodeLearn.CodeEngine.Interfaces;
using CodeLearn.CodeEngine.Processing;
using CodeLearn.Domain.StudentGroups.ValueObjects;
using CodeLearn.Domain.Testings.ValueObjects;
using CodeLearn.Domain.TestingSessions.ValueObjects;
using CodeLearn.Domain.Tests.ValueObjects;
using CodeLearn.Infrastructure.Jobs;
using CodeLearn.Infrastructure.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using System.Text;

namespace CodeLearn.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuditingInterceptors();

        var connectionString = configuration.GetConnectionString("LocalConnection");
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
            options.Password.RequiredLength = 3;
            options.Password.RequiredUniqueChars = 0;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.User.RequireUniqueEmail = false;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddJwtAuth(configuration);

        services.AddScoped<IFileProcessingService, FileProcessingService>();

        services.AddCodeTesterService();

        services.AddBackgroundJobs();

        return services;
    }

    private static IServiceCollection AddAuditingInterceptors(this IServiceCollection services)
    {
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor<TestId>>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor<TestId>>();

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor<TestingId>>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor<TestingId>>();

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor<TestingSessionId>>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor<TestingSessionId>>();

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor<StudentGroupId>>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor<StudentGroupId>>();

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
        services
            .AddAuthentication(options =>
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
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                };
            })
            .AddNegotiate();

        services.AddSingleton<ITokenService, TokenService>();

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

    private static IServiceCollection AddCodeTesterService(this IServiceCollection services)
    {
        services.AddScoped<ICodeFormatter, CodeFormatter>();
        services.AddScoped<ICodeCompiler, CodeCompiler>();
        services.AddScoped<ICodeTester, CodeTester>();

        services.AddScoped<ICodeTesterService, CodeTesterService>();

        return services;
    }

    private static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
    {
        services.AddQuartz();

        services.AddQuartzHostedService();

        services.ConfigureOptions<FinishingTestingsAndSessionsBackgroundJobSetup>();

        return services;
    }
}