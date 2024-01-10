import { Outlet } from 'react-router-dom';
import DashboardTitle from '@/features/teachers/layout/components/DashboardTitle.tsx';
import SideMenu from '@/features/teachers/layout/components/SideMenu.tsx';

const headerStyle = {
  height: '100vh',
  maxHeight: '100vh',
};

function TeacherDashboardLayout() {
  return (
    <div className="flex h-full">
      <div
        className="bg-background hide-scrollbar border-default h-full w-64 overflow-auto border-r"
        style={headerStyle}
      >
        <DashboardTitle />
        <SideMenu />
      </div>
      <div className="flex flex-1 flex-col">
        <Outlet />
      </div>
    </div>
  );
}

export default TeacherDashboardLayout;
