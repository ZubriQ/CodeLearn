import { lazy, Suspense } from 'react';
import { Navigate, type RouteObject } from 'react-router-dom';

const HomePage = lazy(() => import('@/features/home/pages/Home.tsx'));
const TeacherDashboardLayout = lazy(() => import('@/features/teachers/layout/'));
const TestingSessionPage = lazy(() => import('@/features/testing-sessions/pages/TestingSession.tsx'));
const NotFoundPage = lazy(() => import('@/features/errors/pages/404.tsx'));
const SignInPage = lazy(() => import('@/features/users/pages/SignIn.tsx'));
const SignUpPage = lazy(() => import('@/features/users/pages/SignUp.tsx'));
const TestsPage = lazy(() => import('@/features/teachers/pages/Tests.tsx'));
const StudentGroupsPage = lazy(() => import('@/features/teachers/pages/StudentGroups.tsx'));

export const routes: Array<RouteObject> = [
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
            <TestsPage />
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
