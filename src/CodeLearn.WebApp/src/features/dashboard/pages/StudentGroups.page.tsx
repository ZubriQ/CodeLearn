import { useEffect } from 'react';
import { useDashboardPageTitle } from '@/components/layout';
import { StudentGroup } from '@/features/dashboard/models/StudentGroup.ts';
import { columns } from '../columns/StudentGroups.columns.tsx';
import { DataTable } from '@/components/ui/data-table.tsx';

const studentGroups: StudentGroup[] = [
  {
    id: 1,
    enrolmentYear: 2020,
    name: 'IF-60',
  },
  {
    id: 2,
    enrolmentYear: 2021,
    name: 'IF-70',
  },
  {
    id: 3,
    enrolmentYear: 2022,
    name: 'IF-80',
  },
  {
    id: 4,
    enrolmentYear: 2023,
    name: 'IF-90',
  },
  {
    id: 5,
    enrolmentYear: 2024,
    name: 'IF-10',
  },
  {
    id: 6,
    enrolmentYear: 2018,
    name: 'IF-20',
  },
  {
    id: 7,
    enrolmentYear: 2015,
    name: 'IF-30',
  },
  {
    id: 8,
    enrolmentYear: 2014,
    name: 'IF-30',
  },
  {
    id: 9,
    enrolmentYear: 2013,
    name: 'IF-30',
  },
  {
    id: 10,
    enrolmentYear: 2012,
    name: 'IF-30',
  },
  {
    id: 11,
    enrolmentYear: 2010,
    name: 'IF-300',
  },
];

function StudentGroupsPage() {
  const [, setCurrentPageTitle] = useDashboardPageTitle();

  useEffect(() => {
    setCurrentPageTitle('Student Groups');
  }, []);

  return (
    <div className="mx-auto py-4 lg:px-11 xl:px-24">
      <DataTable columns={columns} data={studentGroups} />
    </div>
  );
}

export default StudentGroupsPage;
