import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import './styles/tailwind.css';
import routes from './routes.tsx';
import App from './App.tsx';
import Error from './pages/error';

const container = document.getElementById('root') as HTMLElement;

const root = createRoot(container);

const router = createBrowserRouter([
  {
    path: '/',
    element: <App />,
    children: routes,
    errorElement: <Error />,
  },
]);

root.render(
  <StrictMode>
    <RouterProvider router={router} fallbackElement={<div>loading...</div>} />
  </StrictMode>,
);
