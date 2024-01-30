import { useEffect } from 'react';
import { InformationCircleIcon } from '@heroicons/react/16/solid';
import { Button } from '@/components/ui/button.tsx';
import { Input } from '@/components/ui/input.tsx';
import { Label } from '@/components/ui/label.tsx';
import { Textarea } from '@/components/ui/textarea.tsx';
import { Test } from '@/features/dashboard/models/Test.ts';
import TestCards from '@/features/dashboard/components/TestCards.tsx';
import { Tooltip, TooltipContent, TooltipProvider, TooltipTrigger } from '@/components/ui/tooltip.tsx';
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from '@/components/ui/dialog.tsx';
import { useDashboardPageTitle } from '@/components/layout';

const tests: Test[] = [
  {
    id: 1,
    teacherId: 1,
    title: 'Title1',
    description:
      'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius.',
    durationInMinutes: 50,
    createdDateTime: new Date(),
    modifiedDateTime: new Date(),
  },
  {
    id: 2,
    teacherId: 1,
    title: 'Title2',
    description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius.',
    durationInMinutes: 40,
    createdDateTime: new Date(),
    modifiedDateTime: new Date(),
  },
  {
    id: 3,
    teacherId: 1,
    title: 'Title3',
    description:
      'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius.',
    durationInMinutes: 60,
    createdDateTime: new Date(),
    modifiedDateTime: new Date(),
  },
  {
    id: 4,
    teacherId: 1,
    title: 'Title1',
    description:
      'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius.',
    durationInMinutes: 50,
    createdDateTime: new Date(),
    modifiedDateTime: new Date(),
  },
  {
    id: 5,
    teacherId: 1,
    title: 'Title2',
    description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius.',
    durationInMinutes: 40,
    createdDateTime: new Date(),
    modifiedDateTime: new Date(),
  },
  {
    id: 6,
    teacherId: 1,
    title: 'Title3',
    description:
      'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius.',
    durationInMinutes: 60,
    createdDateTime: new Date(),
    modifiedDateTime: new Date(),
  },
  {
    id: 7,
    teacherId: 1,
    title: 'Title1',
    description:
      'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius.',
    durationInMinutes: 50,
    createdDateTime: new Date(),
    modifiedDateTime: new Date(),
  },
  {
    id: 8,
    teacherId: 1,
    title: 'Title2',
    description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius.',
    durationInMinutes: 40,
    createdDateTime: new Date(),
    modifiedDateTime: new Date(),
  },
  {
    id: 9,
    teacherId: 1,
    title: 'Title3',
    description:
      'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius.',
    durationInMinutes: 60,
    createdDateTime: new Date(),
    modifiedDateTime: new Date(),
  },
];

function TeacherTestsPage() {
  const [, setCurrentPageTitle] = useDashboardPageTitle();

  useEffect(() => {
    setCurrentPageTitle('Tests');
  }, []);

  return (
    <>
      <Dialog>
        <DialogTrigger asChild>
          <Button className="mb-6">Add new test</Button>
        </DialogTrigger>
        <DialogContent className="sm:max-w-[425px]">
          <DialogHeader>
            <DialogTitle>Add new test</DialogTitle>
            <DialogDescription>
              Add the necessary information about the new test here. Click save when you're done.
            </DialogDescription>
          </DialogHeader>
          <div className="grid gap-4 py-4">
            <div className="grid grid-cols-4 items-center gap-4">
              <Label htmlFor="title" className="text-right">
                Title
              </Label>
              <Input id="title" className="col-span-3" />
            </div>
            <div className="grid grid-cols-4 items-center gap-4">
              <Label htmlFor="description" className="text-right">
                Description
              </Label>
              <Textarea id="description" placeholder="Type your description here." className="col-span-3" />
            </div>
            <div className="grid grid-cols-4 items-center gap-4">
              <Label htmlFor="duration" className="text-right">
                <TooltipProvider>
                  <Tooltip>
                    <TooltipTrigger asChild>
                      <div className="flex items-center justify-end gap-1">
                        <InformationCircleIcon className="h-4 w-4" />
                        <p>Duration</p>
                      </div>
                    </TooltipTrigger>
                    <TooltipContent>
                      <p>The duration is calculated in minutes</p>
                    </TooltipContent>
                  </Tooltip>
                </TooltipProvider>
              </Label>
              <Input type="number" id="duration" value={150} className="col-span-3" min={5} max={300} step={5} />
            </div>
          </div>
          <DialogFooter>
            <Button type="submit">Save</Button>
          </DialogFooter>
        </DialogContent>
      </Dialog>

      <div className="my-4">
        <TestCards tests={tests} />
      </div>
    </>
  );
}

export default TeacherTestsPage;
