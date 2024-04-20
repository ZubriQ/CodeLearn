import { QuestionChoice } from '@/features/testing-session/models/QuestionChoice.ts';
import { ExerciseDifficulty } from '@/features/dashboard/tests/models/ExerciseDifficulty.ts';

export type QuestionExercise = {
  // Exercise fields
  id: number;
  testId: number;
  title: string;
  description: string;
  difficulty: ExerciseDifficulty;
  // Question exercise fields
  isMultipleAnswers: boolean;
  questionChoices: QuestionChoice[];
};
