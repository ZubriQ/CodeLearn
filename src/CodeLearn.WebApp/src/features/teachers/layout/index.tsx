import { Outlet } from 'react-router-dom';
import SideMenuTitle from '@/features/teachers/layout/components/SideMenuTitle.tsx';
import SideMenu from '@/features/teachers/layout/components/SideMenu.tsx';
import DashboardHeader from '@/components/dashboard-header';
import { SideMenuLinkGroup } from '@/features/teachers/layout/models/SideMenuLinkGroup.ts';

const headerStyle = {
  height: '100vh',
  maxHeight: '100vh',
};

const sections: SideMenuLinkGroup[] = [
  {
    title: 'Pages',
    links: [
      { name: 'Tests', href: '/teacher-dashboard/tests' },
      { name: 'Student groups', href: '/teacher-dashboard/student-groups' },
      { name: 'Students', href: '/teacher-dashboard/students' },
      { name: 'Testing results', href: '/teacher-dashboard/testing-results' },
    ],
  },
  {
    links: [{ name: 'Logout', href: '/sign-out' }],
  },
];

function TeacherDashboardLayout() {
  return (
    <div className="flex h-full">
      <main
        className="bg-background hide-scrollbar border-default h-full w-64 overflow-auto border-r"
        style={headerStyle}
      >
        <SideMenuTitle />
        <SideMenu sections={sections} />
      </main>
      <div className="flex flex-1 flex-col">
        <DashboardHeader />
        <div className="flex-1 flex-grow overflow-auto">
          <div className="px-5 py-4">
            <Outlet />
          </div>
        </div>
      </div>
    </div>
  );
}

export default TeacherDashboardLayout;
