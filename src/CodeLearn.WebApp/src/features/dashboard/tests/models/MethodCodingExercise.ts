import { DataType } from '@/features/dashboard/tests/models/DataType.ts';
import { MethodParameter } from '@/features/dashboard/tests/models/MethodParameter.ts';
import { TestCase } from '@/features/dashboard/tests/models/TestCase.ts';
import { InputOutputExample } from '@/features/dashboard/tests/models/InputOutputExample.ts';
import { ExerciseDifficulty } from '@/features/dashboard/tests/models/ExerciseDifficulty.ts';
import { ExerciseNote } from '@/features/dashboard/tests/models/ExerciseNote.ts';
import { ExerciseTopic } from '@/features/dashboard/tests/models/ExerciseTopic.ts';

export type MethodCodingExercise = {
  // Exercise fields
  id: number;
  testId: number;
  title: string;
  description: string;
  difficulty: ExerciseDifficulty;
  exerciseNotes?: ExerciseNote[];
  exerciseTopics?: ExerciseTopic[];
  // Method coding fields
  methodToExecute: string;
  methodSolutionCode?: string;
  dataType: DataType;
  methodParameters: MethodParameter[];
  testCases: TestCase[];
  inputOutputExamples?: InputOutputExample[];
};
