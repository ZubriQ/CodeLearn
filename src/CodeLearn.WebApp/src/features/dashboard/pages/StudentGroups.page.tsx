import { useEffect } from 'react';
import { useDashboardPageTitle } from '@/components/layout';

function StudentGroupsPage() {
  const [, setCurrentPageTitle] = useDashboardPageTitle();

  useEffect(() => {
    setCurrentPageTitle('Student groups');
  }, []);

  return <div>StudentGroupsPage</div>;
}

export default StudentGroupsPage;
