import { useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { useFieldArray, useForm } from 'react-hook-form';
import { CaretSortIcon, CheckIcon } from '@radix-ui/react-icons';
import { z } from 'zod';
import agent from '@/api/agent.ts';
import { cn } from '@/lib/utils.ts';
import { useDashboardPageTitle } from '@/components/layout';
import { TypographyH3 } from '@/components/typography/typography-h3.tsx';
import { zodResolver } from '@hookform/resolvers/zod';
import { toast } from '@/components/ui/use-toast.ts';
import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from '@/components/ui/form.tsx';
import { Input } from '@/components/ui/input.tsx';
import { Button } from '@/components/ui/button.tsx';
import { Textarea } from '@/components/ui/textarea.tsx';
import { Switch } from '@/components/ui/switch.tsx';
import { Popover, PopoverContent, PopoverTrigger } from '@/components/ui/popover';
import { Command, CommandEmpty, CommandGroup, CommandItem, CommandList } from '@/components/ui/command';
import { Card } from '@/components/ui/card.tsx';
import { TrashIcon } from '@heroicons/react/24/outline';

const answerSchema = z.object({
  text: z.string().min(1, 'Answer text cannot be empty'),
  isCorrect: z.boolean(),
});

// Input Validation Schema
const formSchema = z.object({
  title: z.string().min(3).max(100),
  description: z.string().min(10).max(1000),
  difficultyId: z.number(),
  isMultipleAnswers: z.boolean().default(false),
  answers: z.array(answerSchema),
});

export default function AddQuestionExercisePage() {
  const difficulties = [
    {
      id: 1,
      name: 'Easy',
    },
    {
      id: 2,
      name: 'Medium',
    },
    {
      id: 3,
      name: 'Hard',
    },
  ];

  const { id } = useParams<{ id: string }>();
  const numericId = id ? parseInt(id, 10) : undefined;
  const [, setCurrentPageTitle] = useDashboardPageTitle();
  const navigate = useNavigate();

  useEffect(() => {
    setCurrentPageTitle('Test > Add question exercise');
  }, []);

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      title: '',
      description: '',
      difficultyId: difficulties[0].id,
      isMultipleAnswers: false,
      answers: [
        { text: '', isCorrect: false },
        { text: '', isCorrect: false },
        { text: '', isCorrect: false },
        { text: '', isCorrect: false },
      ],
    },
  });

  const onSubmit = (values: z.infer<typeof formSchema>) => {
    const selectedDifficulty = difficulties.find((d) => d.id === values.difficultyId);

    const correctAnswersCount = values.answers.reduce((count, answer) => count + (answer.isCorrect ? 1 : 0), 0);

    // Set isMultipleAnswers to true if more than one answer is marked as correct
    const isMultipleAnswers = correctAnswersCount > 1;

    const formattedData = {
      title: values.title,
      description: values.description,
      difficulty: selectedDifficulty!.name,
      isMultipleAnswers: isMultipleAnswers,
      answers: values.answers.map((answer) => ({
        text: answer.text,
        isCorrect: answer.isCorrect,
      })),
    };

    if (numericId !== undefined) {
      agent.Exercises.createQuestion(numericId, formattedData)
        .then(() => {
          navigate(`/dashboard/tests/${id}`);
        })
        .catch((error) => {
          toast({
            title: 'Error adding question',
            description: error.message || 'An unexpected error occurred.',
            variant: 'destructive',
          });
        });
    }
  };

  const { fields, append, remove } = useFieldArray({
    control: form.control,
    name: 'answers', // The key of the field array in your form
  });

  // Function to handle adding a new answer
  const addAnswer = () => {
    append({ text: '', isCorrect: false });
  };

  return (
    <>
      <Form {...form}>
        <form onSubmit={form.handleSubmit(onSubmit)} className="m-auto mb-12 min-w-[320px] max-w-[500px] space-y-8">
          <TypographyH3>Add Question Exercise</TypographyH3>
          <FormField
            control={form.control}
            name="title"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Title</FormLabel>
                <FormControl>
                  <Input placeholder="Title" {...field} maxLength={100} className="bg-white" />
                </FormControl>
                <FormDescription>Short title of the question</FormDescription>
                <FormMessage />
              </FormItem>
            )}
          />

          <FormField
            control={form.control}
            name="description"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Question</FormLabel>
                <FormControl>
                  <Textarea placeholder="Description" {...field} maxLength={1000} className="bg-white" />
                </FormControl>
                <FormDescription>Question up to 1000 characters long</FormDescription>
                <FormMessage />
              </FormItem>
            )}
          />

          <FormField
            control={form.control}
            name="difficultyId"
            render={({ field }) => (
              <FormItem>
                <div className="flex flex-col">
                  <FormLabel className="pb-3">Difficulty</FormLabel>
                  <FormControl>
                    <Popover>
                      <PopoverTrigger asChild>
                        <FormControl>
                          <Button
                            variant="outline"
                            role="combobox"
                            className={cn('justify-between font-normal', !field.value && 'text-muted-foreground')}
                          >
                            {field.value
                              ? difficulties.find((d) => d.id === field.value)?.name || 'Select difficulty'
                              : 'Select difficulty'}
                            <CaretSortIcon className="ml-2 h-4 w-4 shrink-0 opacity-50" />
                          </Button>
                        </FormControl>
                      </PopoverTrigger>

                      <PopoverContent className="p-0">
                        <Command>
                          <CommandEmpty>Nothing found.</CommandEmpty>
                          <CommandList>
                            <CommandGroup>
                              {difficulties.map((dif) => (
                                <CommandItem
                                  value={dif.name}
                                  key={dif.id}
                                  onSelect={() => {
                                    form.setValue('difficultyId', dif.id);
                                  }}
                                >
                                  {dif.name}
                                  <CheckIcon
                                    className={cn(
                                      'ml-auto h-4 w-4',
                                      dif.id === field.value ? 'opacity-100' : 'opacity-0',
                                    )}
                                  />
                                </CommandItem>
                              ))}
                            </CommandGroup>
                          </CommandList>
                        </Command>
                      </PopoverContent>
                    </Popover>
                  </FormControl>
                </div>
                <FormDescription>Relative difficulty of the question</FormDescription>
                <FormMessage />
              </FormItem>
            )}
          />

          <>
            {fields.map((item, index) => (
              <Card key={item.id} className="space-y-4 p-6 shadow-sm">
                <FormField
                  control={form.control}
                  name={`answers.${index}.text`}
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Answer {index + 1}</FormLabel>
                      <FormControl>
                        <Textarea {...field} placeholder="Text" />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
                <div className="flex justify-between gap-1">
                  <FormField
                    control={form.control}
                    name={`answers.${index}.isCorrect`}
                    render={({ field }) => (
                      <FormItem className="flex items-center">
                        <div className="flex-grow">
                          <FormLabel className="mr-2">Is correct?</FormLabel>
                        </div>
                        <div className="flex-shrink">
                          <FormControl>
                            <Switch checked={field.value} onCheckedChange={(val) => field.onChange(val)} />
                          </FormControl>
                        </div>
                      </FormItem>
                    )}
                  />

                  <Button
                    variant="outline"
                    size="icon"
                    onClick={() => remove(index)}
                    className="self-center"
                    disabled={fields.length <= 2}
                  >
                    <TrashIcon className="h-4 w-4" />
                  </Button>
                </div>
              </Card>
            ))}

            <Button
              variant="secondary"
              type="button"
              onClick={addAnswer}
              disabled={fields.length >= 10}
              className="mt-4"
            >
              Add answer
            </Button>
          </>

          <div className="grid grid-cols-1 justify-between gap-4 sm:flex sm:grid-cols-2">
            <Button type="submit" className="w-full sm:w-52">
              Submit
            </Button>
            <Button className="w-full sm:w-52" variant="outline" onClick={() => navigate(`/dashboard/tests/${id}`)}>
              Cancel
            </Button>
          </div>
        </form>
      </Form>
    </>
  );
}
