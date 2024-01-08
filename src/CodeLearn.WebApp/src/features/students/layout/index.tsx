import { Outlet } from 'react-router-dom';

function StudentDashboardLayout() {
  return (
    <>
      <div className="text-7xl text-amber-700">Header</div>
      <div className="text-7xl text-amber-700">Left Navigation Bar</div>
      <Outlet />
    </>
  );
}

export default StudentDashboardLayout;
