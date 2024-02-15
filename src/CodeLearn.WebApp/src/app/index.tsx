import { Outlet, ScrollRestoration } from 'react-router-dom';
import useDebugRender from 'tilg';
import { ToastProvider } from '@/components/ui/toast.tsx';
import { Toaster } from '@/components/ui/toaster.tsx';

function App() {
  useDebugRender();

  return (
    <ToastProvider>
      <Outlet />
      <Toaster />
      <ScrollRestoration />
    </ToastProvider>
  );
}

export default App;
