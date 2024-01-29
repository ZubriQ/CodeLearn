using CodeLearn.Domain.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CodeLearn.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        await SeedDefaultRoles();
        await EnsureAdministratorExists();
    }

    private async Task SeedDefaultRoles()
    {
        var defaultRoles = new List<IdentityRole>()
        {
            new(Roles.Administrator),
            new(Roles.Teacher),
            new(Roles.Student),
        };

        foreach (var defaultRole in defaultRoles)
        {
            if (_roleManager.Roles.All(r => r.Name != defaultRole.Name))
            {
                await _roleManager.CreateAsync(defaultRole);
            }
        }
    }

    private async Task EnsureAdministratorExists()
    {
        var adminUsers = await _userManager.GetUsersInRoleAsync(Roles.Administrator);

        if (!adminUsers.Any())
        {
            var adminUser = new ApplicationUser
            {
                FirstName = "AdminFirstName",
                LastName = "AdminLastName",
                UserName = "admin",
                Email = "Adm1n@example.com"
            };

            var createUserResult = await _userManager.CreateAsync(adminUser, "Adm1n@example.com");
            if (createUserResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(adminUser, Roles.Administrator);
            }
        }
    }
}