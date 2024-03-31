import { AnswerDto } from '@/api/exercises/AnswerDto.ts';

export type CreateQuestionRequest = {
  title: string;
  description: string;
  difficulty: string;
  answers: AnswerDto[];
  exerciseTopics: number[];
};
