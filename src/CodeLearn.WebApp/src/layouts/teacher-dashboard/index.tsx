import { Outlet } from 'react-router-dom';

export default function TeacherDashboardLayout() {
  return (
    <>
      <div className="text-7xl text-amber-700">Header</div>
      <div className="text-7xl text-amber-700">Left Navigation Bar</div>
      <Outlet />
    </>
  );
}
