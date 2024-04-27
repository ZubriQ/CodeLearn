import { createSlice } from '@reduxjs/toolkit';

const completedExerciseIdsSlice = createSlice({
  name: 'completedExerciseIds',
  initialState: [],
  reducers: {
    setCompletedExercises: (_, action) => {
      return action.payload;
    },
  },
});

export const { setCompletedExercises } = completedExerciseIdsSlice.actions;
export default completedExerciseIdsSlice.reducer;
