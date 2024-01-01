import { Outlet, ScrollRestoration } from 'react-router-dom';
import useDebugRender from 'tilg';

export default function App() {
  useDebugRender();

  return (
    <>
      <Outlet />
      <ScrollRestoration />
    </>
  );
}
