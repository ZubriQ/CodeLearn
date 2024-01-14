import DashboardHeader from '@/components/dashboard-header';
import { Button } from '@/components/ui/button.tsx';
import { Test } from '@/components/test-cards/Test.ts';
import TestCards from '@/components/test-cards';

const tests: Test[] = [
  {
    id: 1,
    teacherId: 1,
    title: 'Title1',
    description:
      'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius.',
    durationInMinutes: 50,
    createdDateTime: new Date(),
    modifiedDateTime: new Date(),
  },
  {
    id: 2,
    teacherId: 1,
    title: 'Title2',
    description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius.',
    durationInMinutes: 40,
    createdDateTime: new Date(),
    modifiedDateTime: new Date(),
  },
  {
    id: 3,
    teacherId: 1,
    title: 'Title3',
    description:
      'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius.',
    durationInMinutes: 60,
    createdDateTime: new Date(),
    modifiedDateTime: new Date(),
  },
  {
    id: 4,
    teacherId: 1,
    title: 'Title1',
    description:
      'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius.',
    durationInMinutes: 50,
    createdDateTime: new Date(),
    modifiedDateTime: new Date(),
  },
  {
    id: 5,
    teacherId: 1,
    title: 'Title2',
    description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius.',
    durationInMinutes: 40,
    createdDateTime: new Date(),
    modifiedDateTime: new Date(),
  },
  {
    id: 6,
    teacherId: 1,
    title: 'Title3',
    description:
      'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius.',
    durationInMinutes: 60,
    createdDateTime: new Date(),
    modifiedDateTime: new Date(),
  },
  {
    id: 7,
    teacherId: 1,
    title: 'Title1',
    description:
      'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius.',
    durationInMinutes: 50,
    createdDateTime: new Date(),
    modifiedDateTime: new Date(),
  },
  {
    id: 8,
    teacherId: 1,
    title: 'Title2',
    description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius.',
    durationInMinutes: 40,
    createdDateTime: new Date(),
    modifiedDateTime: new Date(),
  },
  {
    id: 9,
    teacherId: 1,
    title: 'Title3',
    description:
      'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec congue, nisl eget molestie varius.',
    durationInMinutes: 60,
    createdDateTime: new Date(),
    modifiedDateTime: new Date(),
  },
];

function TeacherTestsPage() {
  return (
    <>
      <DashboardHeader title="Tests" />

      <div className="flex-1 flex-grow overflow-auto">
        <div className="px-5 py-4">
          <div className="my-2">
            <Button>Add new test</Button>
            <div className="my-4">
              <TestCards tests={tests} />
            </div>
          </div>
        </div>
      </div>
    </>
  );
}

export default TeacherTestsPage;
