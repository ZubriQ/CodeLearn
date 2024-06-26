﻿namespace CodeLearn.Domain.QuestionChoices;

public sealed class QuestionChoice : BaseEntity<QuestionChoiceId>, IAggregateRoot
{
    public ExerciseId ExerciseId { get; private set; }
    public string Text { get; private set; }
    public bool IsCorrect { get; private set; }

    private readonly IList<ChoiceExerciseSubmission> _exerciseSubmissions = [];
    public IReadOnlyList<ChoiceExerciseSubmission> ExerciseSubmissions => _exerciseSubmissions.AsReadOnly();

    private QuestionChoice(
        ExerciseId exerciseId,
        string text,
        bool isCorrect)
        : base(default!)
    {
        ExerciseId = exerciseId;
        Text = text;
        IsCorrect = isCorrect;
    }

    public static QuestionChoice Create(
        ExerciseId exerciseId,
        string text,
        bool isCorrect)
    {
        return new QuestionChoice(
            exerciseId,
            text,
            isCorrect);
    }
}