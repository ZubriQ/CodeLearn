import { SideMenuGroup } from '../models/SideMenuGroup.ts';
import SideMenuSectionLinksList from '../components/SideMenuSectionLinksList.tsx';

const sections: SideMenuGroup[] = [
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

function SideMenu() {
  return (
    <nav role="menu" aria-label="Sidebar" aria-orientation="vertical">
      <ul>
        {sections.map((section) => (
          <div className="border-default border-b px-6 py-5">
            {section.title && (
              <div className="mb-2 flex space-x-3 font-normal">
                <span>{section.title}</span>
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
