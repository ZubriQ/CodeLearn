import DashboardHeader from '@/components/dashboard-header';

function TestsPage() {
  return (
    <>
      <DashboardHeader title="Tests" />

      <div className="flex-1 flex-grow overflow-auto">
        <div className="px-5 py-4">
          <div>TestsPage</div>
        </div>
      </div>
    </>
  );
}

export default TestsPage;
