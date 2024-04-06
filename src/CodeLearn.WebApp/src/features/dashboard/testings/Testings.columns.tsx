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
import { Testing } from '@/features/dashboard/testings/Testing.ts';

export const columns: ColumnDef<Testing>[] = [
  {
    accessorKey: 'testTitle',
    header: 'Test',
    cell: ({ row }) => {
      const testing = row.original;

      return <p className="truncate text-zinc-800">{testing.testTitle}</p>;
    },
  },
  {
    accessorKey: 'studentGroupName',
    header: 'Group',
    cell: ({ row }) => {
      const testing = row.original;

      return <p className="text-zinc-800">{testing.studentGroupName}</p>;
    },
  },
  {
    accessorKey: 'deadlineDate',
    header: 'Deadline',
    cell: ({ row }) => {
      const testing = row.original;

      const date = new Date(testing.deadlineDate);
      const formattedDateTime = new Intl.DateTimeFormat('ru-RU', {
        year: 'numeric',
        month: 'numeric',
        day: '2-digit',
        hour12: false,
      }).format(date);

      return <p className="text-zinc-800">{formattedDateTime}</p>;
    },
  },
  {
    id: 'actions',
    cell: ({ row }) => {
      const testing = row.original;

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
              <DropdownMenuSeparator />
              <DropdownMenuItem
                onClick={() => {
                  agent.Testings.delete(testing.id)
                    .then(() => {
                      location.reload();
                    })
                    .catch((error) => {
                      toast({
                        title: 'Error deleting a testing',
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
