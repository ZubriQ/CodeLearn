using CodeLearn.Application.Common.Interfaces;
using CodeLearn.Domain.Exercises;
using CodeLearn.Domain.Exercises.Entities;
using CodeLearn.Domain.ExerciseSubmissions;
using CodeLearn.Domain.ExerciseSubmissions.JunctionTables;
using CodeLearn.Domain.QuestionChoices;
using CodeLearn.Domain.Students;
using CodeLearn.Domain.Students.Entities;
using CodeLearn.Domain.Teachers;
using CodeLearn.Domain.Testings;
using CodeLearn.Domain.TestingSessions;
using CodeLearn.Domain.Tests;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;

namespace CodeLearn.Infrastructure.Data;

public sealed class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Teacher> Teachers => Set<Teacher>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<StudentGroup> StudentGroups => Set<StudentGroup>();

    public DbSet<DataType> DataTypes => Set<DataType>();
    public DbSet<ExerciseNote> ExerciseNotes => Set<ExerciseNote>();
    public DbSet<ExerciseTopic> ExerciseTopics => Set<ExerciseTopic>();
    public DbSet<MethodParameter> MethodParameters => Set<MethodParameter>();
    public DbSet<QuestionChoice> QuestionChoices => Set<QuestionChoice>();
    public DbSet<SelectedChoice> SelectedChoices => Set<SelectedChoice>();
    public DbSet<TestCase> TestCases => Set<TestCase>();
    public DbSet<TestCaseParameter> TestCaseParameters => Set<TestCaseParameter>();
    public DbSet<Test> Tests => Set<Test>();
    public DbSet<Testing> Testings => Set<Testing>();
    public DbSet<TestingSession> TestingSessions => Set<TestingSession>();

    public DbSet<Exercise> Exercises => Set<Exercise>();
    public DbSet<MethodCodingExercise> MethodCodingExercises => Set<MethodCodingExercise>();
    public DbSet<ClassCodingExercise> ClassCodingExercises => Set<ClassCodingExercise>();
    public DbSet<QuestionExercise> QuestionExercises => Set<QuestionExercise>();

    public DbSet<ExerciseSubmission> ExerciseSubmissions => Set<ExerciseSubmission>();
    public DbSet<ChoiceExerciseSubmission> ChoiceExerciseSubmissions => Set<ChoiceExerciseSubmission>();
    public DbSet<CodeExerciseSubmission> CodeExerciseSubmissions => Set<CodeExerciseSubmission>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}