import SideMenuTitle from '@/components/dashboard-layout/SideMenuTitle.tsx';
import SideMenu from '@/components/dashboard-layout/SideMenu.tsx';
import { Outlet } from 'react-router-dom';
import { SideMenuLinkGroup } from '@/components/dashboard-layout/SideMenuLinkGroup.ts';

const headerStyle = {
  height: '100vh',
  maxHeight: '100vh',
};

interface DashboardLayoutProps {
  sections: Array<SideMenuLinkGroup>;
}

function DashboardLayout(props: DashboardLayoutProps) {
  return (
    <div className="flex h-full">
      <main
        className="bg-background hide-scrollbar border-default h-full w-64 overflow-auto border-r"
        style={headerStyle}
      >
        <SideMenuTitle />
        <SideMenu sections={props.sections} />
      </main>
      <div className="flex flex-1 flex-col">
        <Outlet />
      </div>
    </div>
  );
}

export default DashboardLayout;
