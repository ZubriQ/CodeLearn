import { Navigate, type RouteObject } from 'react-router-dom';
import RequireRole from '@/components/require-role';
import { ROLES } from '@/constants/roles.ts';

// For Landing & etc.
import HomePage from '@/features/home/pages/Home.page.tsx';
import NotFoundPage from '@/features/errors/pages/404.page.tsx';
import SignInPage from '@/features/users/pages/SignIn.page.tsx';
import SignOutPage from '@/features/users/pages/SignOut.page.tsx';

// For Teachers & Administrator
import DashboardLayout from '@/features/dashboard/DashboardLayout.tsx';
import TeacherTestsPage from '@/features/dashboard/tests/pages/Tests.page.tsx';
import AddMethodCodingExercisePage from '@/features/dashboard/tests/pages/AddMethodCodingExercise.page.tsx';
import AddQuestionExercisePage from '@/features/dashboard/tests/pages/AddQuestionExercise.page.tsx';
import SingleTestPage from '@/features/dashboard/tests/pages/SingleTest.page.tsx';
import StudentGroupsPage from '@/features/dashboard/student-groups/pages/StudentGroups.page.tsx';
import EditStudentGroupPage from '@/features/dashboard/student-groups/pages/EditStudentGroup.page.tsx';
import StudentsPage from '@/features/dashboard/students/pages/Students.page.tsx';
import TeachersPage from '@/features/dashboard/teachers/pages/Teachers.page.tsx';
import TestingSessionsPage from '@/features/dashboard/testing-sessions/pages/TestingSessions.page.tsx';
import TestingsPage from '@/features/dashboard/testings/pages/Testings.page.tsx';

// For Students
import CurriculumLayout from '@/features/curriculum/layout';
import StudentTestingsPage from '@/features/curriculum/pages/Testings.page.tsx';
import CurriculumTestingSessionsPage from '@/features/curriculum/pages/TestingSessions.page.tsx';

// Students are tested on this page
import TestingSessionPage from '@/features/testing-session/pages/TestingSession.page.tsx';

export const routes: RouteObject[] = [
  {
    index: true,
    element: <HomePage />,
  },
  {
    path: '*',
    element: <NotFoundPage />,
  },
  {
    path: 'sign-in',
    element: <SignInPage />,
  },
  {
    path: 'sign-out',
    element: <SignOutPage />,
  },
  {
    path: 'dashboard',
    element: (
      <RequireRole allowedRoles={[ROLES.TEACHER, ROLES.ADMIN]}>
        <DashboardLayout />
      </RequireRole>
    ),
    children: [
      {
        index: true,
        element: <Navigate to="tests" />,
      },
      {
        path: 'tests',
        element: <TeacherTestsPage />,
      },
      {
        path: 'tests/:id',
        element: <SingleTestPage />,
      },
      {
        path: 'tests/:id/add-method-coding-exercise',
        element: <AddMethodCodingExercisePage />,
      },
      {
        path: 'tests/:id/add-question-exercise',
        element: <AddQuestionExercisePage />,
      },
      {
        path: 'testings',
        element: <TestingsPage />,
      },
      {
        path: 'student-groups',
        element: <StudentGroupsPage />,
      },
      {
        path: 'student-groups/:id',
        element: <EditStudentGroupPage />,
      },
      {
        path: 'students',
        element: <StudentsPage />,
      },
      {
        path: 'teachers',
        element: <TeachersPage />,
      },
      {
        path: 'testing-results',
        element: <TestingSessionsPage />,
      },
    ],
  },
  {
    path: 'curriculum',
    element: (
      <RequireRole allowedRoles={[ROLES.STUDENT]}>
        <CurriculumLayout />
      </RequireRole>
    ),
    children: [
      {
        index: true,
        element: <Navigate to="testings" />,
      },
      {
        path: 'testings',
        element: <StudentTestingsPage />,
      },
      {
        path: 'testing-sessions',
        element: <CurriculumTestingSessionsPage />,
      },
    ],
  },
  {
    path: 'testing-session/:id',
    element: <TestingSessionPage />,
  },
];

export default routes;
