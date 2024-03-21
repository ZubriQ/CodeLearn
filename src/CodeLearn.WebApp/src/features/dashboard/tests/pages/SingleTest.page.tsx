import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { useDashboardPageTitle } from '@/components/layout';
import { Test } from '@/features/dashboard/tests/models/Test.ts';
import agent from '@/api/agent.ts';
import { toast } from '@/components/ui/use-toast.ts';
import DashboardPageContainer from '@/features/dashboard/DashboardPageContainer.tsx';

export default function SingleTestPage() {
  const [test, setTest] = useState<Test | undefined>(undefined);
  const { id } = useParams<{ id?: string }>();
  const [, setCurrentPageTitle] = useDashboardPageTitle();

  useEffect(() => {
    setCurrentPageTitle('Test');

    const testId = parseInt(id!, 10);

    if (id) {
      agent.Tests.getById(testId)
        .then((fetchedTest) => {
          setTest(fetchedTest);
        })
        .catch((error) => {
          toast({
            title: 'Error fetching test',
            description: error.message || 'An unexpected error occurred.',
            variant: 'destructive',
          });
        });
    }
  }, [id]);

  return <DashboardPageContainer>Single Test</DashboardPageContainer>;
}
