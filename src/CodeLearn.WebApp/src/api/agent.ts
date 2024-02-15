import axios, { AxiosResponse } from 'axios';

axios.defaults.baseURL = 'http://localhost:5081/api/';

const responseBody = (response: AxiosResponse) => response.data;

const requests = {
  get: (url: string) => axios.get(url).then(responseBody),
  post: (url: string, body: {}) => axios.post(url, body).then(responseBody),
  put: (url: string, body: {}) => axios.put(url, body).then(responseBody),
  delete: (url: string) => axios.delete(url).then(responseBody),
};

const TestErrors = {
  get400Error: () => requests.get('buggy/bad-request'),
  get401Error: () => requests.get('buggy/unauthorised'),
  get404Error: () => requests.get('buggy/not-found'),
  get500Error: () => requests.get('buggy/server-error'),
  getValidationError: () => requests.get('buggy/validation-error'),
};

const StudentGroup = {
  // Using '(response) => response.studentGroups' to destructure { list[] } object.
  list: () => requests.get('student-groups').then((response) => response.studentGroups),
  details: (id: number) => requests.get(`student-groups/${id}`),
};

const agent = {
  StudentGroup,
  TestErrors,
};

export default agent;
