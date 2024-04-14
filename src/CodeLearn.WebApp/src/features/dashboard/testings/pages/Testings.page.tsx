import React, { useEffect, useState } from 'react';
import { format } from 'date-fns';
import { Check, ChevronsUpDown } from 'lucide-react';
import { Calendar as CalendarIcon } from 'lucide-react';
import { useDashboardPageTitle } from '@/components/layout';
import { columns } from '../Testings.columns.tsx';
import { DataTable } from '@/components/ui/data-table.tsx';
import { Button } from '@/components/ui/button.tsx';
import { useToast } from '@/components/ui/use-toast.ts';
import agent from '@/api/agent.ts';
import Loading from '@/components/loading';
import DashboardPageContainer from '@/features/dashboard/DashboardPageContainer.tsx';
import {
  Sheet,
  SheetClose,
  SheetContent,
  SheetDescription,
  SheetFooter,
  SheetHeader,
  SheetTitle,
  SheetTrigger,
} from '@/components/ui/sheet.tsx';
import { Test } from '@/features/dashboard/tests/models/Test.ts';
import { Popover, PopoverContent, PopoverTrigger } from '@/components/ui/popover.tsx';
import { Command, CommandGroup, CommandItem, CommandList } from '@/components/ui/command.tsx';
import { Testing } from '@/features/dashboard/testings/Testing.ts';
import { StudentGroup } from '@/features/dashboard/student-groups/StudentGroup.ts';
import { Label } from '@/components/ui/label.tsx';
import { cn } from '@/lib/utils.ts';
import { Calendar } from '@/components/ui/calendar.tsx';
import { Input } from '@/components/ui/input.tsx';
import { CreateTestingRequest } from '@/api/testings/CreateTestingRequest.ts';

