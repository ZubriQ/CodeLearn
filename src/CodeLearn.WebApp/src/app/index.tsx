import { Outlet, ScrollRestoration } from 'react-router-dom';
import useDebugRender from 'tilg';
import { Toaster } from '@/components/ui/toaster.tsx';

function App() {
  useDebugRender();

  return (
    <>
      <Outlet />
      <Toaster />
      <ScrollRestoration />
    </>
  );
}

export default App;
