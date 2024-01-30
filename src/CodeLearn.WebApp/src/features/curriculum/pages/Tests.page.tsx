import { useEffect } from 'react';
import { useDashboardPageTitle } from '@/components/layout';

function StudentTestsPage() {
  const [, setCurrentPageTitle] = useDashboardPageTitle();

  useEffect(() => {
    setCurrentPageTitle('Tests');
  }, []);

  return <div>StudentsTestsPage</div>;
}

export default StudentTestsPage;
