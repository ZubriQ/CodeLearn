import { lazy, Suspense } from 'react';
import { Navigate, type RouteObject } from 'react-router-dom';

// For Landing & etc.
const HomePage = lazy(() => import('@/features/home/pages/Home.page.tsx'));
const NotFoundPage = lazy(() => import('@/features/errors/pages/404.page.tsx'));
const SignInPage = lazy(() => import('@/features/users/pages/SignIn.page.tsx'));
const SignUpPage = lazy(() => import('@/features/users/pages/SignUp.page.tsx'));
// const TestingSessionPage = lazy(() => import('@/features/testing-sessions/pages/TestingSession.page.tsx'));

// For Teachers & Administrator
const DashboardLayout = lazy(() => import('@/features/dashboard/DashboardLayout.tsx'));
const TeacherTestsPage = lazy(() => import('@/features/dashboard/tests/pages/Tests.page.tsx'));
const AddMethodCodingExercisePage = lazy(
  () => import('@/features/dashboard/tests/pages/AddMethodCodingExercise.page.tsx'),
);
const AddQuestionExercisePage = lazy(() => import('@/features/dashboard/tests/pages/AddQuestionExercise.page.tsx'));
const SingleTestPage = lazy(() => import('@/features/dashboard/tests/pages/SingleTest.page.tsx'));
const StudentGroupsPage = lazy(() => import('@/features/dashboard/student-groups/pages/StudentGroups.page.tsx'));
const EditStudentGroupPage = lazy(() => import('@/features/dashboard/student-groups/pages/EditStudentGroup.page.tsx'));
const StudentsPage = lazy(() => import('@/features/dashboard/students/pages/Students.page.tsx'));
const TestingSessionsPage = lazy(() => import('@/features/dashboard/testing-sessions/pages/TestingSessions.page.tsx'));
const TestingsPage = lazy(() => import('@/features/dashboard/testings/pages/Testings.page.tsx'));

// For Students
const CurriculumLayout = lazy(() => import('@/features/curriculum/layout'));
const StudentTestsPage = lazy(() => import('@/features/curriculum/pages/Tests.page.tsx'));

// Students are tested on this page
const TestingSessionPage = lazy(() => import('@/features/testing-session/pages/TestingSession.page.tsx'));

export const routes: RouteObject[] = [
  {
    index: true,
    element: (
      <Suspense>
        <HomePage />
      </Suspense>
    ),
  },
  {
    path: '*',
    element: (
      <Suspense>
        <NotFoundPage />
      </Suspense>
    ),
  },
  {
    path: 'sign-in',
    element: (
      <Suspense>
        <SignInPage />
      </Suspense>
    ),
  },
  {
    path: 'sign-up',
    element: (
      <Suspense>
        <SignUpPage />
      </Suspense>
    ),
  },
  {
    path: 'dashboard',
    element: (
      <Suspense>
        <DashboardLayout />
      </Suspense>
    ),
    children: [
      {
        index: true,
        element: <Navigate to="tests" />,
      },
      {
        path: 'tests',
        element: (
          <Suspense>
            <TeacherTestsPage />
          </Suspense>
        ),
      },
      {
        path: 'tests/:id',
        element: (
          <Suspense>
            <SingleTestPage />
          </Suspense>
        ),
      },
      {
        path: 'tests/:id/add-method-coding-exercise',
        element: (
          <Suspense>
            <AddMethodCodingExercisePage />
          </Suspense>
        ),
      },
      {
        path: 'tests/:id/add-question-exercise',
        element: (
          <Suspense>
            <AddQuestionExercisePage />
          </Suspense>
        ),
      },
      {
        path: 'testings',
        element: (
          <Suspense>
            <TestingsPage />
          </Suspense>
        ),
      },
      {
        path: 'student-groups',
        element: (
          <Suspense>
            <StudentGroupsPage />
          </Suspense>
        ),
      },
      {
        path: 'student-groups/:id',
        element: (
          <Suspense>
            <EditStudentGroupPage />
          </Suspense>
        ),
      },
      {
        path: 'students',
        element: (
          <Suspense>
            <StudentsPage />
          </Suspense>
        ),
      },
      {
        path: 'testing-results',
        element: (
          <Suspense>
            <TestingSessionsPage />
          </Suspense>
        ),
      },
    ],
  },
  {
    path: 'curriculum',
    element: (
      <Suspense>
        <CurriculumLayout />
      </Suspense>
    ),
    children: [
      {
        index: true,
        element: <Navigate to="tests" />,
      },
      {
        path: 'tests',
        element: (
          <Suspense>
            <StudentTestsPage />
          </Suspense>
        ),
      },
    ],
  },
  {
    path: 'testing-session/:id',
    element: (
      <Suspense>
        <TestingSessionPage />
      </Suspense>
    ),
  },
];

export default routes;
