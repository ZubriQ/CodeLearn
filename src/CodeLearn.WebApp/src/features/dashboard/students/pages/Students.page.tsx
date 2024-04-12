import React, { useEffect, useState } from 'react';
import { useDashboardPageTitle } from '@/components/layout';
import { useToast } from '@/components/ui/use-toast.ts';
import { StudentGroup } from '@/features/dashboard/student-groups/StudentGroup.ts';
import agent from '@/api/agent.ts';
import { Student } from '@/features/dashboard/students/Student.ts';
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
import { Button } from '@/components/ui/button.tsx';
import Loading from '@/components/loading';
import { DataTable } from '@/components/ui/data-table.tsx';
import DashboardPageContainer from '@/features/dashboard/DashboardPageContainer.tsx';
import { columns } from '@/features/dashboard/students/Students.columns.tsx';
import { Label } from '@/components/ui/label.tsx';
import { Popover, PopoverContent, PopoverTrigger } from '@/components/ui/popover.tsx';
import { Check, ChevronsUpDown } from 'lucide-react';
import { Command, CommandGroup, CommandItem, CommandList } from '@/components/ui/command.tsx';
import { cn } from '@/lib/utils.ts';
import { Input } from '@/components/ui/input.tsx';
import { RegisterStudentRequest } from '@/api/users/RegisterStudentRequest.ts';

