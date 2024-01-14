import { useEffect } from 'react';
import { useDashboardPageTitle } from '@/components/dashboard-layout';

function TestingResultsPage() {
  const [, setCurrentPageTitle] = useDashboardPageTitle();

  useEffect(() => {
    setCurrentPageTitle('Testing results');
  }, []);

  return <div>TestingResultsPage</div>;
}

export default TestingResultsPage;
