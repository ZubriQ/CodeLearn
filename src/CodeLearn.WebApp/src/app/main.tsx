import { createRoot } from 'react-dom/client';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import '@/styles/tailwind.css';
import '@/styles/main.css';
import App from '@/app';
import routes from '@/app/routes.tsx';
import Error500Page from '@/features/errors/pages/500.page.tsx';
import Loading from '@/components/loading';

const container = document.getElementById('root') as HTMLElement;

const root = createRoot(container);

const router = createBrowserRouter([
  {
    path: '/',
    element: <App />,
    children: routes,
    errorElement: <Error500Page />,
  },
]);

root.render(<RouterProvider router={router} fallbackElement={<Loading />} />);
