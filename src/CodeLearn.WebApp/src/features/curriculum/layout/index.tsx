import { SideMenuLinkGroup } from '@/components/layout/models/SideMenuLinkGroup.ts';
import Layout from '@/components/layout';
import { ArrowLeftStartOnRectangleIcon } from '@heroicons/react/24/outline';

const sections: SideMenuLinkGroup[] = [
  {
    id: 1,
    title: 'Testings',
    links: [
      { name: 'Testing list', href: '/curriculum/testings', id: 1 },
      { name: 'Testing sessions', href: '/curriculum/testing-sessions', id: 2 },
    ],
  },
  {
    id: 2,
    links: [{ name: 'Sign out', href: '/sign-out', id: 3, icon: <ArrowLeftStartOnRectangleIcon /> }],
  },
];

function CurriculumLayout() {
  return <Layout sections={sections} sectionsTitle="Curriculum" />;
}

export default CurriculumLayout;
