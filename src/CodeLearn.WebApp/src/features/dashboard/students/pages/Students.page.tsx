import React, { useEffect, useState } from 'react';
import { useDashboardPageTitle } from '@/components/layout';
import { useToast } from '@/components/ui/use-toast.ts';
import { StudentGroup } from '@/features/dashboard/student-groups/StudentGroup.ts';
import agent from '@/api/agent.ts';
import { Student } from '@/features/dashboard/students/Student.ts';
import { Sheet, SheetContent, SheetTrigger } from '@/components/ui/sheet.tsx';
import { Button } from '@/components/ui/button.tsx';
import Loading from '@/components/loading';
import { DataTable } from '@/components/ui/data-table.tsx';
import DashboardPageContainer from '@/features/dashboard/DashboardPageContainer.tsx';
import { columns } from '@/features/dashboard/students/Students.columns.tsx';

function StudentsPage() {
  const { toast } = useToast();
  const [, setCurrentPageTitle] = useDashboardPageTitle();

  const [isLoading, setIsLoading] = useState(true);
  const [students, setStudents] = useState<Student[]>([]);
  const [studentGroups, setStudentGroups] = useState<StudentGroup[]>([]);

  // Add new student states
  // Will go here...

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

  const handleOnSubmit = async (event: React.FormEvent) => {
    event.preventDefault();

    // Validation
    // Goes here...

    // Create a dto

    // Add dto
    // try {
    //   await agent.Students.create(dto);
    //   location.reload();
    // } catch (error) {
    //   toast({
    //     title: 'Error creating student',
    //     description: error.message || 'An unexpected error occurred.',
    //     variant: 'destructive',
    //   });
    // }
  };

  return (
    <DashboardPageContainer>
      <div>
        <Sheet>
          <SheetTrigger asChild>
            <Button className="mb-6">Add new student</Button>
          </SheetTrigger>
          <SheetContent>
            <form onSubmit={handleOnSubmit}></form>
          </SheetContent>
        </Sheet>

        <Sheet>
          <SheetTrigger asChild>
            <Button variant="outline" className="ml-4">
              Import a list
            </Button>
          </SheetTrigger>
          <SheetContent>
            <form onSubmit={handleOnSubmit}></form>
          </SheetContent>
        </Sheet>
      </div>

      {isLoading ? <Loading /> : <DataTable columns={columns} data={students} filterBy="fullName" />}
    </DashboardPageContainer>
  );
}

export default StudentsPage;
