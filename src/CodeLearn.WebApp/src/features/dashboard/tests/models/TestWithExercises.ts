import { ExerciseDetails } from '@/features/dashboard/tests/models/ExerciseDetails.ts';

export type TestWithExercises = {
  id: number;
  title: string;
  description: string;
  isPublic: boolean;
  created: Date;
  lastModified: Date;
  methodCodingExercises: ExerciseDetails[];
  questionExercises: ExerciseDetails[];
};
