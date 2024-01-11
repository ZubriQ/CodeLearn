import { SideMenuLinkGroup } from '@/components/dashboard-layout/SideMenuLinkGroup.ts';
import DashboardLayout from '@/components/dashboard-layout';
import { ArrowLeftStartOnRectangleIcon } from '@heroicons/react/24/outline';

const sections: SideMenuLinkGroup[] = [
  {
    id: 1,
    title: 'Pages',
    links: [
      { name: 'Tests', href: '/teacher-dashboard/tests', id: 1 },
      { name: 'Student groups', href: '/teacher-dashboard/student-groups', id: 2 },
      { name: 'Students', href: '/teacher-dashboard/students', id: 3 },
      { name: 'Testing results', href: '/teacher-dashboard/testing-results', id: 4 },
    ],
  },
  {
    id: 2,
    links: [{ name: 'Logout', href: '/sign-out', id: 5, icon: <ArrowLeftStartOnRectangleIcon /> }],
  },
];

function TeacherDashboardLayout() {
  return <DashboardLayout sections={sections} />;
}

export default TeacherDashboardLayout;
