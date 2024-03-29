import { TestCaseParameterDto } from '@/api/exercises/TestCaseParameterDto.ts';

export type TestCaseDto = {
  correctOutputValue: string;
  testCaseParameters: TestCaseParameterDto[];
};
