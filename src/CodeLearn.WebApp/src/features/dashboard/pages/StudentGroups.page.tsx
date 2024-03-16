import { useEffect, useState } from 'react';
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
import { useToast } from '@/components/ui/use-toast.ts';
import agent from '@/api/agent.ts';
import Loading from '@/components/loading';
import DashboardPageContainer from '@/features/dashboard/components/DashboardPageContainer.tsx';

function StudentGroupsPage() {
  const { toast } = useToast();
  const [, setCurrentPageTitle] = useDashboardPageTitle();
  const [studentGroups, setStudentGroups] = useState<StudentGroup[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [studentGroupName, setStudentGroupName] = useState('');
  const [enrolmentYear, setEnrolmentYear] = useState('');
  const [isDialogOpen, setIsDialogOpen] = useState(false);

  useEffect(() => {
    setCurrentPageTitle('Student Groups');

    setIsLoading(true);
    agent.StudentGroup.list()
      .then((groups) => {
        setStudentGroups(groups);
        setIsLoading(false);
      })
      .catch((err) => {
        toast({
          title: 'Error fetching student groups',
          description: err.message || 'An unexpected error occurred.',
          variant: 'destructive',
        });
        setIsLoading(false);
      });
  }, []);

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>, setter: (value: string) => void) => {
    setter(e.target.value);
  };

  const handleAdd = () => {
    const newStudentGroup = {
      name: studentGroupName,
      enrolmentYear: parseInt(enrolmentYear, 10),
    };

    agent.StudentGroup.create(newStudentGroup)
      .then(() => {
        setEnrolmentYear('');
        setStudentGroupName('');
        setIsDialogOpen(false);
        location.reload();
      })
      .catch((error) => {
        return toast({
          title: 'Error adding a new student group',
          description: error.message || 'An unexpected error occurred.',
          variant: 'destructive',
        });
      });
  };

  return (
    <DashboardPageContainer>
      <Dialog open={isDialogOpen} onOpenChange={setIsDialogOpen}>
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
              <Input
                id="studentGroupName"
                className="col-span-3"
                value={studentGroupName}
                onChange={(e) => handleInputChange(e, setStudentGroupName)}
              />
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
              <Input
                type="number"
                id="enrolmentYear"
                className="col-span-3"
                min={2020}
                max={2100}
                step={1}
                value={enrolmentYear}
                onChange={(e) => handleInputChange(e, setEnrolmentYear)}
              />
            </div>
          </div>
          <DialogFooter>
            <Button type="submit" onClick={handleAdd}>
              Submit
            </Button>
          </DialogFooter>
        </DialogContent>
      </Dialog>

      <Button variant="outline" className="ml-4">
        Import a list
      </Button>

      {isLoading ? <Loading /> : <DataTable columns={columns} data={studentGroups} filterBy="name" />}
    </DashboardPageContainer>
  );
}

export default StudentGroupsPage;
