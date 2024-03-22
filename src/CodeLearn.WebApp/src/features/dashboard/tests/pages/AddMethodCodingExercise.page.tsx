import { useEffect } from 'react';
import { useDashboardPageTitle } from '@/components/layout';
import { TypographyH3 } from '@/components/typography/typography-h3.tsx';

function AddMethodCodingExercisePage() {
  const [, setCurrentPageTitle] = useDashboardPageTitle();

  useEffect(() => {
    setCurrentPageTitle('Test > Add method coding exercise');
  }, []);

  return <TypographyH3>Create Exercise</TypographyH3>;
}

export default AddMethodCodingExercisePage;
