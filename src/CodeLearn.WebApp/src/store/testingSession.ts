import { create } from 'zustand';
import { persist, createJSONStorage } from 'zustand/middleware';

interface TestingSessionState {
  /** ID's of all exercises (code + questions) */
  exerciseIds: number[];

  /** ID's of solved exercises and answered questions (code + questions) */
  completedExerciseIds: number[];

  /** Storing Answered Questions: exercise ID - Selected Options  */
  answeredExercises: Record<number, number[]>;

  /** Storing Code: exercise ID - Solution Code  */
  codingExercises: Record<number, string[]>;

  addExerciseIds: (newIds: number[]) => void;
  markExerciseComplete: (exerciseId: number) => void;
  saveQuestionAnswers: (exerciseId: number, selectedOptions: number[]) => void;
  saveCodeSolution: (exerciseId: number, code: string[]) => void;
  resetSession: () => void;
}

const initialState = {
  exerciseIds: [],
  completedExerciseIds: [],
  answeredExercises: {},
  codingExercises: {},
};

const useTestingSessionStore = create<TestingSessionState>()(
  persist(
    (set) => ({
      ...initialState,

      addExerciseIds: (newIds: number[]) =>
        set((state) => ({
          ...state,
          exerciseIds: [...new Set([...state.exerciseIds, ...newIds])],
        })),

      markExerciseComplete: (exerciseId: number) =>
        set((state) => ({
          ...state,
          completedExerciseIds: Array.from(new Set([...state.completedExerciseIds, exerciseId])),
        })),

      saveQuestionAnswers: (exerciseId: number, selectedOptions: number[]) =>
        set((state) => ({
          ...state,
          answeredExercises: {
            ...state.answeredExercises,
            [exerciseId]: selectedOptions,
          },
        })),

      saveCodeSolution: (exerciseId: number, code: string[]) =>
        set((state) => ({
          ...state,
          codingExercises: {
            ...state.codingExercises,
            [exerciseId]: code,
          },
        })),

      resetSession: () => set(() => initialState),
    }),
    {
      name: 'testing-session',
      storage: createJSONStorage(() => localStorage),
      partialize: (state) => ({
        exerciseIds: state.exerciseIds,
        completedExerciseIds: state.completedExerciseIds,
        answeredExercises: state.answeredExercises,
        codingExercises: state.codingExercises,
      }),
    },
  ),
);

export default useTestingSessionStore;
