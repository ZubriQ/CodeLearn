import { createSlice } from '@reduxjs/toolkit';

const answeredQuestionsSlice = createSlice({
  name: 'answeredQuestions',
  initialState: [],
  reducers: {
    setAnsweredQuestions: (_, action) => {
      return action.payload;
    },
  },
});

export const { setAnsweredQuestions } = answeredQuestionsSlice.actions;
export default answeredQuestionsSlice.reducer;
