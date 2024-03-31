import { useEffect } from 'react';
import { useDashboardPageTitle } from '@/components/layout';
import { TypographyH3 } from '@/components/typography/typography-h3.tsx';
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
import { Textarea } from '@/components/ui/textarea.tsx';
import { Popover, PopoverContent, PopoverTrigger } from '@/components/ui/popover.tsx';
import { Button } from '@/components/ui/button.tsx';
import { cn } from '@/lib/utils.ts';
import { CaretSortIcon, CheckIcon } from '@radix-ui/react-icons';
import { Command, CommandEmpty, CommandGroup, CommandItem, CommandList } from '@/components/ui/command.tsx';
import { Card } from '@/components/ui/card.tsx';
import { Switch } from '@/components/ui/switch.tsx';
import { TrashIcon } from '@heroicons/react/24/outline';
import { useNavigate, useParams } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import { z } from 'zod';
import { zodResolver } from '@hookform/resolvers/zod';
import agent from '@/api/agent.ts';
import { toast } from '@/components/ui/use-toast.ts';
import { difficulties } from '@/features/dashboard/tests/pages/Difficulties.ts';

const methodParameterSchema = z.object({
  dataTypeId: z.number(),
  position: z.number(),
});

const inputOutputExampleSchema = z.object({
  input: z.string().min(1),
  output: z.string().min(1),
});

const exerciseNoteSchema = z.object({
  dataTypeId: z.number().min(1),
  position: z.number().min(1),
});

const testCaseParameterSchema = z.object({
  value: z.string().min(1),
  position: z.number(),
});

const testCaseSchema = z.object({
  correctOutputValue: z.number(),
  testCaseParameters: z.array(testCaseParameterSchema),
});

// Input Validation Schema
const formSchema = z.object({
  title: z.string().min(3).max(100),
  description: z.string().min(10).max(1000),
  difficultyId: z.number(),
  exerciseTopics: z.array(z.number()),
  methodToExecute: z.string().min(1),
  methodSolutionCode: z.string().max(1000),
  methodReturnDataTypeId: z.number(),
  exerciseNotes: z.array(exerciseNoteSchema),
  inputOutputExamples: z.array(inputOutputExampleSchema),
  methodParameters: z.array(methodParameterSchema),
  testCases: z.array(testCaseSchema),
});

function AddMethodCodingExercisePage() {
  const { id } = useParams<{ id: string }>();
  const numericId = id ? parseInt(id, 10) : undefined;
  const [, setCurrentPageTitle] = useDashboardPageTitle();
  const navigate = useNavigate();

  useEffect(() => {
    setCurrentPageTitle('Test > Add method coding exercise');
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
    },
  });

  const onSubmit = (values: z.infer<typeof formSchema>) => {
    const selectedDifficulty = difficulties.find((d) => d.id === values.difficultyId);

    const formattedData = {};

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
                <FormDescription>Relative exercise difficulty</FormDescription>
                <FormMessage />
              </FormItem>
            )}
          />

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

export default AddMethodCodingExercisePage;
