import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { ChevronLeft, ChevronRight } from 'lucide-react';
import { useDispatch, useSelector } from 'react-redux';
import agent from '@/api/agent.ts';
import { ResizableHandle, ResizablePanel, ResizablePanelGroup } from '@/components/ui/resizable.tsx';
import { Button } from '@/components/ui/button.tsx';
import { Badge } from '@/components/ui/badge.tsx';
import { toast } from '@/components/ui/use-toast.ts';
import Loading from '@/components/loading';
import { TestingSession } from '@/features/dashboard/testing-sessions/TestingSession.ts';
import { QuestionExercise } from '@/features/testing-session/models/QuestionExercise.ts';
import Note from '@/features/testing-session/components/Note.component.tsx';
import Timer from '@/features/testing-session/components/Timer.component.tsx';
import ExerciseDifficultyBadge from '@/features/testing-session/components/ExerciseDifficultyBadge.component.tsx';
import TopicsBadge from '@/features/testing-session/components/TopicsBadge.component.tsx';
import { QuestionMarkCircleIcon } from '@heroicons/react/24/outline';
import { setSelectedChoices } from '@/features/testing-session/testing-session-slice.ts';
import { MethodCodingExercise } from '@/features/testing-session/models/MethodCodingExercise.ts';
import { setCompletedExercises } from '@/features/testing-session/completed-exercise-ids-slice.ts';
import CodeEditorWithOutput from '@/features/testing-session/components/CodeEditorWithOutput.component.tsx';
import { Dialog, DialogDescription, DialogFooter, DialogHeader, DialogTitle } from '@/components/ui/dialog';
import { DialogContent } from '@/components/ui/dialog.tsx';
import { Label } from '@/components/ui/label.tsx';
import { Textarea } from '@/components/ui/textarea.tsx';

