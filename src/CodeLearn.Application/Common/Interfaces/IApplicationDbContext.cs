using CodeLearn.Domain.Exercises;
using CodeLearn.Domain.Exercises.Entities;
using CodeLearn.Domain.ExerciseSubmissions;
using CodeLearn.Domain.ExerciseSubmissions.JunctionTables;
using CodeLearn.Domain.QuestionChoices;
using CodeLearn.Domain.StudentGroups;
using CodeLearn.Domain.Teachers;
using CodeLearn.Domain.Testings;
using CodeLearn.Domain.TestingSessions;
using CodeLearn.Domain.Tests;

namespace CodeLearn.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Teacher> Teachers { get; }
    DbSet<StudentGroup> StudentGroups { get; }

    DbSet<DataType> DataTypes { get; }
    DbSet<ExerciseNote> ExerciseNotes { get; }
    DbSet<ExerciseTopic> ExerciseTopics { get; }
    DbSet<MethodParameter> MethodParameters { get; }
    DbSet<QuestionChoice> QuestionChoices { get; }
    DbSet<SelectedChoice> SelectedChoices { get; }
    DbSet<TestCase> TestCases { get; }
    DbSet<TestCaseParameter> TestCaseParameters { get; }
    DbSet<Test> Tests { get; }
    DbSet<Testing> Testings { get; }
    DbSet<TestingSession> TestingSessions { get; }

    DbSet<Exercise> Exercises { get; }
    DbSet<MethodCodingExercise> MethodCodingExercises { get; }
    DbSet<ClassCodingExercise> ClassCodingExercises { get; }
    DbSet<QuestionExercise> QuestionExercises { get; }

    DbSet<ExerciseSubmission> ExerciseSubmissions { get; }
    DbSet<ChoiceExerciseSubmission> ChoiceExerciseSubmissions { get; }
    DbSet<CodeExerciseSubmission> CodeExerciseSubmissions { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}