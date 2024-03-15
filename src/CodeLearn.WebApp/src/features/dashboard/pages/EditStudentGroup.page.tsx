import DashboardPageContainer from '@/features/dashboard/components/DashboardPageContainer.tsx';
import { useDashboardPageTitle } from '@/components/layout';
import { useEffect } from 'react';

export default function EditStudentGroupPage() {
  const [, setCurrentPageTitle] = useDashboardPageTitle();

  useEffect(() => {
    setCurrentPageTitle('Edit Student Group');
  }, []);

  return (
    <>
      <DashboardPageContainer>Edit</DashboardPageContainer>
    </>
  );
}
