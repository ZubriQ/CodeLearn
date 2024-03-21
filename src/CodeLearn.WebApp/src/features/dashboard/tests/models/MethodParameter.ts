import { DataType } from '@/features/dashboard/tests/models/DataType.ts';

export type MethodParameter = {
  id: number;
  exerciseId: number;
  dataType: DataType;
  position: number;
};
