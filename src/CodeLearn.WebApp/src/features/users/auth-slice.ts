// DUCKS pattern
import { AuthState } from '@/features/users/models/AuthState.ts';

import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { User } from '@/features/users/models/User.ts';

const initialState: AuthState = {
  token: null,
  isLoading: false,
  userData: null,
};

const authSlice = createSlice({
  name: 'auth',
  initialState: initialState,
  reducers: {
    loginPending: (state: AuthState) => {
      state.isLoading = true;
    },
    loginSuccess: (state: AuthState, action: PayloadAction<{ token: string; userData: User }>) => {
      state.isLoading = false;
      state.token = action.payload.token;
      state.userData = action.payload.userData;
    },
    loginFailure: (state: AuthState) => {
      state.isLoading = false;
      state.token = null;
      state.userData = null;
    },
  },
});

export const { loginPending, loginSuccess, loginFailure } = authSlice.actions;
export default authSlice.reducer;
