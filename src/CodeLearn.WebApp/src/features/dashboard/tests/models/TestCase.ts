import { TestCaseParameter } from '@/features/dashboard/tests/models/TestCaseParameter.ts';

export type TestCase = {
  id: number;
  exerciseId: number;
  correctOutputValue: string;
  testCaseParameters: TestCaseParameter[];
};
