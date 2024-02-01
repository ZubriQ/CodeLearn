import { useEffect } from 'react';
import { useDashboardPageTitle } from '@/components/layout';
import { StudentGroup } from '@/features/dashboard/models/StudentGroup.ts';
import { ColumnDef } from '@tanstack/react-table';
import { DataTable } from '@/features/dashboard/pages/StudentGroups.data-table.tsx';
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from '@/components/ui/dropdown-menu.tsx';
import { Button } from '@/components/ui/button.tsx';
import { MoreHorizontal } from 'lucide-react';

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

export const columns: ColumnDef<StudentGroup>[] = [
  {
    accessorKey: 'enrolmentYear',
    header: 'Enrolment',
    cell: ({ row }) => {
      const studentGroup = row.original;

      return <p className="font-medium text-zinc-800">{studentGroup.enrolmentYear}</p>;
    },
  },
  {
    accessorKey: 'name',
    header: 'Name',
    cell: ({ row }) => {
      const studentGroup = row.original;

      return <p className="text-zinc-700">{studentGroup.name}</p>;
    },
  },
  {
    id: 'actions',
    cell: ({ row }) => {
      const studentGroup = row.original;

      return (
        <div className="text-right">
          <DropdownMenu>
            <DropdownMenuTrigger asChild>
              <Button variant="ghost" className="h-8 w-8 p-0">
                <span className="sr-only">Open menu</span>
                <MoreHorizontal className="h-4 w-4" />
              </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent align="end">
              <DropdownMenuLabel>Actions</DropdownMenuLabel>
              <DropdownMenuItem onClick={() => navigator.clipboard.writeText(studentGroup.name)}>
                Copy name
              </DropdownMenuItem>
              <DropdownMenuItem>Initiate testing</DropdownMenuItem>
              <DropdownMenuSeparator />
              <DropdownMenuItem>Edit</DropdownMenuItem>
              <DropdownMenuItem>Delete</DropdownMenuItem>
            </DropdownMenuContent>
          </DropdownMenu>
        </div>
      );
    },
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
