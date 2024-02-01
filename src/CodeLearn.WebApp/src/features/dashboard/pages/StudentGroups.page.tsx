import { useEffect } from 'react';
import { InformationCircleIcon } from '@heroicons/react/16/solid';
import { useDashboardPageTitle } from '@/components/layout';
import { StudentGroup } from '@/features/dashboard/models/StudentGroup.ts';
import { columns } from '../columns/StudentGroups.columns.tsx';
import { DataTable } from '@/components/ui/data-table.tsx';
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from '@/components/ui/dialog.tsx';
import { Button } from '@/components/ui/button.tsx';
import { Label } from '@/components/ui/label.tsx';
import { Input } from '@/components/ui/input.tsx';
import { Tooltip, TooltipContent, TooltipProvider, TooltipTrigger } from '@/components/ui/tooltip.tsx';

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
    <div className="mx-auto whitespace-nowrap py-4 lg:px-11 xl:px-24">
      <Dialog>
        <DialogTrigger asChild>
          <Button className="mb-6">Add new group</Button>
        </DialogTrigger>
        <DialogContent className="sm:max-w-[450px]">
          <DialogHeader>
            <DialogTitle>Add new student group</DialogTitle>
            <DialogDescription>
              Add the necessary information about the new student group here. Click save when you're done.
            </DialogDescription>
          </DialogHeader>
          <div className="grid gap-4 py-4">
            <div className="grid grid-cols-4 items-center gap-4">
              <Label htmlFor="studentGroupName" className="text-right">
                Name
              </Label>
              <Input id="studentGroupName" className="col-span-3" />
            </div>
            <div className="grid grid-cols-4 items-center gap-4">
              <Label htmlFor="enrolmentYear" className="text-right">
                <TooltipProvider>
                  <Tooltip>
                    <TooltipTrigger asChild>
                      <div className="flex items-center justify-end gap-1">
                        <InformationCircleIcon className="h-4 w-4" />
                        <p>Enrolment</p>
                      </div>
                    </TooltipTrigger>
                    <TooltipContent>
                      <p>The year the group started to exist</p>
                    </TooltipContent>
                  </Tooltip>
                </TooltipProvider>
              </Label>
              <Input type="number" id="enrolmentYear" className="col-span-3" min={2018} max={2100} step={1} />
            </div>
          </div>
          <DialogFooter>
            <Button type="submit">Save</Button>
          </DialogFooter>
        </DialogContent>
      </Dialog>

      <Button variant="outline" className="ml-4">
        Import a list
      </Button>

      <DataTable columns={columns} data={studentGroups} filterBy="name" />
    </div>
  );
}

export default StudentGroupsPage;
