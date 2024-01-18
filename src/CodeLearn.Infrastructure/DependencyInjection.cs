using CodeLearn.Application.Common.Interfaces;
using CodeLearn.Infrastructure.Authentication;
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

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor<int>>();
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor<long>>();
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor<Guid>>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor<int>>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor<long>>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor<Guid>>();

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
            //.AddDefaultTokenProviders()
            //.AddDefaultUI();

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
            //options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
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
        
        //var jwtSettings = new JwtSettings();
        //configration.Bind(JwtSettings.SectionName, jwtSettings);

        //services.AddSingleton(Options.Create(jwtSettings));

        //services
        //    .AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme);
        //.AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
        //{
        //    ValidateIssuer = true,
        //    ValidateAudience = true,
        //    ValidateLifetime = true,
        //    ValidateIssuerSigningKey = true,
        //    ValidIssuer = jwtSettings.Issuer,
        //    ValidAudience = jwtSettings.Audience,
        //    IssuerSigningKey = new SymmetricSecurityKey(
        //        Encoding.UTF8.GetBytes(jwtSettings.Secret)),
        //});

        //services
        //    .AddAuthorizationBuilder()
        //    .AddPolicy("api", policy =>
        //    {
        //        policy.RequireAuthenticatedUser();
        //        policy.AddAuthenticationSchemes(IdentityConstants.BearerScheme);
        //    });

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