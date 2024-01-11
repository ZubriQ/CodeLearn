import DashboardHeader from '@/components/dashboard-header';

function StudentsPage() {
  return (
    <>
      <DashboardHeader title="Students" />

      <div className="flex-1 flex-grow overflow-auto">
        <div className="px-5 py-4">
          <div>StudentsPage</div>
        </div>
      </div>
    </>
  );
}

export default StudentsPage;
