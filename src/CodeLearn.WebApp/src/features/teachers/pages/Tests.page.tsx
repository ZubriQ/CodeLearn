import DashboardHeader from '@/components/dashboard-header';
import { Button } from '@/components/ui/button.tsx';

function TeacherTestsPage() {
  return (
    <>
      <DashboardHeader title="Tests" />

      <div className="flex-1 flex-grow overflow-auto">
        <div className="px-5 py-4">
          <div>TestsPage</div>
          <Button>Add new test</Button>
        </div>
      </div>
    </>
  );
}

export default TeacherTestsPage;
