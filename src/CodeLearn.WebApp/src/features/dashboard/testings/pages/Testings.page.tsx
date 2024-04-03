import { useEffect, useState } from 'react';
import { useDashboardPageTitle } from '@/components/layout';
import { columns } from '../Testings.columns.tsx';
import { DataTable } from '@/components/ui/data-table.tsx';
import { Button } from '@/components/ui/button.tsx';
import { useToast } from '@/components/ui/use-toast.ts';
import agent from '@/api/agent.ts';
import Loading from '@/components/loading';
import DashboardPageContainer from '@/features/dashboard/DashboardPageContainer.tsx';
import { Testing } from '@/features/dashboard/testings/Testing.ts';

export default function TestingsPage() {
  const { toast } = useToast();
  const [, setCurrentPageTitle] = useDashboardPageTitle();
  const [testings, setTestings] = useState<Testing[]>([]);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    setCurrentPageTitle('Testings');

    setIsLoading(true);
    agent.Testings.list()
      .then((testings) => {
        setTestings(testings);
        setIsLoading(false);
      })
      .catch((error) => {
        toast({
          title: 'Error fetching testings',
          description: error.message || 'An unexpected error occurred.',
          variant: 'destructive',
        });
        setIsLoading(false);
      });
  }, []);

  // const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>, setter: (value: string) => void) => {
  //   setter(e.target.value);
  // };

  const handleAdd = () => {
    // const newStudentGroup = {
    //   name: studentGroupName,
    //   enrolmentYear: parseInt(enrolmentYear, 10),
    // };
    //
    // agent.StudentGroup.create(newStudentGroup)
    //   .then(() => {
    //     setEnrolmentYear('');
    //     setStudentGroupName('');
    //     setIsDialogOpen(false);
    //     location.reload();
    //   })
    //   .catch((error) => {
    //     return toast({
    //       title: 'Error adding a new student group',
    //       description: error.message || 'An unexpected error occurred.',
    //       variant: 'destructive',
    //     });
    //   });
  };

  return (
    <DashboardPageContainer>
      <div className="whitespace-nowrap">
        <Button className="mb-6">Assign testing to a student group</Button>
      </div>

      {isLoading ? <Loading /> : <DataTable columns={columns} data={testings} filterBy="studentGroupName" />}
    </DashboardPageContainer>
  );
}
