import axios, { AxiosResponse } from 'axios';
import { store } from '@/app/store.ts';
import { LoginRequest } from '@/api/users/LoginRequest.ts';
import { CreateMethodCodingRequest } from '@/api/exercises/CreateMethodCodingRequest.ts';
import { CreateQuestionRequest } from '@/api/exercises/CreateQuestionRequest.ts';
import { CreateTestingRequest } from '@/api/testings/CreateTestingRequest.ts';
import { RegisterStudentRequest } from '@/api/users/RegisterStudentRequest.ts';
import { LoginResponse } from '@/api/users/LoginResponse.ts';
import { CreateMethodCodingExerciseSubmissionRequest } from '@/api/exercise-submissions/CreateMethodCodingExerciseSubmissionRequest.ts';
import { CreateQuestionExerciseSubmissionRequest } from '@/api/exercise-submissions/CreateQuestionExerciseSubmissionsRequest.ts';
import { jwtDecode } from 'jwt-decode';
import { loginSuccess, logout } from '@/features/users/auth-slice.ts';
import { RegisterTeacherRequest } from '@/api/users/RegisterTeacherRequest.ts';

axios.defaults.baseURL = 'http://localhost:5000/api/';

axios.interceptors.request.use(
  (config) => {
    const token = store.getState().auth.token;
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  },
);

// Check for token expiry and automatically refresh the token
axios.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalRequest = error.config;
    if (error.response && error.response.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;

      const authToken = store.getState().auth.token;
      if (isTokenExpired(authToken)) {
        try {
          await refreshToken();
          // Update the original request with the new token
          originalRequest.headers.Authorization = `Bearer ${store.getState().auth.token}`;
          // Retry the original request with the new token
          return axios(originalRequest);
        } catch (refreshError) {
          console.error('Logging out');
          store.dispatch(logout());
        }
      }
    }
    return Promise.reject(error);
  },
);

const isTokenExpired = (token: string): boolean => {
  const decodedToken = jwtDecode(token);
  return decodedToken.exp < Date.now() / 1000;
};

const refreshToken = async (): Promise<void> => {
  const refreshTokenValue = store.getState().auth.refreshToken;
  const token = store.getState().auth.token;
  const username = store.getState().auth.username;

  if (refreshTokenValue) {
    try {
      const response = await requests.post('users/refresh-token', { jwtToken: token, refreshToken: refreshTokenValue });
      const { jwtToken, refreshToken: newRefreshToken } = response;

      store.dispatch(loginSuccess({ jwtToken, refreshToken: newRefreshToken, username }));
    } catch (error) {
      // Handle refresh token failure
      console.error('Failed to refresh token:', error);
      store.dispatch(logout());
    }
  } else {
    // No refresh token available
    store.dispatch(logout());
  }
};

const responseBody = (response: AxiosResponse) => response.data;

const requests = {
  get: (url: string) => axios.get(url).then(responseBody),
  post: (url: string, body: {}) => axios.post(url, body).then(responseBody),
  put: (url: string, body: {}) => axios.put(url, body).then(responseBody),
  delete: (url: string) => axios.delete(url).then(responseBody),
};

const Auth = {
  login: (credentials: LoginRequest): Promise<LoginResponse> => {
    return requests.post('users/login', credentials);
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
  getByIdWithExerciseIds: (id: number) => requests.get(`tests/${id}/with-exercise-ids`),
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
  getMethodCodingById: (id: number) => requests.get(`exercises/method-coding/${id}`),
  getQuestionById: (id: number) => requests.get(`exercises/question/${id}`),
};

const ExerciseTopics = {
  list: () => requests.get('exercise-topics').then((response) => response.exerciseTopics),
};

const DataTypes = {
  list: () => requests.get('data-types').then((response) => response.dataTypes),
};

const Testings = {
  getById: (id: number) => requests.get(`testings/${id}`),
  list: () => requests.get('testings').then((response) => response.testings),
  listForStudent: () => requests.get('testings/for-student').then((response) => response.testings),
  create: (request: CreateTestingRequest) => requests.post(`testings`, request),
  delete: (id: number) => requests.delete(`testings/${id}`),
};

const Students = {
  list: () => requests.get('users/students').then((response) => response.students),
  create: (request: RegisterStudentRequest) => requests.post(`users/students`, request),
  delete: (id: string) => requests.delete(`users/students/${id}`),
  importList: (request: FormData) => {
    return axios.post('users/students/import-list', request, {
      headers: { 'Content-Type': 'multipart/form-data' },
    });
  },
};

const Teachers = {
  list: () => requests.get('users/teachers').then((response) => response.teachers),
  create: (request: RegisterTeacherRequest) => requests.post(`users/teachers`, request),
  delete: (id: string) => requests.delete(`users/teachers/${id}`),
};

const TestingSessions = {
  create: (request: { testingId: number }) => requests.post(`testing-sessions`, request),
  getById: (id: number) => requests.get(`testing-sessions/${id}`),
  getCompletedExerciseIdsById: (id: number) => requests.get(`testing-sessions/${id}/completed-exercises`),
  listForCurriculum: () => requests.get('testing-sessions/my-sessions').then((response) => response.testingSessions),
  finishTestingSession: (
    id: number,
    request: {
      studentFeedback: string;
    },
  ) => requests.post(`testing-sessions/${id}/finish`, request),
};

const ExerciseSubmissions = {
  createQuestionSubmission: (testingSessionId: number, request: CreateQuestionExerciseSubmissionRequest) =>
    requests.post(`testing-sessions/${testingSessionId}/exercise-submissions/question`, request),
  createMethodCodingSubmission: (testingSessionId: number, request: CreateMethodCodingExerciseSubmissionRequest) =>
    requests.post(`testing-sessions/${testingSessionId}/exercise-submissions/method-coding`, request),
};

const agent = {
  Auth,
  StudentGroup,
  Tests,
  Exercises,
  ExerciseTopics,
  DataTypes,
  Testings,
  Students,
  Teachers,
  TestingSessions,
  ExerciseSubmissions,
};

export default agent;
