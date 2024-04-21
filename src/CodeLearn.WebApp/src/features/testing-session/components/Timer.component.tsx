import React, { useEffect, useState } from 'react';

interface TimerProps {
  finishDateTime: string;
}

const Timer: React.FC<TimerProps> = ({ finishDateTime }) => {
  const [timeLeft, setTimeLeft] = useState('00:00:00');

  useEffect(() => {
    const interval = setInterval(() => {
      const now = new Date();
      const finishDate = new Date(finishDateTime); // Convert string to Date object
      const difference = finishDate.getTime() - now.getTime();

      if (difference <= 0) {
        clearInterval(interval);
        setTimeLeft('00:00:00');
        return;
      }

      const hours = Math.floor(difference / (1000 * 60 * 60));
      const minutes = Math.floor((difference % (1000 * 60 * 60)) / (1000 * 60));
      const seconds = Math.floor((difference % (1000 * 60)) / 1000);

      setTimeLeft(
        `${hours.toString().padStart(2, '0')}:${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')}`,
      );
    }, 1000);

    return () => clearInterval(interval);
  }, [finishDateTime]);

  return <>{timeLeft}</>;
};

export default Timer;
