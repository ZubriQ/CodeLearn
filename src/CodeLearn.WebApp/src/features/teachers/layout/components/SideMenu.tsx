type SideMenuOption = {
  name: string;
  href: string;
};

type SideMenuSection = {
  title?: string;
  links: Array<SideMenuOption>;
};

const sections: SideMenuSection[] = [
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
        {/* Section */}
        <div className="border-default border-b px-6 py-5">
          <ul className="space-y-1">
            <li>
              <a className="block" target="_self">
                <span className="border-default ring-foreground group-hover:border-foreground-muted group flex max-w-full cursor-pointer items-center space-x-2 py-1 font-normal outline-none focus-visible:z-10 focus-visible:ring-1">
                  <span className="text-foreground-light group-hover:text-foreground w-full truncate text-sm transition">
                    Link
                  </span>
                </span>
              </a>
            </li>
          </ul>
        </div>
      </ul>
    </nav>
  );
}

export default SideMenu;
