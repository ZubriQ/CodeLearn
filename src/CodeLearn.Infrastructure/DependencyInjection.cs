using CodeLearn.Application.Common.Interfaces;
using CodeLearn.Domain.Testings.ValueObjects;
using CodeLearn.Domain.TestingSessions.ValueObjects;
using CodeLearn.Domain.Tests.ValueObjects;
using CodeLearn.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CodeLearn.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor<TestId>>();
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor<TestingId>>();
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor<TestingSessionId>>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor<TestId>>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor<TestingId>>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor<TestingSessionId>>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseSqlServer(connectionString);
        });

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