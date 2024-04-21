import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { ChevronLeft, ChevronRight, RotateCw } from 'lucide-react';
import { CodeBracketIcon, CommandLineIcon } from '@heroicons/react/24/outline';
import { ResizableHandle, ResizablePanel, ResizablePanelGroup } from '@/components/ui/resizable.tsx';
import { Textarea } from '@/components/ui/textarea.tsx';
import { Button } from '@/components/ui/button.tsx';
import { Badge } from '@/components/ui/badge.tsx';
import { TestingSession } from '@/features/dashboard/testing-sessions/TestingSession.ts';
import { QuestionExercise } from '@/features/testing-session/models/QuestionExercise.ts';
import { MethodCodingExercise } from '@/features/testing-session/models/MethodCodingExercise.ts';
import agent from '@/api/agent.ts';
import { toast } from '@/components/ui/use-toast.ts';
import Loading from '@/components/loading';
import Note from '@/features/testing-session/components/Note.component.tsx';
import Timer from '@/features/testing-session/components/Timer.component.tsx';
import ExerciseDifficultyBadge from '@/features/testing-session/components/ExerciseDifficultyBadge.component.tsx';
import TopicsBadge from '@/features/testing-session/components/TopicsBadge.component.tsx';

export default function TestingSessionPage() {
  const { id } = useParams<{ id?: string }>();

  const [testingSession, setTestingSession] = useState<TestingSession>();
  const [testing, setTesting] = useState();
  const [test, setTest] = useState();
  const [currentExercise, setCurrentExercise] = useState<QuestionExercise | MethodCodingExercise | undefined>();
  const [currentExerciseNumber, setCurrentExerciseNumber] = useState(1);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const testId = parseInt(id!, 10);

        if (!id) {
          return;
        }

        const testingSession = await agent.TestingSessions.getById(testId);
        setTestingSession(testingSession);

        const testing = await agent.Testings.getById(testingSession.testingId);
        setTesting(testing);

        const test = await agent.Tests.getByIdWithExerciseIds(testing.testId);
        setTest(test);

        // Setup first exercise
        if (test.methodCodingExercises.length > 0) {
          const firstMethodCodingExercise = await agent.Exercises.getMethodCodingById(test.methodCodingExercises[0]);
          setCurrentExercise(firstMethodCodingExercise);
        } else if (test.questionExercises.length > 0) {
          const firstQuestionExercise = await agent.Exercises.getQuestionById(test.questionExercises[0]);
          setCurrentExercise(firstQuestionExercise);
        }
      } catch (error) {
        toast({
          title: 'Error fetching data',
          description: error.message || 'An unexpected error occurred.',
          variant: 'destructive',
        });
      }
    };

    fetchData();
  }, [id]);

  console.log(testingSession);
  console.log(testing);
  console.log(test);
  console.log(currentExercise);

  function isMethodCodingExercise(exercise: QuestionExercise | MethodCodingExercise): exercise is MethodCodingExercise {
    return (exercise as MethodCodingExercise).methodSolutionCode !== undefined;
  }

  const fetchAndSetQuestionExercise = async (questionId: number) => {
    try {
      const questionExercise = await agent.Exercises.getQuestionById(questionId);
      const index = test.questionExercises.indexOf(questionId);
      setCurrentExercise(questionExercise);
      setCurrentExerciseNumber(index + 1); // Set by index + 1
    } catch (error) {}
  };

  const fetchAndSetMethodCodingExercise = async (exerciseId: number) => {
    try {
      const methodCodingExercise = await agent.Exercises.getMethodCodingById(exerciseId);
      const index = test.methodCodingExercises.indexOf(exerciseId);
      setCurrentExercise(methodCodingExercise);
      setCurrentExerciseNumber(index + 1);
    } catch (error) {}
  };

  if (currentExercise === undefined) {
    return <Loading />;
  }

  return (
    <div className="flex h-screen flex-col bg-zinc-50 p-4">
      <div className="mb-4 flex flex-wrap items-center space-x-2">
        {/* Row for Questions */}
        <div className="flex flex-wrap items-center space-x-1.5 space-y-1">
          <span className="whitespace-nowrap">Questions:</span>
          {test?.questionExercises.map((questionId, index) => (
            <Button
              key={questionId}
              className="size-7 rounded-md p-0 text-xs"
              variant="outline"
              onClick={() => fetchAndSetQuestionExercise(questionId)}
            >
              {index + 1}
            </Button>
          ))}
        </div>

        {/* Row for Exercises */}
        <div className="flex flex-wrap items-center space-x-1.5 space-y-1">
          <span className="whitespace-nowrap">Exercises:</span>
          {test?.methodCodingExercises.map((exerciseId, index) => (
            <Button
              key={exerciseId}
              className="size-7 rounded-md p-0 text-xs"
              variant="outline"
              onClick={() => fetchAndSetMethodCodingExercise(exerciseId)}
            >
              {index + 1}
            </Button>
          ))}
        </div>
      </div>

      {/* Resizable panels for the main content */}
      <ResizablePanelGroup direction="horizontal" className="flex flex-1 overflow-hidden">
        {/* Left panel for exercise descriptions */}
        <ResizablePanel defaultSize={50} maxSize={60} className="overflow-auto">
          <div className="grid h-full grid-rows-[auto_1fr] gap-2">
            <div className="space-x-2 space-y-1">
              <Badge variant="outline" className="w-[68px]">
                {testingSession && <Timer finishDateTime={testingSession.finishDateTime} />}
              </Badge>

              <Badge variant="outline" className="truncate">
                {isMethodCodingExercise(currentExercise)
                  ? `Exercise ${currentExerciseNumber}`
                  : `Question ${currentExerciseNumber}`}
              </Badge>

              <ExerciseDifficultyBadge difficulty={currentExercise.difficulty} />

              <TopicsBadge topics={currentExercise.exerciseTopics} />
            </div>

            {/* Exercise description and related content */}
            <div className="space-y-3 overflow-auto rounded-xl border bg-zinc-100 p-4">
              <h2 className="text-2xl font-semibold">{currentExercise.title}</h2>

              <p className="text-sm leading-6">{currentExercise.description}</p>

              {isMethodCodingExercise(currentExercise) && (
                <>
                  {currentExercise.inputOutputExamples.map((example, index) => (
                    <div key={index}>
                      <h3 className="font-semibold">Example {index + 1}:</h3>
                      <div className="rounded bg-zinc-200/70 p-2">
                        <p className="px-2">
                          <b>Input:</b> {example.input}
                        </p>
                        <p className="px-2">
                          <b>Output:</b> {example.output}
                        </p>
                      </div>
                    </div>
                  ))}

                  {currentExercise.exerciseNotes && (
                    <div>
                      <h3 className="font-semibold">Notes:</h3>
                      <ul className="list-inside space-y-1">
                        {currentExercise.exerciseNotes.map((note, index) => (
                          <Note key={index} decoration={note.decoration} entry={note.entry} />
                        ))}
                      </ul>
                    </div>
                  )}
                </>
              )}
            </div>
          </div>
        </ResizablePanel>

        <ResizableHandle withHandle className="mx-2" />

        {/* Right panel for the code input */}
        <ResizablePanel defaultSize={50} minSize={40} className="flex h-full flex-col">
          {/* Solution */}
          <div className="mb-4 flex-1 overflow-hidden rounded-xl border bg-white p-4">
            {/* Header */}
            <div className="-mx-4 -mt-4 flex items-center rounded-t-lg bg-green-600 p-1.5 text-white">
              <CodeBracketIcon width="20" height="20" className="mx-2" />
              <p className="font-semibold">Solution</p>
            </div>
            {/* Textarea container */}
            <div className="mt-4 flex h-full flex-col">
              {/* Stretch the textarea to fill the container */}
              <Textarea className="mb-9 flex-1 resize-none rounded-sm">{currentExercise.methodSolutionCode}</Textarea>
            </div>
          </div>

          {/* Output */}
          <div className="flex-none rounded-xl border bg-white px-4">
            {/* Header */}
            <div className="-mx-4 flex items-center rounded-t-lg bg-green-600 p-1.5 text-white">
              <CommandLineIcon width="20" height="20" className="mx-2" />
              <p className="font-semibold">Output</p>
            </div>
            <div className="mt-4 flex flex-col">
              {/* Stretch the textarea to fill the container */}
              <Textarea className="h-28 resize-none rounded-sm" readOnly={true} />
            </div>
            {/* Buttons at the bottom */}
            <div className="mb-4 mt-4 flex flex-wrap justify-between gap-2 space-x-2">
              <div className="flex space-x-2">
                <Button variant="outline">
                  <ChevronLeft width="18" className="-ml-1" />
                  Back
                </Button>
                <Button variant="secondary" className="hover:bg-zinc-200">
                  <RotateCw width="16" className="-ml-1 mr-2" />
                  Reset
                </Button>
              </div>
              <div className="flex space-x-2">
                <Button className="bg-green-600 hover:bg-green-700">Attempt</Button>
                <Button variant="outline">
                  Next
                  <ChevronRight width="18" className="-mr-1" />
                </Button>
              </div>
            </div>
          </div>
        </ResizablePanel>
      </ResizablePanelGroup>
    </div>
  );
}
