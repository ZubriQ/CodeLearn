import { InputOutputExample } from '@/features/dashboard/tests/models/InputOutputExample.ts';
import { ExerciseDifficulty } from '@/features/dashboard/tests/models/ExerciseDifficulty.ts';
import { ExerciseNote } from '@/features/testing-session/models/ExerciseNote.ts';
import { ExerciseTopic } from '@/features/testing-session/models/ExerciseTopic.ts';

export type MethodCodingExercise = {
  // Exercise fields
  id: number;
  testId: number;
  title: string;
  description: string;
  difficulty: ExerciseDifficulty;
  // Method coding fields
  methodSolutionCode?: string;
  exerciseTopics?: ExerciseTopic[];
  exerciseNotes?: ExerciseNote[];
  inputOutputExamples?: InputOutputExample[];
};
