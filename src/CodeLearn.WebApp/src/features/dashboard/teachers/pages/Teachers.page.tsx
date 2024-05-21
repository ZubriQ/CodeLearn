import React, { useEffect, useState } from 'react';
import { useDashboardPageTitle } from '@/components/layout';
import { useToast } from '@/components/ui/use-toast.ts';
import agent from '@/api/agent.ts';
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
import { columns } from '@/features/dashboard/teachers/Teachers.columns.tsx';
import { Label } from '@/components/ui/label.tsx';
import { Input } from '@/components/ui/input.tsx';
import { Teacher } from '@/features/dashboard/teachers/Teacher.ts';
import { RegisterTeacherRequest } from '@/api/users/RegisterTeacherRequest.ts';

function TeachersPage() {
  const { toast } = useToast();
  const [, setCurrentPageTitle] = useDashboardPageTitle();

  const [isLoading, setIsLoading] = useState(true);
  const [teachers, setTeachers] = useState<Teacher[]>([]);

  // Add new teacher states
  const [selectedFirstName, setSelectedFirstName] = useState<string>('');
  const [selectedLastName, setSelectedLastName] = useState<string>('');
  const [selectedPatronymic, setSelectedPatronymic] = useState<string>('');

  useEffect(() => {
    setCurrentPageTitle('Teachers');
    setIsLoading(true);
    agent.Teachers.list()
      .then((teachers) => {
        setTeachers(teachers);
      })
      .catch((error) => {
        toast({
          title: 'Error fetching teachers',
          description: error.message || 'An unexpected error occurred.',
          variant: 'destructive',
        });
      })
      .finally(() => setIsLoading(false));
  }, []);

  const handleOnRegisterNewTeacher = async (event: React.FormEvent) => {
    event.preventDefault();

    // Validation
    if (!selectedFirstName || !selectedLastName) {
      toast({
        title: 'Error',
        description: 'You must type correct data.',
        variant: 'destructive',
      });
      return;
    }

    const registerTeacherRequest: RegisterTeacherRequest = {
      firstName: selectedFirstName,
      lastName: selectedLastName,
      patronymic: selectedPatronymic,
    };

    agent.Teachers.create(registerTeacherRequest)
      .then(() => {
        window.location.reload();
      })
      .catch((error) => {
        toast({
          title: 'Error registering a teacher',
          description: error.response?.data?.error || 'An unexpected error occurred.',
          variant: 'destructive',
        });
      });
  };

  return (
    <DashboardPageContainer>
      <div>
        {/* Add new teacher */}
        <Sheet>
          <SheetTrigger asChild>
            <Button className="mb-6">Add new teacher</Button>
          </SheetTrigger>
          <SheetContent>
            <form onSubmit={handleOnRegisterNewTeacher}>
              <SheetHeader className="mb-4">
                <SheetTitle>Register new teacher</SheetTitle>
                <SheetDescription>This action will register a new teacher.</SheetDescription>
              </SheetHeader>

              <div className="grid gap-3 py-3">
                <Label>First Name</Label>
                <Input
                  id="firstName"
                  name="firstName"
                  maxLength={50}
                  value={selectedFirstName}
                  onChange={(e) => setSelectedFirstName(e.target.value)}
                />
              </div>

              <div className="grid gap-3 py-3">
                <Label>Last Name</Label>
                <Input
                  id="lastName"
                  name="lastName"
                  maxLength={50}
                  value={selectedLastName}
                  onChange={(e) => setSelectedLastName(e.target.value)}
                />
              </div>

              <div className="grid gap-3 py-3">
                <Label>Patronymic</Label>
                <Input
                  id="patronymic"
                  name="patronymic"
                  maxLength={50}
                  value={selectedPatronymic}
                  onChange={(e) => setSelectedPatronymic(e.target.value)}
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
      </div>

      {isLoading ? <Loading /> : <DataTable columns={columns} data={teachers} filterBy="fullName" />}
    </DashboardPageContainer>
  );
}

export default TeachersPage;
