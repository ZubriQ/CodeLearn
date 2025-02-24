import { create } from 'zustand';
import { persist, createJSONStorage } from 'zustand/middleware';
import { getRoleFromToken } from '@/lib/utils.ts';

interface LoginSuccess {
  jwtToken: string;
  refreshToken: string;
  username: string;
}

interface AuthState {
  isLoading: boolean;
  isAuthenticated: boolean;
  token: string | undefined;
  refreshToken: string | undefined;
  username: string | undefined;
  role: string | undefined;

  loginPending: () => void;
  loginSuccess: (payload: LoginSuccess) => void;
  loginFailure: () => void;
  logout: () => void;
}

const initialState = {
  isLoading: false,
  isAuthenticated: false,
  token: undefined,
  refreshToken: undefined,
  username: undefined,
  role: undefined,
};

const useAuthStore = create<AuthState>()(
  persist(
    (set) => ({
      ...initialState,

      loginPending: () => {
        set((state) => ({
          ...state,
          isLoading: true,
          isAuthenticated: false,
        }));
      },

      loginSuccess: (payload: LoginSuccess) => {
        set((state) => ({
          ...state,
          isLoading: false,
          token: payload.jwtToken,
          refreshToken: payload.refreshToken,
          username: payload.username,
          role: getRoleFromToken(payload.jwtToken),
          isAuthenticated: true,
        }));
      },

      loginFailure: () => {
        set((state) => ({
          ...state,
          isLoading: false,
          token: undefined,
          username: undefined,
          isAuthenticated: false,
        }));
      },

      logout: () => {
        set((state) => ({
          ...state,
          isLoading: false,
          isAuthenticated: false,
          token: undefined,
          refreshToken: undefined,
          username: undefined,
          role: undefined,
        }));
      },
    }),
    {
      name: 'auth',
      storage: createJSONStorage(() => localStorage),
      partialize: (state) => ({
        token: state.token,
        refreshToken: state.refreshToken,
        username: state.username,
        role: state.role,
        isAuthenticated: state.isAuthenticated,
      }),
    },
  ),
);

export default useAuthStore;
