// DUCKS pattern
import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { getRoleFromToken } from '@/lib/utils.ts';

export type AuthState = {
  isLoading: boolean;
  isAuthenticated: boolean;
  token: string | undefined;
  refreshToken: string | undefined;
  username: string | undefined;
  role: string | undefined;
};

const initialState: AuthState = {
  isLoading: false,
  isAuthenticated: false,
  token: undefined,
  refreshToken: undefined,
  username: undefined,
  role: undefined,
};

const authSlice = createSlice({
  name: 'auth',
  initialState: initialState,
  reducers: {
    loginPending: (state: AuthState) => {
      state.isLoading = true;
      state.isAuthenticated = false;
    },
    loginSuccess: (
      state: AuthState,
      action: PayloadAction<{
        jwtToken: string;
        refreshToken: string;
        username: string;
      }>,
    ) => {
      state.isLoading = false;
      state.token = action.payload.jwtToken;
      state.refreshToken = action.payload.refreshToken;
      state.username = action.payload.username;
      state.role = getRoleFromToken(action.payload.jwtToken);
      state.isAuthenticated = true;
    },
    loginFailure: (state: AuthState) => {
      state.isLoading = false;
      state.token = undefined;
      state.username = undefined;
      state.isAuthenticated = false;
    },
    logout: (state) => {
      state.isLoading = false;
      state.isAuthenticated = false;
      state.token = undefined;
      state.refreshToken = undefined;
      state.username = undefined;
      state.role = undefined;
    },
  },
});

export const { loginPending, loginSuccess, loginFailure, logout } = authSlice.actions;
export default authSlice.reducer;
