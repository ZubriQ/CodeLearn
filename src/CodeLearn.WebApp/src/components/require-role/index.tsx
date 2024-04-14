import { ReactNode } from 'react';
import { Navigate, useLocation } from 'react-router-dom';
import { useAppSelector } from '@/app/hooks.ts';

interface RequireRoleProps {
  children: ReactNode;
  allowedRoles: string[];
}

function RequireRole({ children, allowedRoles }: RequireRoleProps) {
  const { role, isAuthenticated } = useAppSelector((state) => state.auth);
  const location = useLocation();

  if (!isAuthenticated) {
    // Redirect to sign-in page, but save the current location they were trying to go to
    return <Navigate to="/sign-in" state={{ from: location }} replace />;
  }

  if (!allowedRoles.includes(role)) {
    // Redirect to the unauthorized or home page if they are not allowed
    return <Navigate to="/" replace />;
  }

  return <>{children}</>;
}

export default RequireRole;
