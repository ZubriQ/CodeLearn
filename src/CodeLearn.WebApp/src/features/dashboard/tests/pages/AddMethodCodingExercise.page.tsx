import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { useFieldArray, useForm } from 'react-hook-form';
import { z } from 'zod';
import { zodResolver } from '@hookform/resolvers/zod';
import { TrashIcon, PlusIcon, MinusIcon } from '@heroicons/react/24/outline';
import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from '@/components/ui/form.tsx';
import agent from '@/api/agent.ts';
import { useDashboardPageTitle } from '@/components/layout';
import { TypographyH3 } from '@/components/typography/typography-h3.tsx';
import { Input } from '@/components/ui/input.tsx';
import { Textarea } from '@/components/ui/textarea.tsx';
import { Button } from '@/components/ui/button.tsx';
import { Card } from '@/components/ui/card.tsx';
import { toast } from '@/components/ui/use-toast.ts';
import { difficulties } from '@/features/dashboard/tests/pages/Difficulties.ts';
import { DataType } from '@/features/dashboard/tests/models/DataType.ts';
import { ExerciseTopic } from '@/features/dashboard/tests/models/ExerciseTopic.ts';
import { Checkbox } from '@/components/ui/checkbox.tsx';
import { decorations } from '@/features/dashboard/tests/pages/Decorations.ts';
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
  entry: z.string().min(1),
  decoration: z.any(), // actually a string that works as id
});

const testCaseParameterSchema = z.object({
  value: z.string().min(1),
  position: z.number(),
});

