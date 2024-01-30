import { useEffect } from 'react';
import { useDashboardPageTitle } from '@/components/layout';

function StudentsPage() {
  const [, setCurrentPageTitle] = useDashboardPageTitle();

  useEffect(() => {
    setCurrentPageTitle('Students');
  }, []);

  return <div>StudentsPage</div>;
}

export default StudentsPage;
