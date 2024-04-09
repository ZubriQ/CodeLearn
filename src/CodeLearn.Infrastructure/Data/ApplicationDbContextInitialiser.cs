using CodeLearn.Domain.Constants;
using CodeLearn.Domain.Exercises.Entities;
using CodeLearn.Domain.ExerciseTopics;
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
        await SeedDataTypes();
        await SeedExerciseTopics();
        await EnsureAdministratorExists();
        await EnsureTestStudentExistExists();
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

    private async Task EnsureTestStudentExistExists()
    {
        var studentUsers = await _userManager.GetUsersInRoleAsync(Roles.Student);

        if (!studentUsers.Any())
        {
            var student = new ApplicationUser
            {
                FirstName = "firstName",
                LastName = "lastName",
                UserName = "student",
                Email = "Stud3nt@example.com",
                StudentGroupName = "PIB",
            };

            var createUserResult = await _userManager.CreateAsync(student, "Stud3nt@example.com");

            if (createUserResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(student, Roles.Student);
            }
        }
    }

    private async Task SeedDataTypes()
    {
        if (!_context.DataTypes.Any())
        {
            var dataTypes = new List<DataType>
            {
                DataType.Create("System.Void", "void"),
                DataType.Create("System.Object", "object"),
                DataType.Create("System.Boolean", "bool"),
                DataType.Create("System.String", "string"),
                DataType.Create("System.Char", "char"),
                DataType.Create("System.Byte", "byte"),
                DataType.Create("System.Int32", "int"),
                DataType.Create("System.Int64", "long"),
                DataType.Create("System.Double", "double"),
                DataType.Create("System.Single", "float"),
            };

            _context.DataTypes.AddRange(dataTypes);

            await _context.SaveChangesAsync();
        }
    }

    private async Task SeedExerciseTopics()
    {
        if (!_context.ExerciseTopics.Any())
        {
            var topics = new List<ExerciseTopic>
            {
                ExerciseTopic.Create("Array"),
                ExerciseTopic.Create("String"),
                ExerciseTopic.Create("Math"),
                ExerciseTopic.Create("Dynamic Programming"),
                ExerciseTopic.Create("Greedy"),
                ExerciseTopic.Create("Sorting"),
                ExerciseTopic.Create("Binary Search"),
                ExerciseTopic.Create("Linked List"),
                ExerciseTopic.Create("Bredth-First Search"),
                ExerciseTopic.Create("Tree"),
                ExerciseTopic.Create("Binary Tree"),
                ExerciseTopic.Create("Trie"),
                ExerciseTopic.Create("Matrix"),
                ExerciseTopic.Create("Two Pointers"),
                ExerciseTopic.Create("Stack"),
                ExerciseTopic.Create("Queue"),
                ExerciseTopic.Create("Hashing"),
                ExerciseTopic.Create("Backtracking"),
                ExerciseTopic.Create("Geometry"),
            };

            _context.ExerciseTopics.AddRange(topics);

            await _context.SaveChangesAsync();
        }
    }
}