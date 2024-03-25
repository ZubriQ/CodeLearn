using CodeLearn.Domain.Exercises.Enums;
using CodeLearn.Domain.Exercises;
using CodeLearn.Domain.Tests.ValueObjects;
using FluentValidation.Results;
using CodeLearn.Domain.Exercises.Entities;
using CodeLearn.Domain.Exercises.ValueObjects;

namespace CodeLearn.Application.Exercises.Commands.CreateMethodCodingExercise;

public record ExerciseNoteModel(string Entry, string Decoration);

public record InputOutputExampleModel(string Input, string Output);

public record MethodParameterModel(int DataTypeId, int Position);

public record TestCaseParameterModel(string Value, int Position);

public record TestCaseModel(string CorrectOutputValue, TestCaseParameterModel[] TestCaseParameters);

public record CreateMethodCodingExerciseCommand(
    int TestId,
    string Title,
    string Description,
    string Difficulty,
    int[] ExerciseTopics,
    string MethodToExecute,
    string MethodSolutionCode,
    int MethodReturnTypeId,
    ExerciseNoteModel[] ExerciseNotes,
    InputOutputExampleModel[] InputOutputExamples,
    MethodParameterModel[] MethodParameters,
    TestCaseModel[] TestCases)
    : IRequest<OneOf<int, ValidationFailed, NotFound>>;

public class CreateMethodCodingExerciseCommandHandler(
    IApplicationDbContext context,
    IValidator<CreateMethodCodingExerciseCommand> validator) 
    : IRequestHandler<CreateMethodCodingExerciseCommand, OneOf<int, ValidationFailed, NotFound>>
{
    public async Task<OneOf<int, ValidationFailed, NotFound>> Handle(CreateMethodCodingExerciseCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return new ValidationFailed(validationResult.Errors);
        }

        if (!Enum.TryParse<ExerciseDifficulty>(request.Difficulty, true, out var difficultyEnum))
        {
            var validationFailure = ValidateDifficultyEnum(request);
            return new ValidationFailed(validationFailure);
        }

        var dataType = context.DataTypes
            .FirstOrDefault(x => x.Id == DataTypeId.Create(request.MethodReturnTypeId));

        if (dataType is null)
        {
            return new NotFound();
        }

        var exercise = MethodCodingExercise.Create(
            TestId.Create(request.TestId),
            request.Title,
            request.Description,
            difficultyEnum,
            request.MethodToExecute,
            request.MethodSolutionCode,
            dataType);

        foreach (var note in request.ExerciseNotes)
        {
            if (!Enum.TryParse<ExerciseNoteDecoration>(note.Decoration, true, out var noteDecorationEnum))
            {
                var validationFailure = ValidateDecorationEnum(note.Decoration);
                return new ValidationFailed(validationFailure);
            }

            var newNote = ExerciseNote.Create(exercise.Id, note.Entry, noteDecorationEnum);
            exercise.AddNote(newNote);
        }

        foreach (var example in request.InputOutputExamples)
        {
            var newExample = InputOutputExample.Create(exercise.Id, example.Input, example.Output);
            exercise.AddExample(newExample);
        }

        foreach (var methodParameter in request.MethodParameters)
        {
            var parameterDataType = await context.DataTypes
                .FirstOrDefaultAsync(x => x.Id.Value == methodParameter.DataTypeId);

            if (parameterDataType is null)
            {
                return new NotFound();
            }
            // Is id generated?
            var parameter = MethodParameter.Create(exercise.Id, parameterDataType, methodParameter.Position);

            exercise.AddMethodParameter(parameter);
        }

        foreach(var testCase in request.TestCases)
        {
            var newTestCase = TestCase.Create(exercise.Id, testCase.CorrectOutputValue);

            foreach (var testCaseParameter in testCase.TestCaseParameters)
            {
                // Is id generated?
                var parameter = TestCaseParameter.Create(newTestCase.Id, testCaseParameter.Value, testCaseParameter.Position);
                newTestCase.AddTestCaseParameter(parameter);
            }

            exercise.AddTestCase(newTestCase);
        }

        context.MethodCodingExercises.Add(exercise);

        await context.SaveChangesAsync(cancellationToken);

        return exercise.Id.Value;
    }
    
    private static List<ValidationFailure> ValidateDifficultyEnum(CreateMethodCodingExerciseCommand request)
    {
        return [new(nameof(request.Difficulty), $"Invalid difficulty level: {request.Difficulty}.")];
    }

    private static List<ValidationFailure> ValidateDecorationEnum(string decoration)
    {
        return [new(nameof(decoration), $"Invalid difficulty level: {decoration}.")];
    }
}