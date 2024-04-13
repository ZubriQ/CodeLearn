// DUCKS pattern
import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { getRoleFromToken } from '@/lib/utils.ts';

export type AuthState = {
  isLoading: boolean;
  token: string | undefined;
  refreshToken: string | undefined;
  username: string | undefined;
  role: string | undefined;
};

const initialState: AuthState = {
  isLoading: false,
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
    },
    loginFailure: (state: AuthState) => {
      state.isLoading = false;
      state.token = undefined;
      state.username = undefined;
    },
  },
});

export const { loginPending, loginSuccess, loginFailure } = authSlice.actions;
export default authSlice.reducer;
