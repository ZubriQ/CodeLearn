import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { logout } from '@/features/users/auth-slice.ts';
import { useAppDispatch } from '@/app/hooks.ts';

export default function SignOutPage() {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  useEffect(() => {
    dispatch(logout());

    setTimeout(() => navigate('/', { replace: true }), 1000);
  }, [dispatch, navigate]);

  return (
    <div className="flex min-h-screen items-center justify-center">
      <p className="text-2xl">Logging out...</p>
    </div>
  );
}