export default function TestingsPage() {
  const { toast } = useToast();
  const [, setCurrentPageTitle] = useDashboardPageTitle();

  const [isLoading, setIsLoading] = useState(true);
  const [testings, setTestings] = useState<Testing[]>([]);
  const [tests, setTests] = useState<Test[]>([]);
  const [studentGroups, setStudentGroups] = useState<StudentGroup[]>([]);

  // Add new testing states
  const [selectedTest, setSelectedTest] = useState<Test>();
  const [selectedStudentGroup, setSelectedStudentGroup] = useState<StudentGroup>();
  const [selectedDate, setSelectedDate] = useState<Date>();
  const [duration, setDuration] = useState<number>(90);

  useEffect(() => {
    setCurrentPageTitle('Testings');
    setIsLoading(true);
    Promise.all([agent.Testings.list(), agent.Tests.list(), agent.StudentGroup.list()])
      .then(([testingsData, testsData, studentGroupsData]) => {
        setTestings(testingsData);
        setTests(testsData);
        setStudentGroups(studentGroupsData);
      })
      .catch((error) => {
        toast({
          title: 'Error fetching data',
          description: error.message || 'An unexpected error occurred.',
          variant: 'destructive',
        });
      })
      .finally(() => setIsLoading(false));
  }, [toast]);

  const handleSelectTest = (testId: number) => {
    const test = tests.find((t: Test) => t.id === testId);
    setSelectedTest(test);
  };

  const handleSelectStudentGroup = (groupId: number) => {
    const group = studentGroups.find((t: StudentGroup) => t.id === groupId);
    setSelectedStudentGroup(group);
  };

  const handleDurationChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setDuration(Number(e.target.value));
  };

  const handleOnSubmit = async (event: React.FormEvent) => {
    event.preventDefault();

    // Validation
    if (!selectedTest || !selectedStudentGroup || !selectedDate) {
      toast({
        title: 'Error',
        description: 'You must select a test, a student group, and a date.',
        variant: 'destructive',
      });
      return;
    }

    const createTestingRequest: CreateTestingRequest = {
      testId: selectedTest.id,
      studentGroupId: selectedStudentGroup.id,
      deadlineDate: selectedDate,
      durationInMinutes: duration,
    };

    agent.Testings.create(createTestingRequest)
      .then(() => {
        location.reload();
      })
      .catch((error) => {
        toast({
          title: 'Error creating testing',
          description: error.message || 'An unexpected error occurred.',
          variant: 'destructive',
        });
      });
  };

  return (
    <DashboardPageContainer>
      <div>
        <Sheet>
          <SheetTrigger asChild>
            <Button className="mb-6">Schedule testing</Button>
          </SheetTrigger>
          <SheetContent>
            <form onSubmit={handleOnSubmit}>
              <SheetHeader className="mb-2">
                <SheetTitle>Start testing</SheetTitle>
                <SheetDescription>
                  This action will initiate testing for a student group at selected date time.
                </SheetDescription>
              </SheetHeader>

              <div className="grid gap-4 py-4">
                <Label>Test</Label>
                <Popover>
                  <PopoverTrigger asChild>
                    <Button className="justify-between" role="combobox" variant="outline">
                      {selectedTest ? selectedTest.title : 'Select a test'}
                      <ChevronsUpDown className="ml-2 h-4 w-4 shrink-0 opacity-50" />
                    </Button>
                  </PopoverTrigger>
                  <PopoverContent className="w-60 rounded-lg bg-white p-0 shadow-lg">
                    <Command>
                      {isLoading ? (
                        <Loading />
                      ) : (
                        <CommandList>
                          <CommandGroup>
                            {tests.map((test: Test) => (
                              <CommandItem
                                key={test.id}
                                onSelect={() => handleSelectTest(test.id)}
                                className="flex items-center justify-between p-2 hover:bg-gray-100"
                              >
                                <Check
                                  className={cn(
                                    'mr-2 h-4 w-4',
                                    selectedTest?.id === test.id ? 'opacity-100' : 'opacity-0',
                                  )}
                                />
                                <span className="flex-1 text-left">{test.title}</span>
                              </CommandItem>
                            ))}
                          </CommandGroup>
                        </CommandList>
                      )}
                    </Command>
                  </PopoverContent>
                </Popover>
              </div>

              <div className="grid gap-4 py-4">
                <Label>Student Group</Label>
                <Popover>
                  <PopoverTrigger asChild>
                    <Button className="justify-between" role="combobox" variant="outline">
                      {selectedStudentGroup ? selectedStudentGroup.name : 'Select a student group'}
                      <ChevronsUpDown className="ml-2 h-4 w-4 shrink-0 opacity-50" />
                    </Button>
                  </PopoverTrigger>
                  <PopoverContent className="w-60 rounded-lg bg-white p-0 shadow-lg">
                    <Command>
                      {isLoading ? (
                        <div>Loading student groups...</div>
                      ) : (
                        <CommandList>
                          <CommandGroup>
                            {studentGroups.map((group: StudentGroup) => (
                              <CommandItem
                                key={group.id}
                                onSelect={() => handleSelectStudentGroup(group.id)}
                                className="flex items-center justify-between p-2 hover:bg-gray-100"
                              >
                                <Check
                                  className={cn(
                                    'mr-2 h-4 w-4',
                                    selectedStudentGroup?.id === group.id ? 'opacity-100' : 'opacity-0',
                                  )}
                                />
                                <span className="flex-1 text-left">{group.name}</span>
                              </CommandItem>
                            ))}
                          </CommandGroup>
                        </CommandList>
                      )}
                    </Command>
                  </PopoverContent>
                </Popover>
              </div>

              <div className="grid gap-4 py-4">
                <Label>Deadline</Label>
                <Popover>
                  <PopoverTrigger asChild>
                    <Button
                      variant={'outline'}
                      className={cn('justify-start text-left font-normal', !selectedDate && 'text-muted-foreground')}
                    >
                      <CalendarIcon className="mr-2 h-4 w-4" />
                      {selectedDate ? format(selectedDate, 'PPP') : <span>Pick a date</span>}
                    </Button>
                  </PopoverTrigger>
                  <PopoverContent className="w-full p-0">
                    <Calendar mode="single" selected={selectedDate} onSelect={setSelectedDate} initialFocus />
                  </PopoverContent>
                </Popover>
              </div>

              <div className="grid gap-4 py-4">
                <Label>Duration In Minutes</Label>
                <Input type="number" min={5} max={300} step={5} value={duration} onChange={handleDurationChange} />
              </div>

              <SheetFooter>
                <SheetClose asChild>
                  <Button type="submit" className="mt-8">
                    Create
                  </Button>
                </SheetClose>
              </SheetFooter>
            </form>
          </SheetContent>
        </Sheet>
      </div>

      {isLoading ? <Loading /> : <DataTable columns={columns} data={testings} filterBy="studentGroupName" />}
    </DashboardPageContainer>
  );
}
