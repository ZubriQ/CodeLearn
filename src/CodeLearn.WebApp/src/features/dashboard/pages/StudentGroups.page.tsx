import { useEffect } from 'react';
import { useDashboardPageTitle } from '@/components/layout';
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from '@/components/ui/table.tsx';
import { StudentGroup } from '@/features/dashboard/models/StudentGroup.ts';

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
    enrolmentYear: 2019,
    name: 'IF-30',
  },
];

function StudentGroupsPage() {
  const [, setCurrentPageTitle] = useDashboardPageTitle();

  useEffect(() => {
    setCurrentPageTitle('Student Groups');
  }, []);

  return (
    <Table>
      <TableHeader>
        <TableRow>
          <TableHead className="w-32">Enrolment</TableHead>
          <TableHead>Name</TableHead>
        </TableRow>
      </TableHeader>
      <TableBody>
        {studentGroups.map((group) => {
          return (
            <TableRow>
              <TableCell className="font-medium">{group.enrolmentYear}</TableCell>
              <TableCell>{group.name}</TableCell>
            </TableRow>
          );
        })}
      </TableBody>
    </Table>
  );
}

export default StudentGroupsPage;
