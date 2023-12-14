namespace CodeLearn.Domain.Exercises.Entities;

public sealed class ExerciseNote : BaseEntity<ExerciseNoteId>
{
    public ExerciseId ExerciseId { get; private set; } = null!;

    public string Entry { get; private set; } = null!;

    public ExerciseNoteDecoration NoteDecoration { get; private set; }

    private ExerciseNote() { }

    private ExerciseNote(
        ExerciseNoteId exerciseNoteId,
        ExerciseId exerciseId,
        string entry,
        ExerciseNoteDecoration noteDecoration)
        : base(exerciseNoteId)
    {
        ExerciseId = exerciseId;
        Entry = entry;
        NoteDecoration = noteDecoration;
    }

    public static ExerciseNote Create(
        ExerciseId exerciseId,
        string entry,
        ExerciseNoteDecoration noteDecoration = ExerciseNoteDecoration.Plain)
    {
        return new(
            ExerciseNoteId.CreateUnique(),
            exerciseId,
            entry,
            noteDecoration);
    }
}