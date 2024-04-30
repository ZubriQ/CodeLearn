export type CreateTestingRequest = {
  testId: number;
  studentGroupId: number;
  deadlineDate: Date;
  durationInMinutes: number;
  status: string;
};
