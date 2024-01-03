import { lazy, Suspense } from 'react';
import { Navigate, type RouteObject } from 'react-router-dom';
import TestsPage from './pages/tests';
import StudentGroupsPage from './pages/student-groups';

const RootLayout = lazy(() => import('@/layouts/root'));
const DashboardLayout = lazy(() => import('@/layouts/dashboard'));
const TestingPage = lazy(() => import('@/pages/testing'));
const Notfound = lazy(() => import('@/pages/not-found'));
const SignInPage = lazy(() => import('@/pages/sign-in'));
const SignUpPage = lazy(() => import('@/pages/sign-up'));

export const routes: Array<RouteObject> = [
  {
    index: true,
    element: (
      <Suspense>
        <RootLayout />
      </Suspense>
    ),
  },
  {
    path: '*',
    element: (
      <Suspense>
        <Notfound />
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
        element: <TestsPage />,
      },
      {
        path: 'student-groups',
        element: <StudentGroupsPage />,
      },
    ],
  },
  {
    path: 'testing',
    element: (
      <Suspense>
        <TestingPage />
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
];

export default routes;
