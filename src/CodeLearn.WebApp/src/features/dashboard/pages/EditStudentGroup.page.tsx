import { useEffect, useState } from 'react';
import { useForm } from 'react-hook-form';
import { useNavigate, useParams } from 'react-router-dom';
import * as z from 'zod';
import { zodResolver } from '@hookform/resolvers/zod';
import DashboardPageContainer from '@/features/dashboard/components/DashboardPageContainer.tsx';
import { useDashboardPageTitle } from '@/components/layout';
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from '@/components/ui/form';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { StudentGroup } from '@/features/dashboard/models/StudentGroup.ts';
import { toast } from '@/components/ui/use-toast.ts';
import agent from '@/api/agent';

// Input Validation Schema
const formSchema = z.object({
  name: z.string().min(1).max(50),
  enrolmentYear: z.number().int().min(2020).max(2100),
});

export default function EditStudentGroupPage() {
  const [, setCurrentPageTitle] = useDashboardPageTitle();
  const [studentGroup, setStudentGroup] = useState<StudentGroup>();
  const { id } = useParams<{ id: string }>();
  const navigateTo = useNavigate();

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      name: '',
      enrolmentYear: 2020,
    },
  });

  useEffect(() => {
    setCurrentPageTitle('Edit Student Group');

    const fetchStudentGroup = () => {
      const idNumber = parseInt(id ?? '0', 10);
      agent.StudentGroup.getById(idNumber)
        .then((response) => {
          setStudentGroup(response);

          form.reset({
            name: response.name,
            enrolmentYear: response.enrolmentYear,
          });
        })
        .catch((error) => {
          toast({
            title: 'Error fetching student group',
            description: error.message || 'An unexpected error occurred.',
            variant: 'destructive',
          });
        });
    };

    fetchStudentGroup();
  }, [id]);

  if (!studentGroup) {
    return <div>Student group not found</div>;
  }

  const onSubmit = (values: z.infer<typeof formSchema>) => {
    const formattedData = {
      ...values,
    };
    agent.StudentGroup.update(studentGroup.id, formattedData)
      .then(() => {
        navigateTo('/dashboard/student-groups/');
      })
      .catch((error) => {
        toast({
          title: 'Error updating the student group',
          description: error.message || 'An unexpected error occurred.',
          variant: 'destructive',
        });
      });
  };

  return (
    <>
      <DashboardPageContainer>
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)} className="m-auto max-w-96 space-y-8">
            <FormField
              control={form.control}
              name="name"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Student group name</FormLabel>
                  <FormControl>
                    <Input placeholder="Name" {...field} maxLength={50} />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="enrolmentYear"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Enrolment year</FormLabel>
                  <FormControl>
                    <Input
                      placeholder="Year"
                      type="number"
                      min={2020}
                      max={2100}
                      {...field}
                      onChange={(e) => field.onChange(parseInt(e.target.value, 10))}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />

            <div className="grid grid-cols-1 gap-4 sm:grid-cols-2">
              <Button type="submit" className="w-full">
                Save changes
              </Button>
              <Button className="w-full" variant="outline" onClick={() => navigateTo('/dashboard/student-groups/')}>
                Cancel
              </Button>
            </div>
          </form>
        </Form>
      </DashboardPageContainer>
    </>
  );
}
