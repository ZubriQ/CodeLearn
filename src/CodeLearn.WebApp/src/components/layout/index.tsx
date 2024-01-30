import { useEffect, useState } from 'react';
import { Outlet, useOutletContext } from 'react-router-dom';
import SideMenuTitle from '@/components/layout/SideMenuTitle.tsx';
import SideMenu from '@/components/layout/SideMenu.tsx';
import { SideMenuLinkGroup } from '@/components/layout/models/SideMenuLinkGroup.ts';
import DashboardHeader from '@/components/layout-header';
import { NavigationTitle } from '@/components/layout/models/NavigationTitle.ts';

const headerStyle = {
  height: '100vh',
  maxHeight: '100vh',
};

type LayoutProps = {
  sections: SideMenuLinkGroup[];
  sectionsTitle: string;
};

function Layout(props: LayoutProps) {
  const [currentPageTitle, setCurrentPageTitle] = useState<NavigationTitle>('Tests');

  useEffect(() => {
    document.body.className += 'overflow-hidden';

    return () => {
      document.body.className = document.body.className.replace('overflow-hidden', '');
    };
  }, []);

  return (
    <div className="flex h-full bg-gray-50">
      <div
        className="bg-background hide-scrollbar border-default h-full w-64 overflow-auto border-r"
        style={headerStyle}
      >
        <SideMenuTitle name={props.sectionsTitle} />
        <SideMenu sections={props.sections} />
      </div>

      <div className="flex min-w-min flex-1 flex-col">
        <DashboardHeader title={currentPageTitle} />

        <div className="hide-main-scrollbar flex-1 flex-grow overflow-y-auto px-5 py-4">
          <div className="my-2">
            <Outlet context={[currentPageTitle, setCurrentPageTitle]} />
          </div>
        </div>
      </div>
    </div>
  );
}

export default Layout;

export function useDashboardPageTitle() {
  return useOutletContext<[NavigationTitle, React.Dispatch<React.SetStateAction<NavigationTitle>>]>();
}
