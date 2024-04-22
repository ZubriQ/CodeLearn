import { useEffect, useState } from 'react';
import { useDashboardPageTitle } from '@/components/layout';
import { Card, CardDescription, CardFooter, CardHeader, CardTitle } from '@/components/ui/card.tsx';
import { Button } from '@/components/ui/button.tsx';
import agent from '@/api/agent.ts';
import { toast } from '@/components/ui/use-toast.ts';
import { useNavigate } from 'react-router-dom';
import { TestingSession } from '@/features/curriculum/models/TestingSession.ts';

function CurriculumTestingSessionsPage() {
  const [, setCurrentPageTitle] = useDashboardPageTitle();
  const navigate = useNavigate();

  const [testingSessions, setTestingSessions] = useState<TestingSession[]>([]); // Initialize with an empty array

  useEffect(() => {
    setCurrentPageTitle('Testing Sessions');

    agent.TestingSessions.listForCurriculum()
      .then((sessions) => {
        setTestingSessions(sessions);
      })
      .catch((error) => {
        toast({
          title: 'Error fetching testing sessions',
          description: error.message || 'An unexpected error occurred.',
          variant: 'destructive',
        });
      });
  }, []);

  const handleContinueClick = (testingSessionId: number) => {
    navigate(`/testing-session/${testingSessionId}`);
  };

  const handleSeeResultClick = (testingSessionId: number) => {
    navigate(`${testingSessionId}`);
  };

  if (testingSessions === undefined) {
    return <></>;
  }

  return (
    <ul className="mx-auto grid min-w-80 grid-cols-1 gap-4 sm:grid-cols-1 md:grid-cols-1 lg:grid-cols-2 xl:grid-cols-3">
      {testingSessions.map((session) => {
        const startDate = new Date(session.startDateTime);
        const formattedStartDateTime = new Intl.DateTimeFormat('ru-RU', {
          year: 'numeric',
          month: 'numeric',
          day: '2-digit',
          minute: '2-digit',
          hour: '2-digit',
          hour12: false,
        }).format(startDate);

        const finishDate = new Date(session.finishDateTime);
        const formattedFinishDateTime = new Intl.DateTimeFormat('ru-RU', {
          year: 'numeric',
          month: 'numeric',
          day: '2-digit',
          minute: '2-digit',
          hour: '2-digit',
          hour12: false,
        }).format(finishDate);

        return (
          <li key={session.id}>
            <Card className="transition-colors duration-500 hover:bg-zinc-50">
              <CardHeader>
                <CardTitle className="mb-2">{session.testTitle}</CardTitle>
                <CardDescription>
                  Date: {formattedStartDateTime} - {formattedFinishDateTime}
                </CardDescription>
                <div className="flex justify-between">
                  <CardDescription>Status: {session.status}</CardDescription>
                  <CardDescription>Score: {session.score}</CardDescription>
                </div>
              </CardHeader>
              <CardFooter className="flex justify-end">
                {session.status === 'InProgress' ? (
                  <Button size="icon" className="w-full" onClick={() => handleContinueClick(session.id)}>
                    Continue
                  </Button>
                ) : (
                  <Button size="icon" className="w-full" onClick={() => handleSeeResultClick(session.id)}>
                    See result
                  </Button>
                )}
              </CardFooter>
            </Card>
          </li>
        );
      })}
    </ul>
  );
}

export default CurriculumTestingSessionsPage;
