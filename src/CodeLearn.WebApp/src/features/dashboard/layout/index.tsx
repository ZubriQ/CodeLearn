import { SideMenuLinkGroup } from '@/components/layout/models/SideMenuLinkGroup.ts';
import Layout from '@/components/layout';
import { ArrowLeftStartOnRectangleIcon } from '@heroicons/react/24/outline';

const sections: SideMenuLinkGroup[] = [
  {
    id: 1,
    title: 'Test Builder',
    links: [
      { name: 'Tests', href: '/dashboard/tests', id: 1 },
      { name: 'Exercise Topics', href: '/dashboard/exercise-topics', id: 2 },
    ],
  },
  {
    id: 2,
    title: 'Testing Sessions',
    links: [
      { name: 'All Sessions', href: '/dashboard/testing-sessions', id: 1 },
      { name: 'Recent Sessions', href: '/dashboard/recent-testing-sessions', id: 2 },
    ],
  },
  {
    id: 3,
    title: 'User Management',
    links: [
      { name: 'Student Groups', href: '/dashboard/student-groups', id: 1 },
      { name: 'Students', href: '/dashboard/students', id: 2 },
      { name: 'Teachers ', href: '/dashboard/teachers', id: 3 },
    ],
  },

  {
    id: 4,
    links: [{ name: 'Logout', href: '/sign-out', id: 5, icon: <ArrowLeftStartOnRectangleIcon /> }],
  },
];

function DashboardLayout() {
  return <Layout sections={sections} sectionsTitle="Dashboard" />;
}

export default DashboardLayout;
