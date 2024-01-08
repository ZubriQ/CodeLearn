import { Outlet, ScrollRestoration } from 'react-router-dom';
import useDebugRender from 'tilg';

function App() {
  useDebugRender();

  return (
    <>
      <Outlet />
      <ScrollRestoration />
    </>
  );
}

export default App;
