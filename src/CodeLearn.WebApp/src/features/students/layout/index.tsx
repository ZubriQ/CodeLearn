import { SideMenuLinkGroup } from '@/components/dashboard-layout/models/SideMenuLinkGroup.ts';
import DashboardLayout from '@/components/dashboard-layout';

const sections: SideMenuLinkGroup[] = [
  {
    id: 1,
    title: 'Pages',
    links: [
      { name: 'Tests', href: '/student-dashboard/tests', id: 1 },
      { name: 'Testing results', href: '/student-dashboard/testing-results', id: 2 },
    ],
  },
  {
    id: 2,
    links: [{ name: 'Logout', href: '/sign-out', id: 5 }],
  },
];

function StudentDashboardLayout() {
  return <DashboardLayout sections={sections} />;
}

export default StudentDashboardLayout;
