import { useEffect, useState } from 'react';
import { Button } from '@/components/ui/button.tsx';
import { Input } from '@/components/ui/input.tsx';
import { Label } from '@/components/ui/label.tsx';
import { Textarea } from '@/components/ui/textarea.tsx';
import { Test } from '@/features/dashboard/tests/models/Test.ts';
import TestCards from '@/features/dashboard/tests/TestCards.component.tsx';
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from '@/components/ui/dialog.tsx';
import { useDashboardPageTitle } from '@/components/layout';
import agent from '@/api/agent.ts';
import { toast } from '@/components/ui/use-toast.ts';

function TeacherTestsPage() {
  const [, setCurrentPageTitle] = useDashboardPageTitle();
  const [tests, setTests] = useState<Test[]>([]);
  const [testTitle, setTestTitle] = useState('');
  const [testDescription, setTestDescription] = useState('');

  useEffect(() => {
    setCurrentPageTitle('Tests');

    agent.Tests.list()
      .then((tests) => {
        setTests(tests);
      })
      .catch((error) => {
        toast({
          title: 'Error fetching tests',
          description: error.message || 'An unexpected error occurred.',
          variant: 'destructive',
        });
      });
  }, []);

  // Add new Test
  const handleSubmit = () => {
    agent.Tests.create({ title: testTitle, description: testDescription })
      .then((newTest) => {
        setTests([...tests, newTest]);
        location.reload();
      })
      .catch((error) => {
        toast({
          title: 'Error adding test',
          description: error.message || 'An unexpected error occurred.',
          variant: 'destructive',
        });
      });
  };

  return (
    <>
      <Dialog>
        <DialogTrigger asChild>
          <Button className="mb-6">Add new test</Button>
        </DialogTrigger>
        <DialogContent className="sm:max-w-[425px]">
          <DialogHeader>
            <DialogTitle>Add new test</DialogTitle>
            <DialogDescription>
              Add the necessary information about the new test here. Click save when you're done.
            </DialogDescription>
          </DialogHeader>
          <div className="grid gap-4 py-4">
            <div className="grid grid-cols-4 items-center gap-4">
              <Label htmlFor="title" className="text-right">
                Title
              </Label>
              <Input
                id="title"
                className="col-span-3"
                value={testTitle}
                onChange={(e) => setTestTitle(e.target.value)}
              />
            </div>
            <div className="grid grid-cols-4 items-center gap-4">
              <Label htmlFor="description" className="text-right">
                Description
              </Label>
              <Textarea
                id="description"
                placeholder="Type your description here."
                className="col-span-3"
                value={testDescription}
                onChange={(e) => setTestDescription(e.target.value)}
              />
            </div>
          </div>
          <DialogFooter>
            <Button type="submit" onClick={handleSubmit}>
              Submit
            </Button>
          </DialogFooter>
        </DialogContent>
      </Dialog>

      <div className="my-4">
        <TestCards tests={tests} />
      </div>
    </>
  );
}

export default TeacherTestsPage;
