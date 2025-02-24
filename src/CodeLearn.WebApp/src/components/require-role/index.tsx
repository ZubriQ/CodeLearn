import { ReactNode } from 'react';
import { Navigate, useLocation } from 'react-router-dom';
import useAuthStore from '@/store/auth';

interface RequireRoleProps {
  children: ReactNode;
  allowedRoles: string[];
}

function RequireRole({ children, allowedRoles }: RequireRoleProps) {
  const role = useAuthStore((state) => state.role);
  const isAuthenticated = useAuthStore((state) => state.isAuthenticated);

  const location = useLocation();

  if (!isAuthenticated) {
    return <Navigate to="/sign-in" state={{ from: location }} replace />; // Guest
  }

  if (role && !allowedRoles.includes(role)) {
    return <Navigate to="/" replace />; //
  }

  return <>{children}</>;
}

export default RequireRole;
