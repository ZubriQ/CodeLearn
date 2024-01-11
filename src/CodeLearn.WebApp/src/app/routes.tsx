import { lazy, Suspense } from 'react';
import { Navigate, type RouteObject } from 'react-router-dom';

// Landing
const HomePage = lazy(() => import('@/features/home/pages/Home.page.tsx'));
const NotFoundPage = lazy(() => import('@/features/errors/pages/404.page.tsx'));
const SignInPage = lazy(() => import('@/features/users/pages/SignIn.page.tsx'));
const SignUpPage = lazy(() => import('@/features/users/pages/SignUp.page.tsx'));
const TestingSessionPage = lazy(() => import('@/features/testing-sessions/pages/TestingSession.page.tsx'));

// Teacher
const TeacherDashboardLayout = lazy(() => import('@/features/teachers/layout/'));
const TeacherTestsPage = lazy(() => import('@/features/teachers/pages/Tests.page.tsx'));
const StudentGroupsPage = lazy(() => import('@/features/teachers/pages/StudentGroups.page.tsx'));
const StudentsPage = lazy(() => import('@/features/teachers/pages/Students.page.tsx'));
const TestingResultsPage = lazy(() => import('@/features/teachers/pages/TestingResults.page.tsx'));

// Student
const StudentDashboardLayout = lazy(() => import('@/features/students/layout/'));
const StudentTestsPage = lazy(() => import('@/features/students/pages/Tests.page.tsx'));

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
    path: 'teacher-dashboard',
    element: (
      <Suspense>
        <TeacherDashboardLayout />
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
        path: 'student-groups',
        element: (
          <Suspense>
            <StudentGroupsPage />
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
            <TestingResultsPage />
          </Suspense>
        ),
      },
    ],
  },
  {
    path: 'student-dashboard',
    element: (
      <Suspense>
        <StudentDashboardLayout />
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
