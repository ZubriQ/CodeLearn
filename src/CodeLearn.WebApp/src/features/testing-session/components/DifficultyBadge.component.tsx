import React from 'react';
import { Badge } from '@/components/ui/badge.tsx';
import { ExerciseDifficulty } from '@/features/dashboard/tests/models/ExerciseDifficulty.ts';

interface ExerciseDifficultyProps {
  difficulty: ExerciseDifficulty;
}

const ExerciseDifficultyComponent: React.FC<ExerciseDifficultyProps> = ({ difficulty }) => {
  let badgeColorClass = '';

  switch (difficulty) {
    case ExerciseDifficulty.Hard:
      badgeColorClass = 'bg-red-100 text-red-500 hover:bg-red-100';
      break;
    case ExerciseDifficulty.Medium:
      badgeColorClass = 'bg-orange-100 text-orange-500 hover:bg-orange-100';
      break;
    case ExerciseDifficulty.Easy:
      badgeColorClass = 'bg-green-100 text-green-500 hover:bg-green-100';
      break;
    default:
      badgeColorClass = 'bg-zinc-500';
  }

  return (
    <Badge variant="secondary" className={`truncate ${badgeColorClass}`}>
      {ExerciseDifficulty[difficulty]}
    </Badge>
  );
};

export default ExerciseDifficultyComponent;
