import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { useDashboardPageTitle } from '@/components/layout';
import { Test } from '@/features/dashboard/tests/models/Test.ts';
import agent from '@/api/agent.ts';
import { toast } from '@/components/ui/use-toast.ts';
import DashboardPageContainer from '@/features/dashboard/DashboardPageContainer.tsx';
import { TypographyH3 } from '@/components/typography/typography-h3.tsx';
import { TypographyH2 } from '@/components/typography/typography-h2.tsx';
import { TypographyP } from '@/components/typography/typography-p.tsx';
import { Button } from '@/components/ui/button.tsx';

export default function SingleTestPage() {
  const [test, setTest] = useState<Test | undefined>(undefined);
  const { id } = useParams<{ id?: string }>();
  const [, setCurrentPageTitle] = useDashboardPageTitle();
  const navigate = useNavigate();

  const handleAddNewMethodCodingExerciseClick = () => navigate(`exercises/`);

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

  if (!test) {
    return <TypographyH2>Test not found</TypographyH2>;
  }

  return (
    <DashboardPageContainer>
      <div className="mb-8">
        <TypographyH3>{test?.title}</TypographyH3>
        <TypographyP>{test?.description}</TypographyP>
      </div>

      <div className="mb-8 flex flex-row flex-wrap gap-4">
        <Button className="w-full sm:w-fit">Test group</Button>
        <Button variant="outline" className="w-full sm:w-fit" onClick={handleAddNewMethodCodingExerciseClick}>
          Add coding exercise
        </Button>
        <Button variant="outline" className="w-full sm:w-fit">
          Add question
        </Button>
      </div>

      <div className="mb-8">
        <TypographyH3>Exercises</TypographyH3>
      </div>

      {/* TODO: Render Method coding exercises */}

      {/* TODO: Render Question exercises */}
    </DashboardPageContainer>
  );
}