function StudentsPage() {
  const { toast } = useToast();
  const [, setCurrentPageTitle] = useDashboardPageTitle();

  const [isLoading, setIsLoading] = useState(true);
  const [students, setStudents] = useState<Student[]>([]);
  const [studentGroups, setStudentGroups] = useState<StudentGroup[]>([]);

  // Add new student states
  const [selectedStudentGroup, setSelectedStudentGroup] = useState<StudentGroup>();
  const [selectedStudentFirstName, setSelectedStudentFirstName] = useState<string>('');
  const [selectedStudentLastName, setSelectedStudentLastName] = useState<string>('');
  const [selectedStudentPatronymic, setSelectedStudentPatronymic] = useState<string>('');
  const [selectedStudentUserCode, setSelectedStudentUserCode] = useState<string>('');

  // Import student list states
  const [selectedImportStudentListGroup, setSelectedImportStudentListGroup] = useState<StudentGroup>();

  const handleSelectStudentGroup = (groupId: number) => {
    const group = studentGroups.find((t: StudentGroup) => t.id === groupId);
    setSelectedStudentGroup(group);
  };

  const handleSelectImportStudentListGroup = (groupId: number) => {
    const group = studentGroups.find((t: StudentGroup) => t.id === groupId);
    setSelectedImportStudentListGroup(group);
  };

  useEffect(() => {
    setCurrentPageTitle('Students');
    setIsLoading(true);
    Promise.all([agent.Students.list(), agent.StudentGroup.list()])
      .then(([studentsData, studentGroupsData]) => {
        setStudents(studentsData);
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

  const handleOnRegisterNewStudent = async (event: React.FormEvent) => {
    event.preventDefault();

    // Validation
    if (!selectedStudentFirstName || !selectedStudentLastName || !selectedStudentGroup || !selectedStudentUserCode) {
      toast({
        title: 'Error',
        description: 'You must select a student group, and input correct data.',
        variant: 'destructive',
      });
      return;
    }

    const registerStudentRequest: RegisterStudentRequest = {
      firstName: selectedStudentFirstName,
      lastName: selectedStudentLastName,
      patronymic: selectedStudentPatronymic,
      studentGroupName: selectedStudentGroup?.name,
      userCode: selectedStudentUserCode,
    };

    agent.Students.create(registerStudentRequest)
      .then(() => {
        window.location.reload();
      })
      .catch((error) => {
        toast({
          title: 'Error registering a student',
          description: error.response?.data?.error || 'An unexpected error occurred.',
          variant: 'destructive',
        });
      });
  };

  const handleOnImportStudentList = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    // Validation
    if (!selectedImportStudentListGroup || !event.currentTarget.excelFile.files?.length) {
      toast({
        title: 'Error',
        description: 'You must select a student group and a file to import.',
        variant: 'destructive',
      });
      return;
    }

    const formData = new FormData();
    formData.append('file', event.currentTarget.excelFile.files[0]);
    formData.append('studentGroupName', selectedImportStudentListGroup.name);

    agent.Students.importList(formData)
      .then(() => {
        window.location.reload();
      })
      .catch((error) => {
        toast({
          title: 'Error importing student list',
          description: error.response?.data?.error || 'An unexpected error occurred.',
          variant: 'destructive',
        });
      });
  };

  return (
    <DashboardPageContainer>
      <div>
        {/* Add new student */}
        <Sheet>
          <SheetTrigger asChild>
            <Button className="mb-6">Add new student</Button>
          </SheetTrigger>
          <SheetContent>
            <form onSubmit={handleOnRegisterNewStudent}>
              <SheetHeader className="mb-4">
                <SheetTitle>Register new student</SheetTitle>
                <SheetDescription>This action will register a new student.</SheetDescription>
              </SheetHeader>

              <div className="grid gap-3 py-3">
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

              <div className="grid gap-3 py-3">
                <Label>First Name</Label>
                <Input
                  id="firstName"
                  name="firstName"
                  maxLength={50}
                  value={selectedStudentFirstName}
                  onChange={(e) => setSelectedStudentFirstName(e.target.value)}
                />
              </div>

              <div className="grid gap-3 py-3">
                <Label>Last Name</Label>
                <Input
                  id="lastName"
                  name="lastName"
                  maxLength={50}
                  value={selectedStudentLastName}
                  onChange={(e) => setSelectedStudentLastName(e.target.value)}
                />
              </div>

              <div className="grid gap-3 py-3">
                <Label>Patronymic</Label>
                <Input
                  id="patronymic"
                  name="patronymic"
                  maxLength={50}
                  value={selectedStudentPatronymic}
                  onChange={(e) => setSelectedStudentPatronymic(e.target.value)}
                />
              </div>

              <div className="grid gap-3 py-3">
                <Label>User Code</Label>
                <Input
                  id="userCode"
                  name="userCode"
                  maxLength={50}
                  value={selectedStudentUserCode}
                  onChange={(e) => setSelectedStudentUserCode(e.target.value)}
                />
              </div>

              <SheetFooter>
                <SheetClose asChild>
                  <Button type="submit" className="mt-8">
                    Register
                  </Button>
                </SheetClose>
              </SheetFooter>
            </form>
          </SheetContent>
        </Sheet>

        {/* Import list of students */}
        <Sheet>
          <SheetTrigger asChild>
            <Button variant="outline" className="ml-4">
              Import a list
            </Button>
          </SheetTrigger>
          <SheetContent>
            <form onSubmit={handleOnImportStudentList}>
              <SheetHeader className="mb-4">
                <SheetTitle>Import student list</SheetTitle>
                <SheetDescription>
                  This action will register multiple students from a given Excel file.
                </SheetDescription>
              </SheetHeader>

              <div className="grid gap-3 py-3">
                <Label>Student Group</Label>
                <Popover>
                  <PopoverTrigger asChild>
                    <Button className="justify-between" role="combobox" variant="outline">
                      {selectedImportStudentListGroup ? selectedImportStudentListGroup.name : 'Select a student group'}
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
                                onSelect={() => handleSelectImportStudentListGroup(group.id)}
                                className="flex items-center justify-between p-2 hover:bg-gray-100"
                              >
                                <Check
                                  className={cn(
                                    'mr-2 h-4 w-4',
                                    selectedImportStudentListGroup?.id === group.id ? 'opacity-100' : 'opacity-0',
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

              <div className="grid gap-3 py-3">
                <Label htmlFor="excelFile">Excel File</Label>
                <Input type="file" id="excelFile" name="excelFile" />
              </div>

              <SheetFooter>
                <SheetClose asChild>
                  <Button type="submit" className="mt-8">
                    Import
                  </Button>
                </SheetClose>
              </SheetFooter>
            </form>
          </SheetContent>
        </Sheet>
      </div>

      {isLoading ? <Loading /> : <DataTable columns={columns} data={students} filterBy="fullName" />}
    </DashboardPageContainer>
  );
}

export default StudentsPage;
