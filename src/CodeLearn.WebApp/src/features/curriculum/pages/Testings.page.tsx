import { useEffect, useState } from 'react';
import { useDashboardPageTitle } from '@/components/layout';
import { Card, CardDescription, CardFooter, CardHeader, CardTitle } from '@/components/ui/card.tsx';
import { Button } from '@/components/ui/button.tsx';
import agent from '@/api/agent.ts';
import { toast } from '@/components/ui/use-toast.ts';
import { useNavigate } from 'react-router-dom';
import { Testing } from '@/features/dashboard/testings/Testing.ts';
import { Badge } from '@/components/ui/badge.tsx';

function StudentTestingsPage() {
  const [, setCurrentPageTitle] = useDashboardPageTitle();
  const navigate = useNavigate();

  const [testings, setTestings] = useState<Testing[]>([]); // Initialize with an empty array

  useEffect(() => {
    setCurrentPageTitle('Testings');

    agent.Testings.listForStudent()
      .then((availableTestings) => {
        setTestings(availableTestings);
      })
      .catch((error) => {
        toast({
          title: 'Error fetching testings',
          description: error.message || 'An unexpected error occurred.',
          variant: 'destructive',
        });
      });
  }, []);

  const handleCardClick = (testingId: number) => {
    agent.TestingSessions.create({ testingId })
      .then((testingSessionId) => {
        navigate(`/testing-session/${testingSessionId}`);
      })
      .catch((error) => {
        toast({
          title: 'Error creating testing session',
          description: error.message || 'An unexpected error occurred.',
          variant: 'destructive',
        });
      });
  };

  if (testings === undefined) {
    return <></>;
  }

  return (
    <ul className="mx-auto grid min-w-80 grid-cols-1 gap-4 sm:grid-cols-1 md:grid-cols-1 lg:grid-cols-2 xl:grid-cols-3">
      {testings.map((testing) => {
        const date = new Date(testing.deadlineDate);
        const formattedDateTime = new Intl.DateTimeFormat('ru-RU', {
          year: 'numeric',
          month: 'numeric',
          day: '2-digit',
          hour12: false,
        }).format(date);

        return (
          <li key={testing.id}>
            <Card className="transition-colors duration-500 hover:bg-zinc-50">
              <CardHeader>
                <CardTitle className="mb-2">{testing.testTitle}</CardTitle>
                <CardDescription>Deadline: {formattedDateTime}</CardDescription>
                <CardDescription>Duration: {testing.durationInMinutes} minutes</CardDescription>
                <CardDescription>
                  Status:{' '}
                  <Badge variant="secondary">{testing.status === 'Completed' ? 'Missed' : testing.status}</Badge>
                </CardDescription>
              </CardHeader>
              <CardFooter className="flex justify-end">
                <Button
                  size="icon"
                  className="w-full bg-green-600 hover:bg-green-500"
                  onClick={() => handleCardClick(testing.id)}
                  disabled={testing.status === 'Completed'}
                >
                  Start
                </Button>
              </CardFooter>
            </Card>
          </li>
        );
      })}
    </ul>
  );
}

export default StudentTestingsPage;
