import { SideMenuLinkGroup } from '@/components/layout/models/SideMenuLinkGroup.ts';
import Layout from '@/components/layout';
import { ArrowLeftStartOnRectangleIcon } from '@heroicons/react/24/outline';

const sections: SideMenuLinkGroup[] = [
  {
    id: 1,
    title: 'Testing',
    links: [
      { name: 'Available Tests', href: '/curriculum/tests', id: 1 },
      { name: 'Testing Sessions', href: '/curriculum/testing-sessions', id: 2 },
    ],
  },
  {
    id: 2,
    links: [{ name: 'Logout', href: '/sign-out', id: 5, icon: <ArrowLeftStartOnRectangleIcon /> }],
  },
];

function CurriculumLayout() {
  return <Layout sections={sections} sectionsTitle="Curriculum" />;
}

export default CurriculumLayout;
