import { ColumnDef } from '@tanstack/react-table';
import { StudentGroup } from '@/features/dashboard/models/StudentGroup.ts';
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

export const columns: ColumnDef<StudentGroup>[] = [
  {
    accessorKey: 'name',
    header: 'Name',
    cell: ({ row }) => {
      const studentGroup = row.original;

      return <p className="text-zinc-700">{studentGroup.name}</p>;
    },
  },
  {
    accessorKey: 'enrolmentYear',
    header: 'Enrolment',
    cell: ({ row }) => {
      const studentGroup = row.original;

      return <p className="font-medium text-zinc-800">{studentGroup.enrolmentYear}</p>;
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
