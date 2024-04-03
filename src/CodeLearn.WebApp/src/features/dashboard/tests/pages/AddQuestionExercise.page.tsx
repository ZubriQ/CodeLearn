import { useEffect, useState } from 'react';
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
import { Popover, PopoverContent, PopoverTrigger } from '@/components/ui/popover';
import { Command, CommandEmpty, CommandGroup, CommandItem, CommandList } from '@/components/ui/command';
import { Card } from '@/components/ui/card.tsx';
import { PlusIcon, TrashIcon } from '@heroicons/react/24/outline';
import { difficulties } from '@/features/dashboard/tests/pages/Difficulties.ts';
import { ExerciseTopic } from '@/features/dashboard/tests/models/ExerciseTopic.ts';
import { Checkbox } from '@/components/ui/checkbox.tsx';

const answerSchema = z.object({
  text: z.string().min(1, 'Answer text cannot be empty'),
  isCorrect: z.boolean(),
});

// Input Validation Schema
const formSchema = z.object({
  title: z.string().min(3).max(100),
  description: z.string().min(10).max(1000),
  difficultyId: z.number(),
  answers: z.array(answerSchema),
  exerciseTopics: z.array(z.number()).nonempty('You have to select at least one exercise topic.'),
});

export default function AddQuestionExercisePage() {
  const { id } = useParams<{ id: string }>();
  const numericId = id ? parseInt(id, 10) : undefined;
  const [, setCurrentPageTitle] = useDashboardPageTitle();
  const navigate = useNavigate();
  const [exerciseTopics, setExerciseTopics] = useState<ExerciseTopic[] | undefined>(undefined);

  console.log(exerciseTopics);
  useEffect(() => {
    setCurrentPageTitle('Test > Add question exercise');

    agent.ExerciseTopics.list()
      .then((exerciseTopics) => {
        setExerciseTopics(exerciseTopics);
      })
      .catch((error) => {
        toast({
          title: 'Error fetching exercise topics',
          description: error.message || 'An unexpected error occurred.',
          variant: 'destructive',
        });
      });
  }, []);

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      title: '',
      description: '',
      difficultyId: difficulties[0].id,
      answers: [
        { text: '', isCorrect: false },
        { text: '', isCorrect: false },
        { text: '', isCorrect: false },
        { text: '', isCorrect: false },
      ],
      exerciseTopics: [],
    },
  });

  const onSubmit = (values: z.infer<typeof formSchema>) => {
    const selectedDifficulty = difficulties.find((d) => d.id === values.difficultyId);
    const formattedData = {
      title: values.title,
      description: values.description,
      difficulty: selectedDifficulty!.name,
      answers: values.answers.map((answer) => ({
        text: answer.text,
        isCorrect: answer.isCorrect,
      })),
      exerciseTopics: values.exerciseTopics,
    };

    if (numericId !== undefined) {
      agent.Exercises.createQuestion(numericId, formattedData)
        .then(() => {
          navigate(`/dashboard/tests/${id}`);
        })
        .catch((error) => {
          toast({
            title: 'Error adding exercise',
            description: error.message || 'An unexpected error occurred.',
            variant: 'destructive',
          });
        });
    }
  };

  const { fields, append, remove } = useFieldArray({
    control: form.control,
    name: 'answers',
  });

  const addAnswer = () => {
    append({ text: '', isCorrect: false });
  };

  return (
    <>
      <Form {...form}>
        <form onSubmit={form.handleSubmit(onSubmit)} className="m-auto mb-12 min-w-[320px] max-w-2xl space-y-8">
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
                  <Textarea placeholder="Text" {...field} maxLength={1000} className="bg-white" />
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
                <FormDescription>Relative exercise difficulty</FormDescription>
                <FormMessage />
              </FormItem>
            )}
          />

          {exerciseTopics && (
            <FormField
              control={form.control}
              name="exerciseTopics"
              render={() => (
                <FormItem>
                  <FormLabel>Exercise Topics</FormLabel>
                  {exerciseTopics.map((topic) => (
                    <FormField
                      key={topic.id}
                      control={form.control}
                      name="exerciseTopics"
                      render={({ field }) => (
                        <FormItem className="ml-2 flex items-center space-x-3">
                          <div>
                            <FormControl>
                              <Checkbox
                                className="mr-2 align-middle"
                                checked={field.value?.includes(topic.id)}
                                onCheckedChange={(checked) => {
                                  const newValue = checked
                                    ? [...field.value, topic.id]
                                    : field.value.filter((value) => value !== topic.id);
                                  field.onChange(newValue);
                                }}
                              />
                            </FormControl>
                            <FormLabel>{topic.name}</FormLabel>
                          </div>
                        </FormItem>
                      )}
                    />
                  ))}
                  <FormDescription>Select at least one topic that fits best</FormDescription>
                  <FormMessage />
                </FormItem>
              )}
            />
          )}

          <div className="space-y-3">
            <FormLabel>Answers</FormLabel>
            {fields.map((item, index) => (
              <Card key={item.id} className="space-y-4 bg-transparent p-5 shadow-sm">
                <FormField
                  control={form.control}
                  name={`answers.${index}.text`}
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Answer {index + 1}</FormLabel>
                      <FormControl>
                        <Textarea {...field} placeholder="Text" className="bg-white" />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
                <div className="flex justify-between">
                  <FormField
                    control={form.control}
                    name={`answers.${index}.isCorrect`}
                    render={({ field }) => (
                      <FormItem className="flex items-center">
                        <div className="flex">
                          <FormControl>
                            <Checkbox
                              checked={field.value}
                              onCheckedChange={(val) => field.onChange(val)}
                              className="mr-2"
                            />
                          </FormControl>
                          <FormLabel>Is correct?</FormLabel>
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

            <Card className="flex items-center justify-center border-dashed bg-transparent px-4 py-12">
              <Button type="button" onClick={addAnswer} disabled={fields.length >= 10} className="w-fit content-center">
                <PlusIcon className="mr-2 size-4" /> Add answer
              </Button>
            </Card>
          </div>

          <div className="grid grid-cols-1 justify-between gap-4 sm:flex sm:grid-cols-2">
            <Button type="submit" className="w-full sm:w-32">
              Submit
            </Button>
            <Button className="w-full sm:w-32" variant="outline" onClick={() => navigate(`/dashboard/tests/${id}`)}>
              Cancel
            </Button>
          </div>
        </form>
      </Form>
    </>
  );
}
