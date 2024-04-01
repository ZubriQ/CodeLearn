import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { useFieldArray, useForm } from 'react-hook-form';
import { z } from 'zod';
import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from '@/components/ui/form.tsx';
import { useDashboardPageTitle } from '@/components/layout';
import agent from '@/api/agent.ts';
import { TypographyH3 } from '@/components/typography/typography-h3.tsx';
import { Input } from '@/components/ui/input.tsx';
import { Textarea } from '@/components/ui/textarea.tsx';
import { Button } from '@/components/ui/button.tsx';
import { Card } from '@/components/ui/card.tsx';
import { zodResolver } from '@hookform/resolvers/zod';
import { toast } from '@/components/ui/use-toast.ts';
import { difficulties } from '@/features/dashboard/tests/pages/Difficulties.ts';
import { DataType } from '@/features/dashboard/tests/models/DataType.ts';
import { ExerciseTopic } from '@/features/dashboard/tests/models/ExerciseTopic.ts';
import { Checkbox } from '@/components/ui/checkbox.tsx';
import Combobox from '@/components/ui/combobox.tsx';

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
  exerciseTopics: z.array(z.number()).nonempty('You have to select at least one exercise topic.'),
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
  const [dataTypes, setDataTypes] = useState<DataType[] | undefined>(undefined);
  const [exerciseTopics, setExerciseTopics] = useState<ExerciseTopic[] | undefined>(undefined);

  useEffect(() => {
    setCurrentPageTitle('Test > Add method coding exercise');

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

    agent.DataTypes.list()
      .then((dataTypes) => {
        setDataTypes(dataTypes);
      })
      .catch((error) => {
        toast({
          title: 'Error fetching data types',
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
      exerciseTopics: [],
      methodToExecute: 'GetArea',
      methodSolutionCode: 'public static double GetArea(double a, double b)\n{\n    // Example\n    return a * b;\n}',
    },
  });

  const onSubmit = (values: z.infer<typeof formSchema>) => {
    const selectedDifficulty = difficulties.find((d) => d.id === values.difficultyId);

    const formattedData = {};

    if (numericId !== undefined) {
      agent.Exercises.createMethodCoding(numericId, formattedData)
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

  const { control } = form;
  const { fields, append, remove } = useFieldArray({
    control,
    name: 'methodParameters',
  });

  return (
    <>
      <Form {...form}>
        <form onSubmit={form.handleSubmit(onSubmit)} className="m-auto mb-12 min-w-[320px] max-w-2xl space-y-8">
          <TypographyH3>Add Method Coding Exercise</TypographyH3>
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
                <FormLabel>Description</FormLabel>
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
            render={() => (
              <FormItem>
                <div className="flex flex-col">
                  <FormLabel className="pb-3">Difficulty</FormLabel>
                  <FormControl>
                    <Combobox
                      name="difficultyId"
                      options={difficulties.map((dif) => ({ id: dif.id, alias: dif.name }))}
                      placeholder="Select difficulty"
                    />
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
                  <div className="mb-3">
                    <FormLabel>Exercise Topics</FormLabel>
                    <FormDescription>Select at least 1 topic that fits best</FormDescription>
                  </div>
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
                  <FormMessage />
                </FormItem>
              )}
            />
          )}

          <FormField
            control={form.control}
            name="methodToExecute"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Method Name</FormLabel>
                <FormControl>
                  <Input placeholder="Enter name" {...field} maxLength={50} className="bg-white" />
                </FormControl>
                <FormDescription>Method for running test cases to check if they work correctly</FormDescription>
                <FormMessage />
              </FormItem>
            )}
          />

          <FormField
            control={form.control}
            name="methodReturnDataTypeId"
            render={() => (
              <FormItem>
                <div className="flex flex-col">
                  <FormLabel className="pb-3">Method Return Data Type</FormLabel>
                  <FormControl>
                    {dataTypes && (
                      <Combobox
                        name="methodReturnDataTypeId"
                        options={dataTypes.map((type) => ({ id: type.id, alias: type.alias }))}
                        placeholder="Select data type"
                      />
                    )}
                  </FormControl>
                </div>
                <FormDescription>Select what kind of data the method will return</FormDescription>
                <FormMessage />
              </FormItem>
            )}
          />

          <FormField
            control={form.control}
            name="methodSolutionCode"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Student's Initial Solution Code</FormLabel>
                <FormControl>
                  <Textarea placeholder="Code" {...field} maxLength={1000} className="h-32 bg-white" />
                </FormControl>
                <FormDescription>Help students by adding some initial code</FormDescription>
                <FormMessage />
              </FormItem>
            )}
          />

          <FormField
            control={form.control}
            name="methodParameters"
            render={() => (
              <div>
                <FormLabel>Method Parameters</FormLabel>
                <FormDescription>You must add at least 1 parameter</FormDescription>
                <div className="mb-4 mt-4 flex space-x-2">
                  <Button
                    type="button"
                    onClick={() =>
                      append({
                        dataTypeId: dataTypes?.[0]?.id ?? 0,
                        position: fields.length + 1,
                      })
                    }
                  >
                    Add Parameter
                  </Button>
                  {fields.length > 0 && (
                    <Button type="button" variant="destructive" onClick={() => remove(fields.length - 1)}>
                      Remove Last
                    </Button>
                  )}
                </div>

                {fields.length > 0 && (
                  <Card className="bg-transparent pl-4 pt-4">
                    {fields.map((item, index) => (
                      <Combobox
                        key={item.id}
                        name={`methodParameters.${index}.dataTypeId`}
                        options={dataTypes ?? []}
                        placeholder="Select data type"
                        className="mb-4 mr-4 flex-1"
                      />
                    ))}
                  </Card>
                )}
              </div>
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
