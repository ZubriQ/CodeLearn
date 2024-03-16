import { useEffect } from 'react';
import { useDashboardPageTitle } from '@/components/layout';

function TestingSessionsPage() {
  const [, setCurrentPageTitle] = useDashboardPageTitle();

  useEffect(() => {
    setCurrentPageTitle('Testing Sessions');
  }, []);

  return <div>TestingResultsPage</div>;
}

export default TestingSessionsPage;
