import { SideMenuLinkGroup } from '../models/SideMenuLinkGroup.ts';
import SideMenuSectionLinksList from '../components/SideMenuSectionLinksList.tsx';

interface SideMenuProps {
  sections: Array<SideMenuLinkGroup>;
}

function SideMenu(props: SideMenuProps) {
  return (
    <nav role="menu" aria-label="Sidebar" aria-orientation="vertical">
      <ul>
        {props.sections.map((section) => (
          <div className="border-default border-b px-6 py-5">
            {section.title && (
              <div className="mb-2 flex space-x-3 font-normal">
                <span className="text-sm font-medium text-gray-500">{section.title}</span>
              </div>
            )}
            <SideMenuSectionLinksList links={section.links} />
          </div>
        ))}
      </ul>
    </nav>
  );
}

export default SideMenu;
