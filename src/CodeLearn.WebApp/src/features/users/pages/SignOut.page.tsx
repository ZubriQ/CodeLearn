import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import useAuthStore from '@/store/auth.ts';

export default function SignOutPage() {
  const { logout } = useAuthStore();

  const navigate = useNavigate();

  useEffect(() => {
    logout();

    setTimeout(() => navigate('/', { replace: true }), 1000);
  }, [navigate]);

  return (
    <div className="flex min-h-screen items-center justify-center">
      <p className="text-2xl">Logging out...</p>
    </div>
  );
}
