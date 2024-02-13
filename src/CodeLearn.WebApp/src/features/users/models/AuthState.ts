import { User } from '@/features/users/models/User.ts';

export type AuthState = {
  token?: string | null;
  isLoading: boolean;
  userData?: User | null;
};
