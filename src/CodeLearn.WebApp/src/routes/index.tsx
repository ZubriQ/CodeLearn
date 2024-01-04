import { lazy, Suspense } from 'react';
import { Navigate, type RouteObject } from 'react-router-dom';
import TestsPage from '../pages/tests';
import StudentGroupsPage from '../pages/student-groups';

const HomePage = lazy(() => import('@/pages/home'));
const TeacherDashboardLayout = lazy(() => import('@/layouts/teacher-dashboard'));
const TestingSessionPage = lazy(() => import('@/pages/testing-session'));
const NotFoundPage = lazy(() => import('@/pages/not-found'));
const SignInPage = lazy(() => import('@/pages/sign-in'));
const SignUpPage = lazy(() => import('@/pages/sign-up'));

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
        element: <TestsPage />,
      },
      {
        path: 'student-groups',
        element: <StudentGroupsPage />,
      },
    ],
  },
  {
    path: 'testing-session',
    element: (
      <Suspense>
        <TestingSessionPage />
      </Suspense>
    ),
  },
];

export default routes;
