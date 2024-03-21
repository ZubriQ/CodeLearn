namespace CodeLearn.Domain.Exercises.Entities;

public sealed class ExerciseNote : BaseEntity<ExerciseNoteId>
{
    public ExerciseId ExerciseId { get; private set; } = null!;
    public string Entry { get; private set; } = null!;
    public ExerciseNoteDecoration Decoration { get; private set; }

    private ExerciseNote() { }

    private ExerciseNote(
        ExerciseId exerciseId,
        string entry,
        ExerciseNoteDecoration noteDecoration)
        : base(default!)
    {
        ExerciseId = exerciseId;
        Entry = entry;
        Decoration = noteDecoration;
    }

    public static ExerciseNote Create(
        ExerciseId exerciseId,
        string entry,
        ExerciseNoteDecoration noteDecoration = ExerciseNoteDecoration.Plain)
    {
        return new ExerciseNote(
            exerciseId,
            entry,
            noteDecoration);
    }
}