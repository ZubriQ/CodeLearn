import { useState } from 'react';
import { Outlet, useOutletContext } from 'react-router-dom';
import SideMenuTitle from '@/components/dashboard-layout/SideMenuTitle.tsx';
import SideMenu from '@/components/dashboard-layout/SideMenu.tsx';
import { SideMenuLinkGroup } from '@/components/dashboard-layout/models/SideMenuLinkGroup.ts';
import DashboardHeader from '@/components/dashboard-header';
import { DashboardPageTitle } from '@/components/dashboard-layout/models/DashboardPageTitle.ts';

const headerStyle = {
  height: '100vh',
  maxHeight: '100vh',
};

type DashboardLayoutProps = {
  sections: SideMenuLinkGroup[];
};

function DashboardLayout(props: DashboardLayoutProps) {
  const [currentPageTitle, setCurrentPageTitle] = useState<DashboardPageTitle>('Tests');

  return (
    <div className="flex h-full bg-gray-50">
      <div
        className="bg-background hide-scrollbar border-default h-full w-64 overflow-auto border-r"
        style={headerStyle}
      >
        <SideMenuTitle />
        <SideMenu sections={props.sections} />
      </div>

      <div className="flex min-w-min flex-1 flex-col">
        <DashboardHeader title={currentPageTitle} />

        <div className="flex-1 flex-grow overflow-y-auto px-5 py-4">
          <div className="my-2">
            <Outlet context={[currentPageTitle, setCurrentPageTitle]} />
          </div>
        </div>
      </div>
    </div>
  );
}

export default DashboardLayout;

export function useDashboardPageTitle() {
  return useOutletContext<[DashboardPageTitle, React.Dispatch<React.SetStateAction<DashboardPageTitle>>]>();
}