export default function TestingSessionPage() {
  const { id } = useParams<{ id?: string }>();
  const dispatch = useDispatch();

  const [testingFinishedFeedbackMenu, setTestingFinishedFeedbackMenu] = useState(true);
  const [studentFeedback, setstudentFeedback] = useState('');

  const [testingSession, setTestingSession] = useState<TestingSession>();
  const [testing, setTesting] = useState();
  const [test, setTest] = useState();

  const [currentExercise, setCurrentExercise] = useState<QuestionExercise | MethodCodingExercise | undefined>();
  const [currentExerciseNumber, setCurrentExerciseNumber] = useState(1);
  const [currentExerciseType, setCurrentExerciseType] = useState<'question' | 'method'>('question');

  const [methodSolutionCode, setMethodSolutionCode] = useState<string>('');
  const [outputTextareaValue, setOutputTextareaValue] = useState<string>('');

  const completedExercises = useSelector((state) => state.completedExercises);
  const isCompleted = (exerciseId: number) => completedExercises.includes(exerciseId);

  // Questions
  const selectedChoices = useSelector((state) =>
    currentExercise ? state.exercise.selectedChoices[currentExercise.id] || [] : [],
  );
  const isSelected = (choiceId: number) => {
    return selectedChoices.includes(choiceId);
  };
  const toggleCheckbox = (choiceId: number) => {
    if (currentExercise && currentExercise.isMultipleAnswers) {
      // If multiple answers are allowed, toggle the selection of the checkbox
      dispatch(
        setSelectedChoices({
          exerciseId: currentExercise.id,
          choices: selectedChoices.includes(choiceId)
            ? selectedChoices.filter((id) => id !== choiceId) // Remove the choiceId if it's already selected
            : [...selectedChoices, choiceId], // Add the choiceId if it's not selected
        }),
      );
    } else {
      // If multiple answers are not allowed, set the selectedChoices to only this choiceId
      dispatch(
        setSelectedChoices({
          exerciseId: currentExercise.id,
          choices: [choiceId],
        }),
      );
    }
  };

  const handleNext = async () => {
    if (currentExerciseType === 'question') {
      if (currentExerciseNumber < test.questionExercises.length) {
        // Fetch and set the next question exercise
        await fetchAndSetQuestionExercise(test.questionExercises[currentExerciseNumber]);
      } else if (test.methodCodingExercises.length > 0) {
        // Switch to the first method coding exercise
        setCurrentExerciseType('method');
        await fetchAndSetMethodCodingExercise(test.methodCodingExercises[0]);
      }
    } else {
      if (currentExerciseNumber < test.methodCodingExercises.length) {
        // Fetch and set the next method coding exercise
        await fetchAndSetMethodCodingExercise(test.methodCodingExercises[currentExerciseNumber]);
      } else if (test.questionExercises.length > 0) {
        // Switch to the first question exercise
        setCurrentExerciseType('question');
        await fetchAndSetQuestionExercise(test.questionExercises[0]);
      }
    }
  };
  const handleBack = async () => {
    if (currentExerciseType === 'question') {
      if (currentExerciseNumber > 1) {
        // Fetch and set the previous question exercise
        await fetchAndSetQuestionExercise(test.questionExercises[currentExerciseNumber - 2]);
      } else if (test.methodCodingExercises.length > 0) {
        // Switch to the last method coding exercise
        setCurrentExerciseType('method');
        await fetchAndSetMethodCodingExercise(test.methodCodingExercises[test.methodCodingExercises.length - 1]);
      }
    } else {
      if (currentExerciseNumber > 1) {
        // Fetch and set the previous method coding exercise
        await fetchAndSetMethodCodingExercise(test.methodCodingExercises[currentExerciseNumber - 2]);
      } else if (test.questionExercises.length > 0) {
        // Switch to the last question exercise
        setCurrentExerciseType('question');
        await fetchAndSetQuestionExercise(test.questionExercises[test.questionExercises.length - 1]);
      }
    }
  };

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
          setMethodSolutionCode(firstMethodCodingExercise.methodSolutionCode);
        } else if (test.questionExercises.length > 0) {
          const firstQuestionExercise = await agent.Exercises.getQuestionById(test.questionExercises[0]);
          setCurrentExercise(firstQuestionExercise);
        }

        // Setup already answered questions
        const fetchCompletedExercises = async () => {
          try {
            const completedExercises = await agent.TestingSessions.getCompletedExerciseIdsById(id);
            dispatch(setCompletedExercises(completedExercises));
          } catch (error) {
            console.error('Failed to fetch answered questions:', error);
          }
        };

        if (id) {
          await fetchCompletedExercises();
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

  function isMethodCodingExercise(exercise: QuestionExercise | MethodCodingExercise): exercise is MethodCodingExercise {
    return (exercise as MethodCodingExercise).methodSolutionCode !== undefined;
  }

  const fetchAndSetQuestionExercise = async (questionId: number) => {
    try {
      const questionExercise = await agent.Exercises.getQuestionById(questionId);
      const index = test.questionExercises.indexOf(questionId);
      setCurrentExercise(questionExercise);
      setCurrentExerciseNumber(index + 1);
    } catch (error) {}
  };

  const fetchAndSetMethodCodingExercise = async (exerciseId: number) => {
    try {
      const methodCodingExercise = await agent.Exercises.getMethodCodingById(exerciseId);
      const index = test.methodCodingExercises.indexOf(exerciseId);
      setCurrentExercise(methodCodingExercise);
      setCurrentExerciseNumber(index + 1);

      setInitialMethodSolutionCode(methodCodingExercise.methodSolutionCode);
    } catch (error) {}
  };

  const handleSendMethodCodingSolution = async () => {
    const testingSessionId = parseInt(id, 10);

    const requestPayload = {
      exerciseId: currentExercise.id,
      methodSolutionCode: methodSolutionCode,
    };

    try {
      const response = await agent.ExerciseSubmissions.createMethodCodingSubmission(testingSessionId, requestPayload);
      setOutputTextareaValue(response.testingInfoOutput);

      if (response.isSuccess) {
        dispatch(setCompletedExercises([...completedExercises, currentExercise.id]));
      }
    } catch (error) {
      // Handle errors
      toast({
        title: 'Submission failed',
        description: error.message || 'An unexpected error occurred.',
        variant: 'destructive',
      });
    }
  };

  const handleSendQuestionChoices = async () => {
    const testingSessionId = parseInt(id, 10);

    const requestPayload = {
      exerciseId: currentExercise.id,
      selectedAnswers: selectedChoices,
    };

    try {
      await agent.ExerciseSubmissions.createQuestionSubmission(testingSessionId, requestPayload);
      dispatch(setCompletedExercises([...completedExercises, currentExercise.id]));
    } catch (error) {
      // Handle errors
      toast({
        title: 'Submission failed',
        description: error.message || 'An unexpected error occurred.',
        variant: 'destructive',
      });
    }
  };
  const navigate = useNavigate();
  const handleFinishTesting = () => {
    setTestingFinishedFeedbackMenu(false);

    const testingSessionId = parseInt(id, 10);
    agent.TestingSessions.finishTestingSession(testingSessionId, { studentFeedback })
      .then(() => {
        navigate('/curriculum');
      })
      .catch((error) => {
        toast({
          title: 'Error finishing testing session',
          description: error.response?.data?.error || 'An unexpected error occurred.',
          variant: 'destructive',
        });
      });
  };

  if (currentExercise === undefined) {
    return <Loading />;
  }

  return (
    <div className="flex h-screen flex-col bg-zinc-50 p-4">
      <div className="-mt-2 mb-3 flex flex-wrap items-center space-x-2">
        {/* Row for Questions */}
        <div className="flex flex-wrap items-center space-x-1.5 space-y-1">
          <span className="whitespace-nowrap">Questions:</span>
          {test?.questionExercises.map((exerciseId, index) => (
            <Button
              key={exerciseId}
              className={`size-7 rounded-md p-0 text-xs`}
              variant={`${isCompleted(exerciseId) ? 'default' : 'outline'}`}
              onClick={() => fetchAndSetQuestionExercise(exerciseId)}
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
              variant={`${isCompleted(exerciseId) ? 'default' : 'outline'}`}
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
                      <div className="rounded bg-zinc-200/55 p-2">
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

        {/* Right panel for the code input/output or questions */}
        <ResizablePanel defaultSize={50} minSize={40} className="flex h-full flex-col">
          {isMethodCodingExercise(currentExercise) ? (
            <CodeEditorWithOutput
              isExerciseCompleted={isCompleted(currentExercise.id)}
              methodSolutionCode={methodSolutionCode}
              onMethodSolutionCodeChange={setMethodSolutionCode}
              handleBack={handleBack}
              handleSendMethodCodingSolution={handleSendMethodCodingSolution}
              handleNext={handleNext}
              outputTextareaValue={outputTextareaValue}
            />
          ) : (
            <>
              {/* Questions */}
              <div className="flex h-full flex-col rounded-xl border bg-white px-4">
                <div className="-mx-4 flex items-center rounded-t-xl bg-green-600 p-1.5 text-white">
                  <QuestionMarkCircleIcon width="20" height="20" className="mx-2 min-w-5" />
                  <p className="truncate font-semibold">
                    {currentExercise.isMultipleAnswers ? 'Select at least 2 answers' : 'Select correct answer'}
                  </p>
                </div>

                {/* Answers */}
                <div className="mt-4 flex-grow">
                  {currentExercise.questionChoices.map((choice) => (
                    <div key={choice.id} className="flex items-center space-x-2 px-2 pb-4">
                      <input
                        type="checkbox"
                        checked={isSelected(choice.id)}
                        onChange={() => toggleCheckbox(choice.id)}
                        disabled={isCompleted(currentExercise.id)}
                        className="size-4 cursor-pointer accent-neutral-900"
                      />
                      <p className="text-sm">{choice.text}</p>
                    </div>
                  ))}
                </div>

                <div className="mb-4 mt-4 flex flex-wrap justify-between gap-2 space-x-2">
                  <div className="flex space-x-2">
                    <Button variant="outline" onClick={handleBack}>
                      <ChevronLeft width="18" className="-ml-1" />
                      Back
                    </Button>
                  </div>
                  <div className="flex space-x-2">
                    <Button
                      className="bg-green-600 hover:bg-green-700"
                      disabled={isCompleted(currentExercise.id)}
                      onClick={handleSendQuestionChoices}
                    >
                      {isCompleted(currentExercise.id) ? 'Completed' : 'Answer'}
                    </Button>
                    <Button variant="outline" onClick={handleNext}>
                      Next
                      <ChevronRight width="18" className="-mr-1" />
                    </Button>
                  </div>
                </div>
              </div>
            </>
          )}
        </ResizablePanel>
      </ResizablePanelGroup>

      <Dialog open={testingFinishedFeedbackMenu}>
        <DialogContent className="sm:max-w-[425px]">
          <DialogHeader>
            <DialogTitle className="mb-1">Testing has been finished</DialogTitle>
            <DialogDescription>
              Thank you so much for your participation in the testing. Your insights are invaluable to us, and we truly
              appreciate your feedback. Your contribution helps us improve and deliver a better experience. We look
              forward to hearing from you.
            </DialogDescription>
          </DialogHeader>
          <div className="grid gap-4 py-4">
            <Label>Feedback</Label>
            <Textarea value={studentFeedback} maxLength={450} onChange={(e) => setstudentFeedback(e.target.value)} />
          </div>
          <DialogFooter>
            <Button onClick={handleFinishTesting}>Finish</Button>
          </DialogFooter>
        </DialogContent>
      </Dialog>
    </div>
  );
}
