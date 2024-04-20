export type TestingSession = {
  id: number;
  testingId: number;
  status: string;
  startDateTime: Date;
  finishDateTime: Date;
  durationInMinutes: number;
  score: number;
};
