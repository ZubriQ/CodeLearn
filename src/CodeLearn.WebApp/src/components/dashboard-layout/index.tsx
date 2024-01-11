import { Outlet } from 'react-router-dom';
import SideMenuTitle from '@/components/dashboard-layout/SideMenuTitle.tsx';
import SideMenu from '@/components/dashboard-layout/SideMenu.tsx';
import { SideMenuLinkGroup } from '@/components/dashboard-layout/SideMenuLinkGroup.ts';

const headerStyle = {
  height: '100vh',
  maxHeight: '100vh',
};

type DashboardLayoutProps = {
  sections: SideMenuLinkGroup[];
};

function DashboardLayout(props: DashboardLayoutProps) {
  return (
    <div className="flex h-full bg-gray-50">
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
