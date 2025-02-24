import { Outlet } from 'react-router-dom';
import { Toaster } from '@/components/ui/toaster.tsx';

function App() {
  return (
    <>
      <Outlet />
      <Toaster />
    </>
  );
}

export default App;
