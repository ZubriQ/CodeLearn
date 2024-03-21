import { QuestionChoice } from '@/features/dashboard/tests/models/QuestionChoice.ts';
import { ExerciseDifficulty } from '@/features/dashboard/tests/models/ExerciseDifficulty.ts';
import { ExerciseNote } from '@/features/dashboard/tests/models/ExerciseNote.ts';
import { ExerciseTopic } from '@/features/dashboard/tests/models/ExerciseTopic.ts';

export type QuestionExercise = {
  // Exercise fields
  id: number;
  testId: number;
  title: string;
  description: string;
  difficulty: ExerciseDifficulty;
  exerciseNotes?: ExerciseNote[];
  exerciseTopics?: ExerciseTopic[];
  // Question exercise fields
  isMultipleAnswers: boolean;
  questionChoices: QuestionChoice[];
};
