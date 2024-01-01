import { Outlet } from 'react-router-dom';

export default function StudentTestingLayout() {
  return (
    <>
      <div className="text-7xl text-amber-700">StudentTestingLayout</div>
      <Outlet />
    </>
  );
}
