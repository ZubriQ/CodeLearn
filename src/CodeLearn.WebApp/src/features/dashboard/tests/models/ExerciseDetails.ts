import { ExerciseDifficulty } from '@/features/dashboard/tests/models/ExerciseDifficulty.ts';

export type ExerciseDetails = {
  id: number;
  title: string;
  description: string;
  difficulty: ExerciseDifficulty;
};
