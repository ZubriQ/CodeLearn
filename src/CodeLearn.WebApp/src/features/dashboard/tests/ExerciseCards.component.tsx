import { useNavigate } from 'react-router-dom';
import { TrashIcon } from '@heroicons/react/24/outline';
import { ExerciseDetails } from '@/features/dashboard/tests/models/ExerciseDetails.ts';
import { Card, CardDescription, CardFooter, CardHeader, CardTitle } from '@/components/ui/card.tsx';
import { Button } from '@/components/ui/button.tsx';
import { Tooltip, TooltipContent, TooltipProvider, TooltipTrigger } from '@/components/ui/tooltip.tsx';
import NoItemsCard from '@/components/no-items-card';
import { toast } from '@/components/ui/use-toast.ts';
import agent from '@/api/agent.ts';
import { ExerciseType } from '@/features/testing-session/models/ExerciseType.ts';

type ExerciseCardsProps = {
  exercises: ExerciseDetails[];
  exerciseType: ExerciseType;
};

function ExerciseCards(props: ExerciseCardsProps) {
  const navigate = useNavigate();

  if (props.exercises.length === 0) {
    return <NoItemsCard itemName="exercise" itemNamePlural="exercises" className="my-4" />;
  }

  return (
    <ul className="mx-auto my-4 grid grid-cols-1 gap-4 sm:grid-cols-1 md:grid-cols-1 lg:grid-cols-2 xl:grid-cols-3">
      {props.exercises.map((exercise) => {
        const handleCardClick = () => {
          if (props.exerciseType === 'MethodCoding') {
            navigate(`method-coding-exercises/${exercise.id}`);
          } else {
            navigate(`question-exercises/${exercise.id}`);
          }
        };

        return (
          <li key={exercise.id}>
            <Card className="cursor-pointer transition-colors duration-500 hover:bg-zinc-100" onClick={handleCardClick}>
              <CardHeader>
                <CardTitle>{exercise.title}</CardTitle>
                <CardDescription>{exercise.description}</CardDescription>
              </CardHeader>
              <CardFooter className="flex justify-end">
                <TooltipProvider delayDuration={100}>
                  <Tooltip>
                    <TooltipTrigger asChild>
                      <Button
                        variant="outline"
                        size="icon"
                        onClick={(event) => {
                          event.stopPropagation();

                          agent.Exercises.delete(exercise.id)
                            .then(() => {
                              location.reload();
                            })
                            .catch((error) =>
                              toast({
                                title: 'Error deleting exercise',
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

export default ExerciseCards;
