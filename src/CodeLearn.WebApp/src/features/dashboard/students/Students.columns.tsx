import { ColumnDef } from '@tanstack/react-table';
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
import { Student } from '@/features/dashboard/students/Student.ts';

export const columns: ColumnDef<Student>[] = [
  {
    accessorKey: 'fullName',
    header: 'Full name',
    cell: ({ row }) => {
      const student = row.original;

      return <p className="font-medium text-zinc-700">{student.fullName}</p>;
    },
  },
  {
    accessorKey: 'studentGroupName',
    header: 'Group',
    cell: ({ row }) => {
      const student = row.original;

      return <p className="font-medium text-zinc-700">{student.studentGroupName}</p>;
    },
  },
  {
    accessorKey: 'userCode',
    header: 'Code',
    cell: ({ row }) => {
      const student = row.original;

      return <p className="font-medium text-zinc-700">{student.userCode}</p>;
    },
  },
  {
    accessorKey: 'userName',
    header: 'Username',
    cell: ({ row }) => {
      const student = row.original;

      return <p className="font-medium text-zinc-700">{student.userName}</p>;
    },
  },
  {
    accessorKey: 'temporaryPassword',
    header: 'Temporary password',
    cell: ({ row }) => {
      const student = row.original;

      return <p className="font-medium text-zinc-700">{student.temporaryPassword}</p>;
    },
  },
  {
    id: 'actions',
    cell: ({ row }) => {
      const student = row.original;

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
              <DropdownMenuItem onClick={() => navigator.clipboard.writeText(student.fullName)}>
                Copy full name
              </DropdownMenuItem>
              <DropdownMenuItem
                onClick={() =>
                  navigator.clipboard.writeText(
                    `Username: ${student.userName} Temporary password: ${student.temporaryPassword}`,
                  )
                }
              >
                Copy credentials
              </DropdownMenuItem>
              <DropdownMenuSeparator />
              <DropdownMenuItem>
                <Link className="h-full w-full" key={student.id} to={`/dashboard/students/${student.id}`}>
                  Edit
                </Link>
              </DropdownMenuItem>
              <DropdownMenuItem
                onClick={() => {
                  agent.Students.delete(student.id)
                    .then(() => {
                      location.reload();
                    })
                    .catch((error) => {
                      toast({
                        title: 'Error deleting a student',
                        description: error.message || 'An unexpected error occurred.',
                        variant: 'destructive',
                      });
                    });
                }}
              >
                Delete
              </DropdownMenuItem>
              <DropdownMenuItem>Reset password</DropdownMenuItem>
            </DropdownMenuContent>
          </DropdownMenu>
        </div>
      );
    },
  },
];
