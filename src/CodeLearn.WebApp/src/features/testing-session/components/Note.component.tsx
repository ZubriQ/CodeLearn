import React from 'react';
import { ExerciseNoteDecoration } from '@/features/dashboard/tests/models/ExerciseNoteDecoration.ts';

interface NoteProps {
  decoration: ExerciseNoteDecoration;
  entry: string;
}

const Note: React.FC<NoteProps> = ({ decoration, entry }) => {
  let className = '';

  switch (decoration) {
    case ExerciseNoteDecoration.Plain:
      className = '';
      break;
    case ExerciseNoteDecoration.Bold:
      className = 'font-bold';
      break;
    case ExerciseNoteDecoration.Bordered:
      className = 'bg-zinc-200/70 rounded-lg px-2 text-zinc-700';
      break;
  }

  return (
    <li className="flex items-center">
      <span className="mr-2">•</span>
      <span className={className}>{entry}</span>
    </li>
  );
};

export default Note;
