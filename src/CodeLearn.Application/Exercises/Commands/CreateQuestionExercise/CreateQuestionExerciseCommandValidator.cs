namespace CodeLearn.Application.Exercises.Commands.CreateQuestionExercise;

public class CreateQuestionExerciseCommandValidator : AbstractValidator<CreateQuestionExerciseCommand>
{
    public CreateQuestionExerciseCommandValidator()
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

        RuleFor(x => x.IsMultipleAnswers)
            .NotEmpty();

        RuleFor(x => x.Answers)
            .NotNull()
            .Must(x => x.Count >= 2 && x.Count <= 10);
    }
}