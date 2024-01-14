import { useEffect } from 'react';
import { useDashboardPageTitle } from '@/components/dashboard-layout';

function StudentsPage() {
  const [, setCurrentPageTitle] = useDashboardPageTitle();

  useEffect(() => {
    setCurrentPageTitle('Students');
  }, []);

  return <div>StudentsPage</div>;
}

export default StudentsPage;
