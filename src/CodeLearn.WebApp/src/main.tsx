import './styles/tailwind.css';
import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import routes from './routes.tsx';
import App from './App.tsx';

const container = document.getElementById('root') as HTMLElement;

const root = createRoot(container);

const router = createBrowserRouter([
  {
    path: '/',
    element: <App />,
    children: routes,
    errorElement: <div>error</div>,
  },
]);

root.render(
  <StrictMode>
    <RouterProvider router={router} fallbackElement={<div>loading...</div>} />
  </StrictMode>,
);
