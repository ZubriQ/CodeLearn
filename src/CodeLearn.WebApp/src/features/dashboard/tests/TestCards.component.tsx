import { useNavigate } from 'react-router-dom';
import { TrashIcon } from '@heroicons/react/24/outline';
import { Test } from '@/features/dashboard/tests/models/Test.ts';
import { Card, CardDescription, CardFooter, CardHeader, CardTitle } from '@/components/ui/card.tsx';
import { Button } from '@/components/ui/button.tsx';
import { Tooltip, TooltipContent, TooltipProvider, TooltipTrigger } from '@/components/ui/tooltip.tsx';
import NoItemsCard from '@/components/no-items-card';
import agent from '@/api/agent.ts';
import { toast } from '@/components/ui/use-toast.ts';

type TestCardsProps = {
  tests: Test[];
};

function TestCards(props: TestCardsProps) {
  const navigate = useNavigate();

  if (props.tests.length === 0) {
    return <NoItemsCard itemName="test" itemNamePlural="tests" />;
  }

  return (
    <ul className="mx-auto grid grid-cols-1 gap-4 sm:grid-cols-1 md:grid-cols-1 lg:grid-cols-2 xl:grid-cols-3">
      {props.tests.map((test) => {
        const handleCardClick = () => navigate(`/dashboard/tests/${test.id}`);

        return (
          <li key={test.id}>
            <Card
              className="min-w-80 cursor-pointer transition-colors duration-500 hover:bg-zinc-100"
              onClick={handleCardClick}
            >
              <CardHeader>
                <CardTitle>{test.title}</CardTitle>
                <CardDescription>{test.description}</CardDescription>
              </CardHeader>
              <CardFooter className="flex justify-end">
                <TooltipProvider delayDuration={100}>
                  <Tooltip>
                    <TooltipTrigger asChild>
                      <Button
                        variant="outline"
                        size="icon"
                        onClick={() => {
                          agent.Tests.delete(test.id)
                            .then(() => {
                              location.reload();
                            })
                            .catch((error) =>
                              toast({
                                title: 'Error deleting a test',
                                description: error.message || 'An unexpected error occurred.',
                                variant: 'destructive',
                              }),
                            );
                        }}
                      >
                        <TrashIcon className="h-4 w-4" />
                      </Button>
                    </TooltipTrigger>
                    <TooltipContent>
                      <p>Delete</p>
                    </TooltipContent>
                  </Tooltip>
                </TooltipProvider>
              </CardFooter>
            </Card>
          </li>
        );
      })}
    </ul>
  );
}

export default TestCards;
