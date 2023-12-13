namespace CodeLearn.Domain.Exercises.Entities;

public sealed class ExerciseNote : BaseEntity<ExerciseNoteId>
{
    public ExerciseId ExerciseId { get; private set; } = null!;

    public string Entry { get; private set; } = null!;

    public NoteDecoration NoteDecoration { get; private set; }

    private ExerciseNote() { }

    private ExerciseNote(
        ExerciseId exerciseId, 
        string entry, 
        NoteDecoration noteDecoration = NoteDecoration.None)
    {
        ExerciseId = exerciseId;
        Entry = entry;
        NoteDecoration = noteDecoration;
    }

    public static ExerciseNote Create(
        ExerciseId exerciseId,
        string entry,
        NoteDecoration noteDecoration)
    {
        return new(
            exerciseId, 
            entry, 
            noteDecoration);
    }
}