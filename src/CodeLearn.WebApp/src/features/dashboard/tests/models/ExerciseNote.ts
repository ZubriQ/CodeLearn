import { ExerciseNoteDecoration } from '@/features/dashboard/tests/models/ExerciseNoteDecoration.ts';

export type ExerciseNote = {
  id: number;
  entry: string;
  decoration: ExerciseNoteDecoration;
};
