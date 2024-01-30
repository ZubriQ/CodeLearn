import { lazy, Suspense } from 'react';
import { Navigate, type RouteObject } from 'react-router-dom';

// For Landing & etc.
const HomePage = lazy(() => import('@/features/home/pages/Home.page.tsx'));
const NotFoundPage = lazy(() => import('@/features/errors/pages/404.page.tsx'));
const SignInPage = lazy(() => import('@/features/users/pages/SignIn.page.tsx'));
const SignUpPage = lazy(() => import('@/features/users/pages/SignUp.page.tsx'));
const TestingSessionPage = lazy(() => import('@/features/testing-sessions/pages/TestingSession.page.tsx'));

// For Teachers & Administrator
const DashboardLayout = lazy(() => import('@/features/dashboard/layout/'));
const TeacherTestsPage = lazy(() => import('@/features/dashboard/pages/Tests.page.tsx'));
const StudentGroupsPage = lazy(() => import('@/features/dashboard/pages/StudentGroups.page.tsx'));
const StudentsPage = lazy(() => import('@/features/dashboard/pages/Students.page.tsx'));
const TestingResultsPage = lazy(() => import('@/features/dashboard/pages/TestingResults.page.tsx'));

// For Students
const CurriculumLayout = lazy(() => import('@/features/curriculum/layout/'));
const StudentTestsPage = lazy(() => import('@/features/curriculum/pages/Tests.page.tsx'));

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
