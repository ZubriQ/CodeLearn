import { useEffect } from 'react';
import { useDashboardPageTitle } from '@/components/dashboard-layout';

function StudentTestsPage() {
  const [, setCurrentPageTitle] = useDashboardPageTitle();

  useEffect(() => {
    setCurrentPageTitle('Tests');
  }, []);

  return <div>StudentsTestsPage</div>;
}

export default StudentTestsPage;
