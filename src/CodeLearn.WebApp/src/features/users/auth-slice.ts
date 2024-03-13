// DUCKS pattern
import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { AuthState } from '@/features/users/models/AuthState.ts';

const initialState: AuthState = {
  token: undefined,
  isLoading: false,
  email: undefined,
  password: undefined,
};

const authSlice = createSlice({
  name: 'auth',
  initialState: initialState,
  reducers: {
    loginPending: (state: AuthState) => {
      state.isLoading = true;
    },
    loginSuccess: (state: AuthState, action: PayloadAction<{ token: string; email: string; password: string }>) => {
      state.isLoading = false;
      state.token = action.payload.token;
      state.email = action.payload.email;
      state.password = action.payload.password;
    },
    loginFailure: (state: AuthState) => {
      state.isLoading = false;
      state.token = undefined;
      state.email = undefined;
      state.password = undefined;
    },
  },
});

export const { loginPending, loginSuccess, loginFailure } = authSlice.actions;
export default authSlice.reducer;
