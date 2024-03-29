import { InputOutputExampleDto } from '@/api/exercises/InputOutputExampleDto.ts';
import { ExerciseNoteDto } from '@/api/exercises/ExerciseNoteDto.ts';
import { MethodParameterDto } from '@/api/exercises/MethodParameterDto.ts';
import { TestCaseDto } from '@/api/exercises/TestCaseDto.ts';

export type CreateMethodCodingRequest = {
  title: string;
  description: string;
  difficulty: string;
  exerciseTopics: number[];
  methodToExecute: string;
  methodSolutionCode: string;
  methodReturnTypeId: number;
  exerciseNotes: ExerciseNoteDto[];
  inputOutputExamples: InputOutputExampleDto[];
  methodParameters: MethodParameterDto[];
  testCases: TestCaseDto[];
};
