import { createSlice, PayloadAction } from '@reduxjs/toolkit';

export type ExerciseState = {
  selectedChoices: Record<number, number[]>;
};

const initialState: ExerciseState = {
  selectedChoices: {},
};

const exerciseSlice = createSlice({
  name: 'exercise',
  initialState: initialState,
  reducers: {
    setSelectedChoices: (state, action: PayloadAction<{ exerciseId: number; choices: number[] }>) => {
      const { exerciseId, choices } = action.payload;
      state.selectedChoices[exerciseId] = choices;
    },
  },
});

export const { setSelectedChoices } = exerciseSlice.actions;
export default exerciseSlice.reducer;