const testCaseSchema = z.object({
  correctOutputValue: z.string(),
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
      methodReturnDataTypeId: undefined,
      exerciseNotes: [],
      inputOutputExamples: [],
      methodParameters: [],
      testCases: [],
    },
  });

  const onSubmit = (values: z.infer<typeof formSchema>) => {
    const selectedDifficulty = difficulties.find((d) => d.id === values.difficultyId);
    const processedNotes = values.exerciseNotes.map((note) => ({
      ...note,
      decoration: note.decoration.toString(),
    }));
    const formattedData = {
      title: values.title,
      description: values.description,
      difficulty: selectedDifficulty!.name,
      exerciseTopics: values.exerciseTopics,
      methodToExecute: values.methodToExecute,
      methodSolutionCode: values.methodSolutionCode,
      methodReturnDataTypeId: values.methodReturnDataTypeId,
      exerciseNotes: processedNotes,
      inputOutputExamples: values.inputOutputExamples,
      methodParameters: values.methodParameters,
      testCases: values.testCases,
    };

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

  const { control, watch, setValue } = form;

  const {
    fields: inputOutputExamplesFields,
    append: appendInputOutputExample,
    remove: removeInputOutputExample,
  } = useFieldArray({
    control: form.control,
    name: 'inputOutputExamples',
  });

  const {
    fields: methodParametersFields,
    append: appendMethodParameter,
    remove: removeMethodParameter,
  } = useFieldArray({
    control: form.control,
    name: 'methodParameters',
  });

  const addInputOutputExample = () => {
    appendInputOutputExample({ input: '', output: '' });
  };

  // Watching the length of methodParameters to trigger updates
  const methodParametersLength = watch('methodParameters')?.length || 0;

  const testCasesArray = useFieldArray({
    control,
    name: 'testCases',
  });

  const {
    fields: exerciseNotesFields,
    append: appendExerciseNote,
    remove: removeExerciseNote,
  } = useFieldArray({
    control: form.control,
    name: 'exerciseNotes',
  });

  // For updating test cases according method parameters count
  useEffect(() => {
    const updatedTestCases = testCasesArray.fields.map((testCase) => {
      const updatedParams = testCase.testCaseParameters || [];
      while (updatedParams.length < methodParametersLength) {
        updatedParams.push({ value: '', position: updatedParams.length });
      }
      updatedParams.length = methodParametersLength;
      return { ...testCase, testCaseParameters: updatedParams };
    });

    setValue('testCases', updatedTestCases);
  }, [methodParametersLength]);

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
                <FormDescription>Description of the problem to solve</FormDescription>
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

          <div>
            <FormLabel>Input-Output Examples</FormLabel>
            {inputOutputExamplesFields.map((item, index) => (
              <Card key={item.id} className="mt-4 flex flex-col space-y-3 bg-transparent p-5 shadow-sm">
                <div className="flex items-center justify-between">
                  <Button
                    size="icon"
                    variant="outline"
                    disabled={true}
                    className="bg-transparent shadow-none disabled:opacity-100"
                  >
                    {index + 1}
                  </Button>
                  <Button
                    variant="outline"
                    size="icon"
                    onClick={() => removeInputOutputExample(index)}
                    aria-label={`Delete Input Output Example ${index + 1}`}
                  >
                    <TrashIcon className="h-4 w-4" />
                  </Button>
                </div>
                <div className="grid grid-cols-1 gap-4 md:grid-cols-2">
                  <FormField
                    control={form.control}
                    name={`inputOutputExamples.${index}.input`}
                    render={({ field }) => (
                      <FormItem>
                        <FormLabel>Input {index + 1}</FormLabel>
                        <FormControl>
                          <Input {...field} placeholder="Text" className="w-full bg-white" />
                        </FormControl>
                        <FormMessage />
                      </FormItem>
                    )}
                  />
                  <div>
                    <FormField
                      control={form.control}
                      name={`inputOutputExamples.${index}.output`}
                      render={({ field }) => (
                        <FormItem>
                          <FormLabel>Output {index + 1}</FormLabel>
                          <FormControl>
                            <Input {...field} placeholder="Text" className="w-full bg-white" />
                          </FormControl>
                          <FormMessage />
                        </FormItem>
                      )}
                    />
                  </div>
                </div>
              </Card>
            ))}

            {inputOutputExamplesFields.length >= 4 || (
              <Card className="mt-4 flex items-center justify-center border-dashed bg-transparent px-4 py-10">
                <Button type="button" onClick={addInputOutputExample} className="w-fit content-center">
                  <PlusIcon className="mr-2 size-4" /> Add input-output example
                </Button>
              </Card>
            )}

            <FormDescription className="mt-2">
              Show what parameters are passed into the method and what the output should be
            </FormDescription>
          </div>

          <div>
            <FormLabel>Exercise Notes</FormLabel>
            {exerciseNotesFields.map((note, index) => (
              <Card key={note.id} className="mt-4 flex flex-col space-y-3 bg-transparent p-5 shadow-sm">
                <div className="flex items-center justify-between">
                  <Button
                    size="icon"
                    variant="outline"
                    disabled={true}
                    className="bg-transparent shadow-none disabled:opacity-100"
                  >
                    {index + 1}
                  </Button>
                  <Button
                    variant="outline"
                    size="icon"
                    onClick={() => removeExerciseNote(index)}
                    aria-label={`Delete Note ${index + 1}`}
                  >
                    <TrashIcon className="h-4 w-4" />
                  </Button>
                </div>
                <FormField
                  control={form.control}
                  name={`exerciseNotes.${index}.entry`}
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Note Text</FormLabel>
                      <FormControl>
                        <Input {...field} placeholder="Note text" className="w-full bg-white" />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
                <FormField
                  control={form.control}
                  name={`exerciseNotes.${index}.decoration`}
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Decoration</FormLabel>
                      <FormControl>
                        <Combobox
                          {...field}
                          options={decorations.map((decoration) => ({
                            id: decoration.id,
                            alias: decoration.name,
                          }))}
                          placeholder="Select decoration"
                          className="w-full bg-white"
                        />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
              </Card>
            ))}

            {exerciseNotesFields.length >= 4 || (
              <Card className="mt-4 flex items-center justify-center border-dashed bg-transparent px-4 py-10">
                <Button type="button" onClick={() => appendExerciseNote({ entry: '', decoration: decorations[0].id })}>
                  <PlusIcon className="mr-2 size-4" />
                  Add note
                </Button>
              </Card>
            )}

            <FormDescription className="mt-2">Comments or restrictions</FormDescription>
          </div>

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
            name="methodParameters"
            render={() => (
              <div>
                <FormLabel>Method Parameters</FormLabel>
                <div className="mb-4 mt-4 flex space-x-2">
                  <Button
                    className="w-full space-x-2 sm:w-fit"
                    type="button"
                    disabled={methodParametersFields.length >= 5}
                    onClick={() =>
                      appendMethodParameter({
                        dataTypeId: dataTypes?.[0]?.id ?? 0,
                        position: methodParametersFields.length + 1,
                      })
                    }
                  >
                    <PlusIcon className="mr-2 size-4" /> Add parameter
                  </Button>
                  <Button
                    type="button"
                    variant="destructive"
                    disabled={methodParametersFields.length === 0}
                    onClick={() => removeMethodParameter(methodParametersFields.length - 1)}
                    className="w-full space-x-2 sm:w-fit"
                  >
                    <MinusIcon className="mr-2 size-4" /> Remove last
                  </Button>
                </div>

                {methodParametersFields.length > 0 && (
                  <Card className="flex w-full flex-row flex-wrap bg-transparent pl-4 pt-4 shadow-sm">
                    {methodParametersFields.map((item, index) => (
                      <div className="flex w-fit flex-col" key={index}>
                        <FormLabel className="mb-2">Param {index + 1}</FormLabel>
                        <Combobox
                          key={item.id}
                          name={`methodParameters.${index}.dataTypeId`}
                          options={dataTypes ?? []}
                          placeholder="Select data type"
                          className="mb-4 mr-4 flex-1"
                        />
                      </div>
                    ))}
                  </Card>
                )}

                {methodParametersFields.length < 1 && (
                  <Card className="mt-4 flex items-center justify-center border-dashed bg-transparent px-4 py-8">
                    <FormDescription> No method parameters added yet </FormDescription>
                  </Card>
                )}

                <FormDescription className="mt-2">You must add at least one parameter</FormDescription>
              </div>
            )}
          />

          <FormField
            control={form.control}
            name="testCases"
            render={() => (
              <FormItem>
                <div>
                  <FormLabel>Test Cases</FormLabel>
                  <FormControl>
                    <>
                      {testCasesArray.fields.map((testCase, index) => (
                        <Card key={testCase.id} className="mt-4 flex flex-col space-y-3 bg-transparent p-4 shadow-sm">
                          <div className="flex items-center justify-between">
                            <Button
                              size="icon"
                              variant="outline"
                              disabled={true}
                              className="bg-transparent shadow-none disabled:opacity-100"
                            >
                              {index + 1}
                            </Button>
                            <Button
                              type="button"
                              size="icon"
                              variant="outline"
                              onClick={() => testCasesArray.remove(index)}
                              aria-label={`Delete Test Case ${index + 1}`}
                            >
                              <TrashIcon className="h-4 w-4" />
                            </Button>
                          </div>
                          <div className="grid grid-cols-1 gap-4 sm:grid-cols-2 md:grid-cols-3">
                            <div className="flex flex-col">
                              <FormLabel htmlFor={`correctOutput-${index}`} className="mb-2 font-medium">
                                Correct Output
                              </FormLabel>
                              <Input
                                id={`correctOutput-${index}`}
                                {...form.register(`testCases.${index}.correctOutputValue`)}
                                placeholder="Value"
                                className="bg-white"
                              />
                            </div>
                            {testCase.testCaseParameters.map((_, paramIndex) => (
                              <div className="flex flex-col" key={`${index}-${paramIndex}`}>
                                <FormLabel
                                  htmlFor={`param-${index}-${paramIndex}`}
                                  className="mb-2"
                                >{`Param ${paramIndex + 1}`}</FormLabel>
                                <Input
                                  id={`param-${index}-${paramIndex}`}
                                  {...form.register(`testCases.${index}.testCaseParameters.${paramIndex}.value`)}
                                  placeholder={`Value`}
                                  className="bg-white"
                                />
                              </div>
                            ))}
                          </div>
                        </Card>
                      ))}
                    </>
                  </FormControl>
                  {testCasesArray.fields.length >= 8 || (
                    <Card className="mt-4 flex items-center justify-center border-dashed bg-transparent px-4 py-10">
                      <Button
                        className="mb-4 mt-4 flex w-full space-x-2 sm:w-fit"
                        type="button"
                        onClick={() =>
                          testCasesArray.append({
                            correctOutputValue: '',
                            testCaseParameters: Array(methodParametersLength)
                              .fill(null)
                              .map((_, idx) => ({
                                value: '',
                                position: idx,
                              })),
                          })
                        }
                      >
                        <PlusIcon className="mr-2 size-4" /> Add test case
                      </Button>
                    </Card>
                  )}
                  <FormDescription className="mt-2">
                    Create a few test cases to ensure students write correct code
                  </FormDescription>
                  <FormMessage />
                </div>
              </FormItem>
            )}
          />

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

export default AddMethodCodingExercisePage;
