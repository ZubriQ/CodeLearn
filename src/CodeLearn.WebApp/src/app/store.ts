import { persistStore, persistReducer, FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER } from 'redux-persist';
import storage from 'redux-persist/lib/storage';
import { configureStore } from '@reduxjs/toolkit';
import authReducer, { AuthState } from '@/features/users/auth-slice.ts';
import exerciseReducer, { ExerciseState } from '@/features/testing-session/testing-session-slice.ts';
import answeredQuestionsReducer from '@/features/testing-session/answered-questions-slice.ts';

export type RootState = {
  auth: AuthState;
  exercise: ExerciseState;
};

import { combineReducers } from '@reduxjs/toolkit';

const persistConfig = {
  key: 'root',
  storage,
};

const persistedReducer = persistReducer(persistConfig, authReducer);

const rootReducer = combineReducers({
  auth: persistedReducer,
  exercise: exerciseReducer,
  answeredQuestions: answeredQuestionsReducer,
});

export const store = configureStore({
  // reducer: {
  //   auth: persistedReducer,
  // },
  reducer: rootReducer,
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({
      serializableCheck: {
        ignoredActions: [FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER],
      },
    }),
});

export const persistor = persistStore(store);
