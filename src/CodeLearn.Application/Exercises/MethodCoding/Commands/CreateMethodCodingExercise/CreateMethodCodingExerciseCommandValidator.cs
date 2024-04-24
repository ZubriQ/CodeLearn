namespace CodeLearn.Application.Exercises.MethodCodingExercises.Commands.CreateMethodCodingExercise;

public class CreateMethodCodingExerciseCommandValidator : AbstractValidator<CreateMethodCodingExerciseCommand>
{
    public CreateMethodCodingExerciseCommandValidator()
    {
        RuleFor(x => x.TestId)
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.Title)
           .NotEmpty()
           .MaximumLength(50);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(50);

        // TODO: Add the rest props validation
    }
}