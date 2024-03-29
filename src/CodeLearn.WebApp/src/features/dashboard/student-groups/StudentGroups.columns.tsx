import { ColumnDef } from '@tanstack/react-table';
import { StudentGroup } from '@/features/dashboard/student-groups/StudentGroup.ts';
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
import agent from '@/api/agent.ts';
import { toast } from '@/components/ui/use-toast.ts';
import { Link } from 'react-router-dom';

export const columns: ColumnDef<StudentGroup>[] = [
  {
    accessorKey: 'name',
    header: 'Name',
    cell: ({ row }) => {
      const studentGroup = row.original;

      return <p className="font-medium text-zinc-700">{studentGroup.name}</p>;
    },
  },
  {
    accessorKey: 'enrolmentYear',
    header: 'Enrolment',
    cell: ({ row }) => {
      const studentGroup = row.original;

      return <p className="text-zinc-800">{studentGroup.enrolmentYear}</p>;
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
              <DropdownMenuItem>
                <Link
                  className="h-full w-full"
                  key={studentGroup.id}
                  to={`/dashboard/student-groups/${studentGroup.id}`}
                >
                  Edit
                </Link>
              </DropdownMenuItem>
              <DropdownMenuItem
                onClick={() => {
                  agent.StudentGroup.delete(studentGroup.id)
                    .then(() => {
                      location.reload();
                    })
                    .catch((error) => {
                      toast({
                        title: 'Error deleting a student group',
                        description: error.message || 'An unexpected error occurred.',
                        variant: 'destructive',
                      });
                    });
                }}
              >
                Delete
              </DropdownMenuItem>
            </DropdownMenuContent>
          </DropdownMenu>
        </div>
      );
    },
  },
];
