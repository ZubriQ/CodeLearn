import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { useDashboardPageTitle } from '@/components/layout';
import agent from '@/api/agent.ts';
import { toast } from '@/components/ui/use-toast.ts';
import { TypographyH3 } from '@/components/typography/typography-h3.tsx';
import { Button } from '@/components/ui/button.tsx';
import { TypographyH2 } from '@/components/typography/typography-h2.tsx';
import { TestWithExercises } from '@/features/dashboard/tests/models/TestWithExercises.ts';
import ExerciseCards from '@/features/dashboard/tests/ExerciseCards.component.tsx';
import { TypographyMuted } from '@/components/typography/typography-muted.tsx';

export default function SingleTestPage(): JSX.Element {
  const [test, setTest] = useState<TestWithExercises | undefined>(undefined);
  const { id } = useParams<{ id?: string }>();
  const [, setCurrentPageTitle] = useDashboardPageTitle();
  const navigate = useNavigate();

  const handleAddMethodCodingExerciseClick = () => navigate(`exercises/add-method-coding`);

  const handleAddQuestionExerciseClick = () => {};

  useEffect(() => {
    setCurrentPageTitle('Test');

    const testId = parseInt(id!, 10);

    if (id) {
      agent.Tests.getByIdWithExercises(testId)
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
    return <TypographyH3>Test not found</TypographyH3>;
  }

  return (
    <div className="my-4">
      <div className="mb-8">
        <TypographyH2>{test?.title}</TypographyH2>
        <TypographyMuted>{test?.description}</TypographyMuted>
      </div>

      <div className="mb-8 flex flex-row flex-wrap gap-4">
        <Button className="w-full sm:w-fit" onClick={handleAddMethodCodingExerciseClick}>
          Add coding exercise
        </Button>
        <Button className="w-full sm:w-fit" onClick={handleAddQuestionExerciseClick}>
          Add question
        </Button>
      </div>

      <div className="mb-8">
        <TypographyH3>Coding Exercises</TypographyH3>
        <ExerciseCards exercises={test.methodCodingExercises} exerciseType="MethodCoding" />
      </div>

      <div className="mb-8">
        <TypographyH3>Questions</TypographyH3>
        <ExerciseCards exercises={test.questionExercises} exerciseType="Question" />
      </div>
    </div>
  );
}
