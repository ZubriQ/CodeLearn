import { Outlet, ScrollRestoration } from 'react-router-dom';
import { Toaster } from '@/components/ui/toaster.tsx';

function App() {
  return (
    <>
      <Outlet />
      <Toaster />
      <ScrollRestoration />
    </>
  );
}

export default App;
