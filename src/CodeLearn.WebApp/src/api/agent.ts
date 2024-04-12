import axios, { AxiosResponse } from 'axios';
import { LoginCredentials } from '@/features/users/models/LoginCredentials.ts';
import { CreateMethodCodingRequest } from '@/api/exercises/CreateMethodCodingRequest.ts';
import { CreateQuestionRequest } from '@/api/exercises/CreateQuestionRequest.ts';
import { CreateTestingRequest } from '@/api/testings/CreateTestingRequest.ts';
import { RegisterStudentRequest } from '@/api/users/RegisterStudentRequest.ts';

axios.defaults.baseURL = 'https://localhost:5001/api/';

const responseBody = (response: AxiosResponse) => response.data;

const requests = {
  get: (url: string) => axios.get(url).then(responseBody),
  post: (url: string, body: {}) => axios.post(url, body).then(responseBody),
  put: (url: string, body: {}) => axios.put(url, body).then(responseBody),
  delete: (url: string) => axios.delete(url).then(responseBody),
};

const Auth = {
  login: (credentials: LoginCredentials): Promise<string> => {
    return requests.post('auth/login', credentials);
  },
};

const StudentGroup = {
  // Using '(response) => response.studentGroups' to destructure { list[] } object.
  list: () => requests.get('student-groups').then((response) => response.studentGroups),
  getById: (id: number) => requests.get(`student-groups/${id}`),
  create: (request: { name: string; enrolmentYear: number }) => requests.post(`student-groups`, request),
  update: (
    id: number,
    request: {
      name: string;
      enrolmentYear: number;
    },
  ) => requests.put(`student-groups/${id}`, request),
  delete: (id: number) => requests.delete(`student-groups/${id}`),
};

const Tests = {
  list: () => requests.get('tests').then((response) => response.tests),
  getById: (id: number) => requests.get(`tests/${id}`),
  getByIdWithExercises: (id: number) => requests.get(`tests/${id}/with-exercises`),
  create: (request: { title: string; description: string }) => requests.post(`tests`, request),
  update: (
    id: number,
    request: {
      title: string;
      description: string;
    },
  ) => requests.put(`tests/${id}`, request),
  delete: (id: number) => requests.delete(`tests/${id}`),
};

const Exercises = {
  delete: (id: number) => requests.delete(`exercises/${id}`),
  createQuestion: (testId: number, request: CreateQuestionRequest) =>
    requests.post(`tests/${testId}/question-exercises`, request),
  createMethodCoding: (testId: number, request: CreateMethodCodingRequest) =>
    requests.post(`tests/${testId}/method-coding-exercises`, request),
};

const ExerciseTopics = {
  list: () => requests.get('exercise-topics').then((response) => response.exerciseTopics),
};

const DataTypes = {
  list: () => requests.get('data-types').then((response) => response.dataTypes),
};

const Testings = {
  list: () => requests.get('testings').then((response) => response.testings),
  create: (request: CreateTestingRequest) => requests.post(`testings`, request),
  delete: (id: number) => requests.delete(`testings/${id}`),
};

const Students = {
  list: () => requests.get('users/students').then((response) => response.students),
  create: (request: RegisterStudentRequest) => requests.post(`users/students`, request),
  delete: (id: string) => requests.delete(`users/students/${id}`),
};

// const TestErrors = {
//   get400Error: () => requests.get('buggy/bad-request'),
//   get401Error: () => requests.get('buggy/unauthorised'),
//   get404Error: () => requests.get('buggy/not-found'),
//   get500Error: () => requests.get('buggy/server-error'),
//   getValidationError: () => requests.get('buggy/validation-error'),
// };

const agent = {
  Auth,
  StudentGroup,
  Tests,
  Exercises,
  ExerciseTopics,
  DataTypes,
  Testings,
  Students,
  // TestErrors,
};

export default agent;
