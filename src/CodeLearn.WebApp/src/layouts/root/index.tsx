import { Outlet } from 'react-router-dom';

export default function RootLayout() {
  return (
    <>
      <div className="text-7xl text-amber-700">Header</div>
      <Outlet />
      <div className="text-7xl text-amber-700">Footer</div>
    </>
  );
}
